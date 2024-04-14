using DoAnLapTrinhWeb.Areas.Admin.Models;
using DoAnLapTrinhWeb.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoAnLapTrinhWeb.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private readonly ISachRepository _sachRepository;
        public BooksController(ISachRepository sachRepositoryy)

        {
            _sachRepository = sachRepositoryy;
        }

        //Action xuất danh sách các cuốn sách
        public async Task<ActionResult> Index()
        {
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
            return View(books);
        }


    }
}
