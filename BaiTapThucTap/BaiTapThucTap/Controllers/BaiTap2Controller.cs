using BaiTapThucTap.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaiTapThucTap.Controllers
{
    public class BaiTap2Controller : Controller
    {
        private readonly DBContext _db;
        public BaiTap2Controller(DBContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var listLoaiSanPham = _db.tbl_DM_Loai_San_Pham.ToList();
            return View(listLoaiSanPham);
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(BaiTap2 danhmuc)
        {
            if (string.IsNullOrWhiteSpace(danhmuc.Ma_LSP))
            {
                ModelState.AddModelError("Ma_LSP", "Mã loại sản phẩm không được để trống.");
            }
            else
            {
                var ktrMa = _db.tbl_DM_Loai_San_Pham
                              .FirstOrDefault(d => d.Ma_LSP.Trim().ToLower() == danhmuc.Ma_LSP.Trim().ToLower());

                if (ktrMa != null)
                {
                    ModelState.AddModelError("Ma_LSP", "Mã loại sản phẩm đã tồn tại.");
                }
            }

            if (string.IsNullOrWhiteSpace(danhmuc.Ten_LSP))
            {
                ModelState.AddModelError("Ten_LSP", "Tên loại sản phẩm không được để trống.");
            }
            else
            {
                var ktten = _db.tbl_DM_Loai_San_Pham
                              .FirstOrDefault(d => d.Ten_LSP.Trim().ToLower() == danhmuc.Ten_LSP.Trim().ToLower());

                if (ktten != null)
                {
                    ModelState.AddModelError("Ten_LSP", "Tên loại sản phẩm đã tồn tại.");
                }
            }

            if (ModelState.IsValid)
            {
                _db.tbl_DM_Loai_San_Pham.Add(danhmuc);
                _db.SaveChanges();
                TempData["success"] = "Thêm Thành Công";
                return RedirectToAction("Index");
            }


            return View(danhmuc);
        }

        public IActionResult Update(int id)
        {
            var lsp = _db.tbl_DM_Loai_San_Pham.Find(id);
            if (lsp == null)
            {
                return NotFound();
            }
            return View(lsp);
        }
        [HttpPost]
        public IActionResult Update(BaiTap2 lsp)
        {
            var existingLSP = _db.tbl_DM_Loai_San_Pham.Find(lsp.Id);

            if (string.IsNullOrWhiteSpace(lsp.Ma_LSP))
            {
                ModelState.AddModelError("Ma_LSP", "Mã loại sản phẩm không được để trống.");
            }
            else
            {
                var oldMaLSP = existingLSP.Ma_LSP;
                var ktrMaTrung = _db.tbl_DM_Loai_San_Pham
                             .FirstOrDefault(d => d.Ma_LSP.Trim().ToLower() == lsp.Ma_LSP.Trim().ToLower());
                if (lsp.Ma_LSP != oldMaLSP)
                {
                    if (ktrMaTrung != null)
                    {
                        ModelState.AddModelError("Ma_LSP", "Mã loại sản phẩm đã tồn tại.");
                    }
                    else
                    {
                        existingLSP.Ma_LSP = lsp.Ma_LSP;
                    }
                }
            }

            if (string.IsNullOrWhiteSpace(lsp.Ten_LSP))
            {
                ModelState.AddModelError("Ten_LSP", "Tên loại sản phẩm không được để trống.");
            }
            else
            {
                var oldTenLSP = existingLSP.Ten_LSP;
                var ktrTenTrung = _db.tbl_DM_Loai_San_Pham
                                  .FirstOrDefault(d => d.Ten_LSP.Trim().ToLower() == lsp.Ten_LSP.Trim().ToLower()); ;
                if (lsp.Ten_LSP != oldTenLSP)
                {
                    if (ktrTenTrung != null)
                    {
                        ModelState.AddModelError("Ten_LSP", "Tên loại sản phẩm đã tồn tại.");
                    }
                    else
                    {
                        existingLSP.Ten_LSP = lsp.Ten_LSP;
                    }
                }
            }


            if (ModelState.IsValid)
            {
                existingLSP.Ten_LSP = lsp.Ten_LSP;
                existingLSP.Ma_LSP = lsp.Ma_LSP;
                existingLSP.Ghi_Chu = lsp.Ghi_Chu;
                _db.SaveChanges();

                TempData["success"] = "Sửa Thành Công";
                return RedirectToAction("Index");
            }
            return View(lsp);
        }
        public IActionResult Delete(int id)
        {
            var lsp = _db.tbl_DM_Loai_San_Pham.FirstOrDefault(x => x.Id == id);
            if (lsp == null)
            {
                return NotFound();
            }
            return View(lsp);
        }
        public IActionResult DeleteConfirmed(int id)
        {
            var lsp = _db.tbl_DM_Loai_San_Pham.Find(id);
            if (lsp == null)
            {
                return NotFound();
            }
            _db.tbl_DM_Loai_San_Pham.Remove(lsp);
            _db.SaveChanges();
            TempData["success"] = "Xóa Thành Công";
            return RedirectToAction("Index");
        }
    }
}
