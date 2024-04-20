﻿using DoAnLapTrinhWeb.Models;
using DoAnLapTrinhWeb.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;


namespace DoAnLapTrinhWeb.Controllers
{
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
            if (ModelState.IsValid)
            {
                // Kiểm tra xem tên tác giả đã tồn tại hay chưa
                if (await _tacGiaRepository.IsTenTacGiaExisted(tacGia.TenTacGia))
                {
                    // Hiển thị thông báo khi tên tác giả đã tồn tại
                    TempData["ErrorMessage"] = "Đã tồn tại tên tác giả, vui lòng nhập tên khác";
                    return View(tacGia);
                }
                else
                {
                    // Thêm tác giả vào cơ sở dữ liệu

                    await _tacGiaRepository.AddAsync(tacGia);

                    // Hiển thị thông báo khi thêm tác giả thành công

                    TempData["SuccessMessage"] = "Đã thêm tác giả thành công.";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Vui long nhap day du";
            }
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
            if (id != tacGia.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await _tacGiaRepository.UpdateAsync(tacGia);
                TempData["SuccessMessage"] = "Đã cập nhật tác giả thành công";

            }
            else
            {
                TempData["ErrorMessage"] = "Vui lòng nhập thông tin đầy đủ";
            }
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

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(int id, tbTacGia tacGia)
        {
            if (tacGia != null)
            {
                await _tacGiaRepository.DeleteAsync(id);
                TempData["SuccessMessage"] = "Xóa tác giả thành công";
            }
            return View(tacGia);
        }
    }
}