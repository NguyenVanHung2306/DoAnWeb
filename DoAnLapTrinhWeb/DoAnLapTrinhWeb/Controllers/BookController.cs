using DoAnLapTrinhWeb.Models;
using DoAnLapTrinhWeb.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DoAnLapTrinhWeb.Controllers
{
    public class BookController : Controller
    {

        private readonly ISachRepository _sachRepository;
        private readonly ITheLoaiRepository _theLoaiyRepository;
        private readonly ITacGiaRepository _tacGiaRepository;
        ApplicationDbContext _context;
        public BookController(ISachRepository sachRepository,

        ITheLoaiRepository theLoaiRepository,
        ITacGiaRepository tacGiaRepository)


        {
            _sachRepository = sachRepository;
            _theLoaiyRepository = theLoaiRepository;
            _tacGiaRepository = tacGiaRepository;

        }
        public async Task<IActionResult> Index()
        {
            var products = await _sachRepository.GetAllAsync();
            return View(products);
        }
        // Hiển thị form thêm sách
        public async Task<IActionResult> Add()
        {
            var tbTheLoais = await _theLoaiyRepository.GetAllAsync();
            ViewBag.TheLoai = new SelectList(tbTheLoais, "Id", "tenTheLoai");
            var tbTacGias = await _tacGiaRepository.GetAllAsync();
            ViewBag.TacGia = new SelectList(tbTacGias, "Id", "TenTacGia");
            return View();

        }
        [HttpPost]
        //Xữ lý thêm sách
        public async Task<IActionResult> Add(tbSach sach, IFormFile imageUrl)
        {
            // Kiểm tra xem tên sách có trống không và có quá 150 ký tự không
            if (string.IsNullOrEmpty(sach.tenSach) || sach.tenSach.Length > 150)
            {
                ModelState.AddModelError("tenSach","Tên sách không hợp lệ. Tên sách không được để trống và không được vượt quá 150 ký tự.");
                return View(sach);
            }

            // Kiểm tra xem tên sách có trùng không
            if (await _sachRepository.IsTenSachExisted(sach.tenSach))
            {
                ModelState.AddModelError("tenSach", "Tên sách bị trùng, bạn có thể đổi tên khác.");
                return View(sach);
            }

            if (imageUrl != null)
            {
                // Lưu hình ảnh đại diện
                sach.imageUrl = await SaveImage(imageUrl);
            }

            // Thêm sách vào cơ sở dữ liệu
            await _sachRepository.AddAsync(sach);

            // Hiển thị thông báo khi thêm sách thành công
            TempData["SuccessMessage"] = "Đã thêm sách thành công.";

            return RedirectToAction(nameof(Index));
        }


        // hàm SaveImage 
        private async Task<string> SaveImage(IFormFile image)
        {
            var savePath = Path.Combine("wwwroot/images", image.FileName); //

            using (var fileStream = new FileStream(savePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            return "/images/" + image.FileName; // Trả về đường dẫn tương đối
        }
        public IActionResult AddTacGia()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddTacGia(tbTacGia tacGia)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem tên tác giả đã tồn tại hay chưa
                if (await _tacGiaRepository.IsTenTacGiaExisted(tacGia.TenTacGia))
                {
                    // Hiển thị thông báo khi tên tác giả đã tồn tại
                    ModelState.AddModelError("TenTacGia", "Đã có tác giả này trong danh sách, không thể thêm.");
                    return View(tacGia);
                }

                // Thêm tác giả vào cơ sở dữ liệu
                await _tacGiaRepository.AddAsync(tacGia);

                // Hiển thị thông báo khi thêm tác giả thành công
                TempData["SuccessMessage"] = "Đã thêm tác giả thành công.";

                return RedirectToAction(nameof(Index));
            }
            return View(tacGia);
        }

        public IActionResult AddTheLoai()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddTheLoai(tbTheLoai theLoai)
        {
            if (ModelState.IsValid)
            {
                // Thêm thể loại vào cơ sở dữ liệu
                await _theLoaiyRepository.AddAsync(theLoai);

                // Hiển thị thông báo khi thêm thể loại thành công
                TempData["SuccessMessage"] = "Đã thêm thể loại thành công.";

                return RedirectToAction(nameof(Index));
            }
            return View(theLoai);
        }

    }

}
