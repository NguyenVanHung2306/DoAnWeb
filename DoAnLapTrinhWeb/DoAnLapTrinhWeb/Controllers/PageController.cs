using DoAnLapTrinhWeb.Models;
using DoAnLapTrinhWeb.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DoAnLapTrinhWeb.Controllers
{
    public class PageController : Controller
    {
        private readonly ITrangRepository _trangRepository;
        private readonly ISachRepository _sachRepository;
        private readonly ApplicationDbContext _context;
        public PageController(ITrangRepository trangRepository, ISachRepository sachRepository, ApplicationDbContext _context)

        {
            _trangRepository = trangRepository;
            _sachRepository = sachRepository;

        }
        public async Task<IActionResult> Index()
        {
            var page = await _trangRepository.GetAllAsync();
            var trangsSortedByTenSach = page.OrderBy(t => t.sach.tenSach);
            return View(trangsSortedByTenSach);
        }


        public async Task<IActionResult> Add(int Id)
        {
            var sach = await _sachRepository.GetByIdAsync(Id);
            return View(sach);
        }

        [HttpPost]
        public async Task<IActionResult> Add(int Id, List<IFormFile> imageurls)
        {
            var Sach = await _sachRepository.GetByIdAsync(Id);
            if (Sach != null)
            {
                Sach.TbTrangs = new List<TbTrang>();
            }


            if (imageurls != null)
            {
                foreach (var imageUrl in imageurls)
                {
                    var savedImage = await SaveImage(imageUrl);

                    // Tạo một đối tượng PostJobImage mới
                    var postJobImage = new TbTrang
                    {
                        SachId = Id,
                        Noidung = savedImage,
                    };

                    // Thêm đối tượng PostJobImage mới vào danh sách post_job.post_Job_Images
                    await _trangRepository.AddAsync(postJobImage);
                }

            }
            else
            {
                ModelState.AddModelError("ImageUrl", "Vui lòng chọn một tệp hình ảnh hợp lệ và có kích thước nhỏ hơn 5MB.");
            }
            // Hiển thị thông báo khi thêm tác giả thành công
            TempData["SuccessMessage"] = "Đã thêm tác giả thành công.";

            return RedirectToAction(nameof(Index));
        }


        private async Task<string> SaveImage(IFormFile image)
        {
            var savePath = Path.Combine("wwwroot/pages", image.FileName); //

            using (var fileStream = new FileStream(savePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            return "/pages/" + image.FileName; // Trả về đường dẫn tương đối
        }


    }

}
