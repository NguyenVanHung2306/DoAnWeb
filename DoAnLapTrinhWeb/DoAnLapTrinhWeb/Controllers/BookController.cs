using DoAnLapTrinhWeb.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DoAnLapTrinhWeb.Controllers
{
    public class BookController : Controller
    {
        private readonly ISachRepository _sachRepository;
        public BookController(ISachRepository sachRepositoryy)

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
