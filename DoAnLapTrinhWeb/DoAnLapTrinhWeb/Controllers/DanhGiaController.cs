using DoAnLapTrinhWeb.Models;
using DoAnLapTrinhWeb.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoAnLapTrinhWeb.Controllers
{
    [Authorize]
    public class DanhGiaController : Controller
    {
        private readonly ISachRepository _sachRepository;
        private readonly ApplicationDbContext _context;
        private readonly UserManager <ApplicationUser> _userManager;
        public DanhGiaController(ApplicationDbContext context,
                                UserManager<ApplicationUser> userManager,
                                ISachRepository sachRepository)
        {
            _sachRepository = sachRepository;
            _context = context;
            _userManager = userManager;
        }

        public async Task<ActionResult> Index(int sachId)
        {
            var books = await _context.tbPhieuDanhGia.Include(p => p.Sach)
                                                    .Where(p => p.sachId == sachId)
                                                    .ToListAsync(); 
            ViewBag.SachId = sachId;
            return View(books);
        }

        public async Task<IActionResult> LuuDanhGia(int sachId)
        {
            ViewBag.sachId = sachId;
            var user = await _userManager.GetUserAsync(User);
            ViewBag.UserId = user.Id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LuuDanhGia(tbPhieuDanhGia model, int sachId)
        {
            // Lưu đánh giá vào CSDL
            _context.tbPhieuDanhGia.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("LuuDanhGia", "DanhGia", new { sachId = sachId });
        }
    }
}
