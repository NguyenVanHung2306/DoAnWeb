using DoAnLapTrinhWeb.Models;
using DoAnLapTrinhWeb.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DoAnLapTrinhWeb.Areas.Admin.Models;

namespace DoAnLapTrinhWeb.Areas.Admin.Controlle
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class BooksController : Controller
    {
        private readonly ISachRepository _sachRepository;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITheLoaiRepository _theLoaiyRepository;
        private readonly ITacGiaRepository _tacGiaRepository;
        private readonly IUserRepository _userRepository;

        public BooksController(ISachRepository sachRepositoryy,
                                UserManager<ApplicationUser> userManager, 
                                ApplicationDbContext context,
                                ITheLoaiRepository theLoaiRepository,
                                ITacGiaRepository tacGiaRepository,
                                IUserRepository userRepository)
        {
            _sachRepository = sachRepositoryy;
            _context = context;
            _userManager = userManager;
            _theLoaiyRepository = theLoaiRepository;
            _tacGiaRepository = tacGiaRepository;
            _userRepository = userRepository;
        }

        //Action xuất danh sách các cuốn sách

        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = 6; // Số lượng sách trên mỗi trang
            int pageNumber = page ?? 1; // Trang hiện tại, nếu không có thì mặc định là trang 1

            // Lấy danh sách sách từ repository
            var sachList = await _sachRepository.GetAllAsync();

            // Thực hiện phân trang bằng LINQ
            var pagedSachList = sachList.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            // Tính toán số lượng trang
            int totalSachCount = sachList.Count();
            int pageCount = (int)Math.Ceiling((double)totalSachCount / pageSize);

            // Thống kê số lượng người dùng đã đăng ký
            var totalUsers = await _userManager.Users.CountAsync();

            // Thống kê số lượng cuốn sách trong web
            var bookCount = await _sachRepository.GetBookCountAsync();

            // Gán giá trị số lượng trang, số lượng người dùng và số lượng sách cho ViewBag
            ViewBag.PageCount = pageCount;
            ViewBag.TotalUsers = totalUsers;
            ViewBag.BookCount = bookCount;

            return View(pagedSachList);
        }

        public async Task<ActionResult> Index2()
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
            var user = await _userManager.GetUserAsync(User);
            var books = await _sachRepository.GetByIdAsync(id);
            if (books == null)
            {
                return NotFound();
            }
            ViewBag.UserId = user.Id;
            return View(books);
        }

        //Action để đọc các trang sách
        public async Task<IActionResult> Read(int sachId)
        {
            var user = await _userManager.GetUserAsync(User);
            var lichSu = _context.tbLichSu.FirstOrDefault(p => p.sachId == sachId && p.userId == user.Id);
            if (lichSu == null)
            {
                //Nếu sách chưa có trong lịch sử thì thêm vào
                tbLichSu tempLichSu = new tbLichSu();
                tempLichSu.sachId = sachId;
                tempLichSu.userId = user.Id;
                tempLichSu.thoiGianDoc = DateTime.Now;
                _context.tbLichSu.Add(tempLichSu);
            }
            else
            {
                //Đã có thì cập nhật thời gian đọc
                lichSu.thoiGianDoc = DateTime.UtcNow;
            }

            _context.SaveChanges();
            ViewBag.SachId = sachId;
            ViewBag.UserId = user.Id;
            return View();
        }

        //Action dùng để lấy danh sách trang
        [HttpGet]
        public ActionResult GetImagePaths(int sachId)
        {
            var result = from c in _context.tbTrang
                         where c.SachId == sachId
                         select c;
            // Truy vấn CSDL và lấy danh sách đường dẫn hình ảnh
            List<string> imagePaths = result.Select(c => c.Noidung).ToList();
            return new JsonResult(imagePaths);
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
            return RedirectToAction(nameof(Index2));
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

        public async Task<IActionResult> Update(int id)
        {
            // Lấy thông tin cuốn sách cần cập nhật từ ID
            var sach = await _sachRepository.GetByIdAsync(id);
            if (sach == null)
            {
                return NotFound();
            }

            // Lấy danh sách thể loại và tác giả để hiển thị trong dropdownlist
            var tbTheLoais = await _theLoaiyRepository.GetAllAsync();
            ViewBag.TheLoai = new SelectList(tbTheLoais, "Id", "tenTheLoai", sach.theLoaiId);
            var tbTacGias = await _tacGiaRepository.GetAllAsync();
            ViewBag.TacGia = new SelectList(tbTacGias, "Id", "TenTacGia", sach.tacGiaId);

            // Trả về view với thông tin sách cần cập nhật
            return View(sach);
        }

        // Hành động xử lý việc cập nhật sách
        [HttpPost]
        public async Task<IActionResult> Update(int id, tbSach sach, IFormFile imageUrl)
        {
            /*            Console.WriteLine("New Image URL: " + newImageUrl);*/
            // Kiểm tra xem sách có tồn tại không
            var existingSach = await _sachRepository.GetByIdAsync(id);
            if (existingSach == null)
            {
                return NotFound();
            }


            // Cập nhật thông tin sách
            existingSach.tenSach = sach.tenSach;
            existingSach.moTa = sach.moTa;
            existingSach.tacGiaId = sach.tacGiaId;
            existingSach.theLoaiId = sach.theLoaiId;

            // Kiểm tra và lưu ảnh mới nếu có
            if (imageUrl != null)
            {
                existingSach.imageUrl = await SaveImage(imageUrl);
            }

            // Cập nhật sách trong cơ sở dữ liệu
            await _sachRepository.UpdateAsync(existingSach);

            // Hiển thị thông báo khi cập nhật thành công
            TempData["SuccessMessage"] = "Đã cập nhật thông tin sách thành công.";

            return RedirectToAction(nameof(Index2));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var sach = await _sachRepository.GetByIdAsync(id);
            if (sach == null)
            {
                return NotFound();
            }
            return View(sach);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sach = await _sachRepository.GetByIdAsync(id);
            if (sach != null)
            {
                await _sachRepository.DeleteAsync(id);
            }
            return RedirectToAction(nameof(Index2));
        }

        public async Task<IActionResult> ListUser()
        {
            var listUser = await _userRepository.GetAllAsync();
            return View(listUser);
        }

        [HttpGet]
        public async Task<IActionResult> deleteSoft(string id)
        {
            var listUser = await _userRepository.GetByIdAsync(id);
            if (listUser == null)
            {
                return NotFound();
            }
            else
            {
                listUser.is_active = 0;
                await _userManager.UpdateAsync(listUser);
            }
            return RedirectToAction(nameof(ListUser));
        }
        [HttpGet]
        public async Task<IActionResult> activeSoft(string id)
        {
            var listUser = await _userRepository.GetByIdAsync(id);
            if (listUser == null)
            {
                return NotFound();
            }
            else
            {
                listUser.is_active = 1;
                await _userManager.UpdateAsync(listUser);
            }
            return RedirectToAction(nameof(ListUser));
        }
    }
}
