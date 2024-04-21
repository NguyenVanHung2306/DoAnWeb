using DoAnLapTrinhWeb.Models;
using DoAnLapTrinhWeb.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoAnLapTrinhWeb.Areas.Admin.Controllers
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
            if (ModelState.IsValid)
            {
                // Kiểm tra xem tên tác giả đã tồn tại hay chưa
                if (await _theLoaiyRepository.IsTenTheLoaiExisted(theLoai.tenTheLoai))
                {
                    // Hiển thị thông báo khi tên tác giả đã tồn tại
                    TempData["ErrorMessage"] = "Đã tồn tại tên thể loại, vui lòng nhập tên khác";
                    return View(theLoai);
                }
                else
                {
                    TempData["SuccessMessage"] = "Đã thêm thể loại thành công";
                    await _theLoaiyRepository.AddAsync(theLoai);
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Vui lòng nhập thông tin đầy đủ";
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
                TempData["SuccessMessage"] = "Đã cập nhật thể loại thành công";

            }
            else
            {
                TempData["ErrorMessage"] = "Vui lòng nhập thông tin đầy đủ";
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

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(int id, tbTheLoai theLoai)
        {

            if (theLoai != null)
            {
                await _theLoaiyRepository.DeleteAsync(id);
                TempData["SuccessMessage"] = "Xóa thể loại thành công";
            }
            return View(theLoai);
        }
    }
}
