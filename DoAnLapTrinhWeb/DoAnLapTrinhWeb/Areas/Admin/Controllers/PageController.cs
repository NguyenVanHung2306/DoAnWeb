using DoAnLapTrinhWeb.Areas.Admin.Models;
using DoAnLapTrinhWeb.Models;
using DoAnLapTrinhWeb.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DoAnLapTrinhWeb.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PageController : Controller
    {
        private readonly ITrangRepository _trangRepository;
        private readonly ISachRepository _sachRepository;
        private readonly ApplicationDbContext _context;
        public PageController(ITrangRepository trangRepository, ISachRepository sachRepository, ApplicationDbContext context)

        {
            _trangRepository = trangRepository;
            _sachRepository = sachRepository;
            _context = context;
        }
        public async Task<IActionResult> Index(int sachId)
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

                    // Tạo một đối tượng pageImage mới
                    var pageImage = new TbTrang
                    {
                        SachId = Id,
                        Noidung = savedImage,
                    };

                    // Thêm đối tượng pageImage mới vào danh sách
                    await _trangRepository.AddAsync(pageImage);
                }
            }
            else
            {
                ModelState.AddModelError("ImageUrl", "Vui lòng chọn một tệp hình ảnh hợp lệ và có kích thước nhỏ hơn 5MB.");
            }
            // Hiển thị thông báo khi thêm tác giả thành công
            TempData["SuccessMessage"] = "Đã thêm tác giả thành công.";

            return RedirectToAction(nameof(Index));
            //return RedirectToAction("Details", "Books", new { id = Id });
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
        public async Task<IActionResult> Details(int Id)
        {
            var page = _context.tbTrang.FirstOrDefault(p => p.Id == Id);
            return View(page);
        }

    }

}
