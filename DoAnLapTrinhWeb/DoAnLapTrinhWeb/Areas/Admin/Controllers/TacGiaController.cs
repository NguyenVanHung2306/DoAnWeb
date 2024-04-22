using DoAnLapTrinhWeb.Areas.Admin.Models;
using DoAnLapTrinhWeb.Models;
using DoAnLapTrinhWeb.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoAnLapTrinhWeb.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class TacGiaController : Controller
    {
        private readonly ISachRepository _sachRepository;
        private readonly ITheLoaiRepository _theLoaiyRepository;
        private readonly ITacGiaRepository _tacGiaRepository;

        public TacGiaController(ISachRepository sachRepository,
        ITheLoaiRepository theLoaiRepository,
        ITacGiaRepository tacGiaRepository)
        {
            _sachRepository = sachRepository;
            _theLoaiyRepository = theLoaiRepository;
            _tacGiaRepository = tacGiaRepository;
        }

        public async Task<IActionResult> Index()
        {
            var tacGia = await _tacGiaRepository.GetAllAsync();
            return View(tacGia);
        }

        public async Task<IActionResult> Display(int id)
        {
            var tacGia = await _tacGiaRepository.GetByIdAsync(id);
            if (tacGia == null)
            {
                return NotFound();
            }
            return View(tacGia);
        }

        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(tbTacGia tacGia)
        {
            //if (ModelState.IsValid)
            //{
            //    // Kiểm tra xem tên tác giả đã tồn tại hay chưa
            //    if (await _tacGiaRepository.IsTenTacGiaExisted(tacGia.TenTacGia))
            //    {
            //        // Hiển thị thông báo khi tên tác giả đã tồn tại
            //        ModelState.AddModelError("TenTacGia", "Đã có tác giả này trong danh sách, không thể thêm.");
            //        return View(tacGia);
            //    }

            //    // Thêm tác giả vào cơ sở dữ liệu
            //    await _tacGiaRepository.AddAsync(tacGia);

            //    // Hiển thị thông báo khi thêm tác giả thành công
            //    TempData["SuccessMessage"] = "Đã thêm tác giả thành công.";

            //    return RedirectToAction(nameof(Index));
            //}
            //return View(tacGia);
            if (string.IsNullOrEmpty(tacGia.TenTacGia))
            {
                TempData["ErrorMessage"] = "Vui lòng nhập đầy đủ";
                return View(tacGia);
            }
            // Kiểm tra xem tên tác giả đã tồn tại hay chưa
            if (await _tacGiaRepository.IsTenTacGiaExisted(tacGia.TenTacGia))
            {
                // Hiển thị thông báo khi tên tác giả đã tồn tại
                TempData["ErrorMessage"] = "Đã tồn tại tên tác giả, vui lòng nhập tên khác";
                return View(tacGia);
            }
            // Thêm tác giả vào cơ sở dữ liệu

            await _tacGiaRepository.AddAsync(tacGia);

            // Hiển thị thông báo khi thêm tác giả thành công

            TempData["SuccessMessage"] = "Đã thêm tác giả thành công.";

            return View(tacGia);
        }
        public async Task<IActionResult> Update(int id)
        {
            var tacGia = await _tacGiaRepository.GetByIdAsync(id);
            if (tacGia == null)
            {
                return NotFound();
            }
            return View(tacGia);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, tbTacGia tacGia)
        {
            //if (id != tacGia.Id)
            //{
            //    return NotFound();
            //}
            //if (ModelState.IsValid)
            //{
            //    await _tacGiaRepository.UpdateAsync(tacGia);
            //    return RedirectToAction(nameof(Index));

            //}
            //return View(tacGia);
            if (id != tacGia.Id)
            {
                return NotFound();
            }

            if (string.IsNullOrEmpty(tacGia.TenTacGia))
            {
                TempData["ErrorMessage"] = "Vui lòng nhập thông tin đầy đủ";
                return View(tacGia);
            }

            await _tacGiaRepository.UpdateAsync(tacGia);

            // Hiển thị thông báo khi thêm tác giả thành công

            TempData["SuccessMessage"] = "Đã cập nhật tác giả thành công";

            return View(tacGia);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var tacGia = await _tacGiaRepository.GetByIdAsync(id);
            if (tacGia == null)
            {
                return NotFound();
            }
            return View(tacGia);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tacGia = await _tacGiaRepository.GetByIdAsync(id);
            if (tacGia != null)
            {
                await _tacGiaRepository.DeleteAsync(id);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
