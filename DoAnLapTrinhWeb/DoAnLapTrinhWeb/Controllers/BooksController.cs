﻿using DoAnLapTrinhWeb.Areas.Admin.Models;
using DoAnLapTrinhWeb.Models;
using DoAnLapTrinhWeb.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DoAnLapTrinhWeb.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private readonly ISachRepository _sachRepository;
        private readonly ApplicationDbContext _context;
        public BooksController(ISachRepository sachRepositoryy, ApplicationDbContext context)

        {
            _sachRepository = sachRepositoryy;
            _context = context;
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
        public async Task<IActionResult> Read(int sachId)
        {
            //var books = await _sachRepository.GetByIdAsync(id);
            //if (books == null)
            //{
            //    return NotFound();
            //}
            ViewBag.SachId = sachId;
            return View();
        }
        [HttpGet]
        public ActionResult GetImagePaths(int sachId)
        {
            var result = from c in _context.tbTrang
                         where c.SachId == sachId
                         select c;
            // Truy vấn CSDL và lấy danh sách đường dẫn hình ảnh
            List<string> imagePaths = result.Select(c => c.Noidung).ToList();
            return new JsonResult(imagePaths);
        }
    }
}
