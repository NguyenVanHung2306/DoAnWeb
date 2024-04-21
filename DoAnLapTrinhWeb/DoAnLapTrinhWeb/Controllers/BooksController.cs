using DoAnLapTrinhWeb.Areas.Admin.Models;
using DoAnLapTrinhWeb.Models;
using DoAnLapTrinhWeb.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        //Action xuất danh sách các cuốn sách
        public async Task<ActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            ViewBag.UserId = user.Id;
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
    }
}
