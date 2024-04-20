using DoAnLapTrinhWeb.Models;
using DoAnLapTrinhWeb.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
