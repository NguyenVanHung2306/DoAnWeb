using DoAnLapTrinhWeb.Areas.Admin.Models;
using DoAnLapTrinhWeb.Models;
using DoAnLapTrinhWeb.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;


namespace DoAnLapTrinhWeb.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private readonly ISachRepository _sachRepository;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public BooksController(ISachRepository sachRepositoryy,
                                UserManager<ApplicationUser> userManager, 
                                ApplicationDbContext context)
        {
            _sachRepository = sachRepositoryy;
            _context = context;
            _userManager = userManager;

        }

        //Action Index tượng trưng
        
        //Action danh sách các cuốn sách có phân trang
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

            // Gán giá trị số lượng trang cho ViewBag
            ViewBag.PageCount = pageCount;
            var user = await _userManager.GetUserAsync(User);
            ViewBag.UserId = user.Id;
            return View(pagedSachList);
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

        //Action tìm kiếm
        public async Task<IActionResult> find(string search)
        {
            var result = from c in _context.tbSach
                         select c;
            if (!string.IsNullOrEmpty(search))
            {
                result = from c in _context.tbSach
                             where c.tenSach.Contains(search) || c.TacGia.TenTacGia.Contains(search) || c.theLoai.tenTheLoai.Contains(search)
                         select c;
            }
            List<tbSach> sachList = result.ToList();
            var user = await _userManager.GetUserAsync(User);
            ViewBag.UserId = user.Id;
            return View(sachList);
        }

        public IActionResult TimKiem(string term)
        {
            var sachGoiY = _context.tbSach
                            .Where(s => s.tenSach.Contains(term))
                            .Select(s => s.tenSach)
                            .Distinct()
                            .Take(10)
                            .ToList();

            var tacGiaGoiY = _context.tbTacGia
                                .Where(t => t.TenTacGia.Contains(term))
                                .Select(t => t.TenTacGia)
                                .Distinct()
                                .Take(10)
                                .ToList();

            var theLoaiGoiY = _context.tbTheLoai
                                .Where(t => t.tenTheLoai.Contains(term))
                                .Select(t => t.tenTheLoai)
                                .Distinct()
                                .Take(10)
                                .ToList();

            return Json(new { sach = sachGoiY, tacGia = tacGiaGoiY, theLoai = theLoaiGoiY });
        }
    }
}
