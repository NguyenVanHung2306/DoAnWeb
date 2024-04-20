using DoAnLapTrinhWeb.Models;
using DoAnLapTrinhWeb.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DoAnLapTrinhWeb.Controllers
{
    public class TheLoaiController : Controller
    {
        private readonly ISachRepository _sachRepository;
        private readonly ITheLoaiRepository _theLoaiyRepository;
        private readonly ITacGiaRepository _tacGiaRepository;

        public TheLoaiController(ISachRepository sachRepository,
        ITheLoaiRepository theLoaiRepository,
        ITacGiaRepository tacGiaRepository)
        {
            _sachRepository = sachRepository;
            _theLoaiyRepository = theLoaiRepository;
            _tacGiaRepository = tacGiaRepository;
        }

        public async Task<IActionResult> Index()
        {
            var theLoai = await _theLoaiyRepository.GetAllAsync();
            return View(theLoai);
        }

        public async Task<IActionResult> Display(int id)
        {
            var theLoai = await _theLoaiyRepository.GetByIdAsync(id);
            if (theLoai == null)
            {
                return NotFound();
            }
            return View(theLoai);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(tbTheLoai theLoai)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem tên tác giả đã tồn tại hay chưa
                if (await _theLoaiyRepository.IsTenTheLoaiExisted(theLoai.tenTheLoai))
                {
                    // Hiển thị thông báo khi tên tác giả đã tồn tại
                    ModelState.AddModelError("TenTheLoai", "Đã có the Loai này trong danh sách, không thể thêm.");
                    return View(theLoai);
                }

                // Thêm tác giả vào cơ sở dữ liệu
                await _theLoaiyRepository.AddAsync(theLoai);

                // Hiển thị thông báo khi thêm tác giả thành công
                TempData["SuccessMessage"] = "Đã thêm the loai thành công.";

                return RedirectToAction(nameof(Index));
            }
            return View(theLoai);
        }

        public async Task<IActionResult> Update(int id)
        {
            var theLoai = await _theLoaiyRepository.GetByIdAsync(id);
            if (theLoai == null)
            {
                return NotFound();
            }
            return View(theLoai);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, tbTheLoai theLoai)
        {
            if (id != theLoai.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await _theLoaiyRepository.UpdateAsync(theLoai);
                return RedirectToAction(nameof(Index));

            }
            return View(theLoai);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var theLoai = await _theLoaiyRepository.GetByIdAsync(id);
            if (theLoai == null)
            {
                return NotFound();
            }
            return View(theLoai);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var theLoai = await _theLoaiyRepository.GetByIdAsync(id);
            if (theLoai != null)
            {
                await _theLoaiyRepository.DeleteAsync(id);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
