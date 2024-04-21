    using DoAnLapTrinhWeb.Models;
using DoAnLapTrinhWeb.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DoAnLapTrinhWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]

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
            var sach = new tbSach { theLoaiId = -1, tacGiaId = -1 };
            return View(sach);

        }

        [HttpPost]
        //Xữ lý thêm sách
        public async Task<IActionResult> Add(tbSach sach, IFormFile imageUrl)
        {
            // Kiểm tra xem tên sách có trống không
            if (ModelState.IsValid)
            {
                if (await _sachRepository.IsTenSachExisted(sach.tenSach))
                {
                    TempData["ErrorMessage"] = "Tên sách đã tồn tại, vui lòng chọn tên sách khác.";
                    return View(sach);
                }
                else
                {
                    await _sachRepository.AddAsync(sach);

                    TempData["SuccessMessage"] = "Đã thêm sách thành công.";
                }

            }
            else
            {
                // Nếu ModelState không hợp lệ, tái tạo SelectList cho theLoaiId và tacGiaId
                var tbTheLoais = await _theLoaiyRepository.GetAllAsync();
                ViewBag.TheLoai = new SelectList(tbTheLoais, "Id", "tenTheLoai", sach.theLoaiId);
                var tbTacGias = await _tacGiaRepository.GetAllAsync();
                ViewBag.TacGia = new SelectList(tbTacGias, "Id", "TenTacGia", sach.tacGiaId);
                TempData["ErrorMessage"] = "Vui long nhap day du";
            }
            return View(sach);
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
