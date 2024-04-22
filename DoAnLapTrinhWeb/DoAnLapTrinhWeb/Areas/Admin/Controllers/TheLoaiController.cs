using DoAnLapTrinhWeb.Areas.Admin.Models;
using DoAnLapTrinhWeb.Models;
using DoAnLapTrinhWeb.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoAnLapTrinhWeb.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
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
            //if (ModelState.IsValid)
            //{
            //    // Kiểm tra xem tên tác giả đã tồn tại hay chưa
            //    if (await _theLoaiyRepository.IsTenTheLoaiExisted(theLoai.tenTheLoai))
            //    {
            //        // Hiển thị thông báo khi tên tác giả đã tồn tại
            //        ModelState.AddModelError("TenTheLoai", "Đã có the Loai này trong danh sách, không thể thêm.");
            //        return View(theLoai);
            //    }

            //    // Thêm tác giả vào cơ sở dữ liệu
            //    await _theLoaiyRepository.AddAsync(theLoai);

            //    // Hiển thị thông báo khi thêm tác giả thành công
            //    TempData["SuccessMessage"] = "Đã thêm the loai thành công.";

            //    return RedirectToAction(nameof(Index));
            //}
            //return View(theLoai);
            if (string.IsNullOrEmpty(theLoai.tenTheLoai))
            {
                TempData["ErrorMessage"] = "Vui lòng nhập thông tin đầy đủ";
                return View(theLoai);
            }

            // Kiểm tra xem tên thể loại đã tồn tại hay không
            if (await _theLoaiyRepository.IsTenTheLoaiExisted(theLoai.tenTheLoai))
            {
                TempData["ErrorMessage"] = "Đã tồn tại tên thể loại, vui lòng nhập tên khác";
                return View(theLoai);
            }

            // Nếu không có lỗi, thêm thể loại và hiển thị thông báo thành công
            await _theLoaiyRepository.AddAsync(theLoai);
            TempData["SuccessMessage"] = "Đã thêm thể loại thành công";

            return View(theLoai);
        }

        public async Task<IActionResult> Update (int id)
        {
            var theLoai = await _theLoaiyRepository.GetByIdAsync(id);
            if (theLoai == null)
            {
                return NotFound();
            }
            return View(theLoai);
        }
        [HttpPost]
        public async Task<IActionResult> Update (int id, tbTheLoai theLoai)
        {
            //if (id != theLoai.Id)
            //{
            //    return NotFound();
            //}
            //if (ModelState.IsValid)
            //{
            //    await _theLoaiyRepository.UpdateAsync(theLoai);
            //    return RedirectToAction(nameof(Index));

            //}
            //return View(theLoai);
            if (id != theLoai.Id)
            {
                return NotFound();
            }

            // Kiểm tra xem ModelState có hợp lệ không
            if (string.IsNullOrEmpty(theLoai.tenTheLoai))
            {
                TempData["ErrorMessage"] = "Vui lòng nhập thông tin đầy đủ";
                return View(theLoai);
            }

            await _theLoaiyRepository.UpdateAsync(theLoai);

            // Hiển thị thông báo khi thêm tác giả thành công

            TempData["SuccessMessage"] = "Đã cập nhật thể loại thành công";

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
