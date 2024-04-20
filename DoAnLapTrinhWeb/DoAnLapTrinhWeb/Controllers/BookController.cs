using DoAnLapTrinhWeb.Models;
using DoAnLapTrinhWeb.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DoAnLapTrinhWeb.Controllers
{
    public class BookController : Controller
    {
        private readonly ISachRepository _sachRepository;
        private readonly ITheLoaiRepository _theLoaiyRepository;
        private readonly ITacGiaRepository _tacGiaRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        public BookController(ISachRepository sachRepositoryy, UserManager<ApplicationUser> userManager,

        ITheLoaiRepository theLoaiRepository,
        ITacGiaRepository tacGiaRepository)

        {
            _sachRepository = sachRepositoryy;
            _userManager = userManager;
            _theLoaiyRepository = theLoaiRepository;
            _tacGiaRepository = tacGiaRepository;
        }

        //Action xuất danh sách các cuốn sách
        public async Task<ActionResult> Index()
        {
            //THong ke nguoi dung da dang ki 
            var totalUsers = await _userManager.Users.CountAsync();
            ViewBag.TotalUsers = totalUsers;

            //Thong ke so luong cuon sach trong trong web
            var bookCount = await _sachRepository.GetBookCountAsync();
            ViewBag.BookCount = bookCount;

            var books = await _sachRepository.GetAllAsync();

            return View(books);
        }

        //Action Xuất thông tin sách
        public async Task<IActionResult> Details(int id)
        {
            var books = await _sachRepository.GetByIdAsync(id);
            if (books == null)
            {
                return NotFound();
            }

            var tacGia = await _tacGiaRepository.GetByIdAsync(books.tacGiaId);

            // Truyền thông tin sách và tác giả vào view
            ViewBag.TacGia = tacGia;
            return View(books);
        }


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
                ModelState.AddModelError("tenSach", "Tên sách không hợp lệ. Tên sách không được để trống và không được vượt quá 150 ký tự.");
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

    }
}
