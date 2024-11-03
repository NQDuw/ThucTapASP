using BaiTapThucTap.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaiTapThucTap.Controllers
{
    public class BaiTap3Controller : Controller
    {
        private readonly DBContext _db;
        private readonly IHostingEnvironment _hosting;
        public BaiTap3Controller(DBContext db, IHostingEnvironment hosting)
        {
            _db = db;
            _hosting = hosting;
        }
        public IActionResult Index()
        {
            var listSanPham = _db.tbl_DM_San_Pham.Include(x => x.LoaiSP).Include(x => x.DonViTinh).ToList();
            return View(listSanPham);
        }
        private void LoadViewBag()
        {
            ViewBag.LoaiSPList = _db.tbl_DM_Loai_San_Pham.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Ten_LSP
            }).ToList();

            ViewBag.DonViTinhList = _db.tbl_DM_Don_Vi_Tinh.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Ten_Don_Vi_Tinh
            }).ToList();
        }

        public IActionResult Add()
        {
            LoadViewBag();
            return View();
        }
        [HttpPost]
        public IActionResult Add(BaiTap3 danhmuc)
        {
            if (string.IsNullOrWhiteSpace(danhmuc.Ma_San_Pham))
            {
                ModelState.AddModelError("Ma_San_Pham", "Mã sản phẩm không được để trống.");
            }
            else
            {
                var ktrMa = _db.tbl_DM_San_Pham
                              .FirstOrDefault(d => d.Ma_San_Pham.Trim().ToLower() == danhmuc.Ma_San_Pham.Trim().ToLower());

                if (ktrMa != null)
                {
                    ModelState.AddModelError("Ma_San_Pham", "Mã  sản phẩm đã tồn tại.");
                }
            }

            if (string.IsNullOrWhiteSpace(danhmuc.Ten_San_Pham))
            {
                ModelState.AddModelError("Ten_San_Pham", "Tên loại sản phẩm không được để trống.");
            }
            else
            {
                var ktten = _db.tbl_DM_San_Pham
                              .FirstOrDefault(d => d.Ten_San_Pham.Trim().ToLower() == danhmuc.Ten_San_Pham.Trim().ToLower());

                if (ktten != null)
                {
                    ModelState.AddModelError("Ten_San_Pham", "Tên  sản phẩm đã tồn tại.");
                }
            }
            LoadViewBag();
            if (danhmuc.Loai_San_Pham_ID <= 0)
            {
                ModelState.AddModelError("Loai_San_Pham_ID", "Vui lòng chọn loại sản phẩm.");
            }

            if (danhmuc.Don_Vi_Tin_ID <= 0)
            {
                ModelState.AddModelError("Don_Vi_Tin_ID", "Vui lòng chọn đơn vị tính.");
            }

            if (ModelState.IsValid)
            {
                _db.tbl_DM_San_Pham.Add(danhmuc);
                _db.SaveChanges();
                TempData["success"] = "Thêm Thành Công";
                return RedirectToAction("Index");
            }

            Console.WriteLine(ViewBag.DonViTinhList);
            return View(danhmuc);
        }

        public IActionResult Update(int id)
        {
            var sp = _db.tbl_DM_San_Pham.Find(id);
            if (sp == null)
            {
                return NotFound();
            }
            return View(sp);
        }
        [HttpPost]

        public IActionResult Update(BaiTap3 sp)
        {
            var existingSP = _db.tbl_DM_San_Pham.Find(sp.Id);

            if (string.IsNullOrWhiteSpace(sp.Ma_San_Pham))
            {
                ModelState.AddModelError("Ma_San_Pham", "Mã sản phẩm không được để trống.");
            }
            else
            {
                var oldMaSanPham = existingSP.Ma_San_Pham;
                var ktrMaTrung = _db.tbl_DM_San_Pham
                             .FirstOrDefault(d => d.Ma_San_Pham.Trim().ToLower() == sp.Ma_San_Pham.Trim().ToLower());
                if (sp.Ma_San_Pham != oldMaSanPham)
                {
                    if (ktrMaTrung != null)
                    {
                        ModelState.AddModelError("Ma_San_Pham", "Mã sản phẩm đã tồn tại.");
                    }
                    else
                    {
                        existingSP.Ma_San_Pham = sp.Ma_San_Pham;
                    }
                }
            }
            if (string.IsNullOrWhiteSpace(sp.Ten_San_Pham))
            {
                ModelState.AddModelError("Ten_San_Pham", "Tên loại sản phẩm không được để trống.");

            }
            else
            {
                var oldTenSanPham = existingSP.Ten_San_Pham;

                var ktrTenTrung = _db.tbl_DM_San_Pham
                                  .FirstOrDefault(d => d.Ten_San_Pham.Trim().ToLower() == sp.Ten_San_Pham.Trim().ToLower());

                if (sp.Ten_San_Pham != oldTenSanPham)
                {
                    if (ktrTenTrung != null)
                    {
                        ModelState.AddModelError("Ten_San_Pham", "Tên sản phẩm đã tồn tại.");
                    }
                    else
                    {
                        existingSP.Ten_San_Pham = sp.Ten_San_Pham;
                    }
                }
            }
            LoadViewBag();
            if (sp.Loai_San_Pham_ID <= 0)
            {
                ModelState.AddModelError("Loai_San_Pham_ID", "Vui lòng chọn loại sản phẩm.");
            }

            if (sp.Don_Vi_Tin_ID <= 0)
            {
                ModelState.AddModelError("Don_Vi_Tin_ID", "Vui lòng chọn đơn vị tính.");
            }


            if (ModelState.IsValid)
            {
                existingSP.Ten_San_Pham = sp.Ten_San_Pham;
                existingSP.Ma_San_Pham = sp.Ma_San_Pham;
                existingSP.Ten_San_Pham = sp.Ten_San_Pham;
                existingSP.Loai_San_Pham_ID = sp.Loai_San_Pham_ID;
                existingSP.Don_Vi_Tin_ID = sp.Don_Vi_Tin_ID;
                existingSP.Ghi_Chu = sp.Ghi_Chu;
                _db.SaveChanges();

                TempData["success"] = "Sửa Thành Công";
                return RedirectToAction("Index");
            }
            return View(sp);
        }
        //Hiển thị form xác nhận xóa chủng loại
        public IActionResult Delete(int id)
        {
            var lsp = _db.tbl_DM_San_Pham.FirstOrDefault(x => x.Id == id);
            if (lsp == null)
            {
                return NotFound();
            }
            return View(lsp);
        }
        // Xử lý xóa chủng loại
        public IActionResult DeleteConfirmed(int id)
        {
            var lsp = _db.tbl_DM_San_Pham.Find(id);
            if (lsp == null)
            {
                return NotFound();
            }
            _db.tbl_DM_San_Pham.Remove(lsp);
            _db.SaveChanges();
            TempData["success"] = "Xóa Thành Công";
            return RedirectToAction("Index");
        }
    }
}
