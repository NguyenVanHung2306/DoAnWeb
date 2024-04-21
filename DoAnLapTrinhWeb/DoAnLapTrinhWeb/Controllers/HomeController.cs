using DoAnLapTrinhWeb.Models;
using DoAnLapTrinhWeb.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
namespace DoAnLapTrinhWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISachRepository _sachRepository;
        private readonly ITheLoaiRepository _theLoaiyRepository;
        private readonly ITacGiaRepository _tacGiaRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        public HomeController(ISachRepository sachRepositoryy, UserManager<ApplicationUser> userManager,

        ITheLoaiRepository theLoaiRepository,
        ITacGiaRepository tacGiaRepository)

        {
            _sachRepository = sachRepositoryy;
            _userManager = userManager;
            _theLoaiyRepository = theLoaiRepository;
            _tacGiaRepository = tacGiaRepository;
        }
        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = 4; // S? l??ng sách trên m?i trang
            int pageNumber = page ?? 1; // Trang hi?n t?i, n?u không có thì m?c ??nh là trang 1

            var sachList = await _sachRepository.GetAllAsync(); // L?y danh sách sách t? repository

            // Th?c hi?n phân trang b?ng LINQ
            var pagedSachList = sachList.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            // Tính toán s? l??ng trang
            int totalSachCount = sachList.Count();
            int pageCount = (int)Math.Ceiling((double)totalSachCount / pageSize);

            // Gán giá tr? s? l??ng trang cho ViewBag.PageCount
            ViewBag.PageCount = pageCount;

            return View(pagedSachList);
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
