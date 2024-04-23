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
    public class LichSuController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public LichSuController(UserManager<ApplicationUser> userManager,
                                ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
        }
        // GET: LichSuController
        //public ActionResult Index(string userId)
        //{
        //    if (!User.Identity.IsAuthenticated)
        //    {
        //        return Redirect("https://localhost:7226/Identity/Account/Login"); // Chuyển hướng đến trang đăng nhập
        //    }
        //    else
        //    {
        //        ViewBag.UserId = userId;
        //        return View();
        //    }
        //}
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("https://localhost:7226/Identity/Account/Login"); // Chuyển hướng đến trang đăng nhập
            }
            else
            {
                var user = await _userManager.GetUserAsync(User);
                ViewBag.UserId = user.Id;
                return View();
            }
        }

        // GET: LichSuController/Delete/5
        public ActionResult Delete(int sachId, string userId)
        {
            var recordsToDelete = _context.tbLichSu.FirstOrDefault(lichSu => lichSu.userId == userId && lichSu.sachId == sachId);
            if(recordsToDelete != null)
            {
                _context.tbLichSu.Remove(recordsToDelete);
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "LichSu", new { userId = userId });
        }
    }
}
