using BaiTapThucTap.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaiTapThucTap.Controllers
{
    public class BaiTap4Controller : Controller
    {
        private readonly DBContext _db;
        public BaiTap4Controller(DBContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var listNCC = _db.tbl_DM_NCC.Where(x => x.Id != 1).ToList();
            return View(listNCC);
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(BaiTap4 danhmuc)
        {
            if (string.IsNullOrWhiteSpace(danhmuc.Ma_NCC))
            {
                ModelState.AddModelError("Ma_NCC", "Mã nhà cung cấp không được để trống.");
            }
            else
            {
                var ktrMa = _db.tbl_DM_NCC
                              .FirstOrDefault(d => d.Ma_NCC.Trim().ToLower() == danhmuc.Ma_NCC.Trim().ToLower());

                if (ktrMa != null)
                {
                    ModelState.AddModelError("Ma_NCC", "Mã nhà cung cấp đã tồn tại.");
                }
            }

            if (string.IsNullOrWhiteSpace(danhmuc.Ten_NCC))
            {
                ModelState.AddModelError("Ten_NCC", "Tên nhà cung cấp không được để trống.");
            }
            else
            {
                var ktten = _db.tbl_DM_NCC
                              .FirstOrDefault(d => d.Ten_NCC.Trim().ToLower() == danhmuc.Ten_NCC.Trim().ToLower());

                if (ktten != null)
                {
                    ModelState.AddModelError("Ten_NCC", "Tên nhà cung cấp đã tồn tại.");
                }
            }

            if (ModelState.IsValid)
            {
                _db.tbl_DM_NCC.Add(danhmuc);
                _db.SaveChanges();
                TempData["success"] = "Thêm Thành Công";
                return RedirectToAction("Index");
            }


            return View(danhmuc);
        }

        public IActionResult Update(int id)
        {
            var ncc = _db.tbl_DM_NCC.Find(id);
            if (ncc == null)
            {
                return NotFound();
            }
            return View(ncc);
        }
        [HttpPost]
        public IActionResult Update(BaiTap4 ncc)
        {
            var existingNCC = _db.tbl_DM_NCC.Find(ncc.Id);

            if (string.IsNullOrWhiteSpace(ncc.Ma_NCC))
            {
                ModelState.AddModelError("Ma_NCC", "Mã nhà cung cấp không được để trống.");
            }
            else
            {
                var oldMaNCC = existingNCC.Ma_NCC;

                var ktrMaTrung = _db.tbl_DM_NCC
                                  .FirstOrDefault(d => d.Ma_NCC.Trim().ToLower() == ncc.Ma_NCC.Trim().ToLower());
                if (ncc.Ma_NCC != oldMaNCC)
                {
                    if (ktrMaTrung != null)
                    {
                        ModelState.AddModelError("Ma_NCC", "Mã nhà cung cấp đã tồn tại.");
                    }
                    else
                    {
                        existingNCC.Ma_NCC = ncc.Ma_NCC;
                    }
                }
            }

            if (string.IsNullOrWhiteSpace(ncc.Ten_NCC))
            {
                ModelState.AddModelError("Ten_NCC", "Tên nhà cung cấp không được để trống.");
            }
            else
            {
                var oldTenNCC = existingNCC.Ten_NCC;
                var ktrTenTrung = _db.tbl_DM_NCC
                                  .FirstOrDefault(d => d.Ten_NCC.Trim().ToLower() == ncc.Ten_NCC.Trim().ToLower());
                if (ncc.Ten_NCC != oldTenNCC)
                {
                    if (ktrTenTrung != null)
                    {
                        ModelState.AddModelError("Ten_NCC", "Tên nhà cung cấp đã tồn tại.");
                    }
                    else
                    {
                        existingNCC.Ten_NCC = ncc.Ten_NCC;
                    }
                }
            }
            if (ModelState.IsValid)
            {
                existingNCC.Ten_NCC = ncc.Ten_NCC;
                existingNCC.Ma_NCC = ncc.Ma_NCC;
                existingNCC.Ghi_Chu = ncc.Ghi_Chu;
                _db.SaveChanges();

                TempData["success"] = "Sửa Thành Công";
                return RedirectToAction("Index");
            }
            return View(ncc);
        }
        public IActionResult Delete(int id)
        {
            var ncc = _db.tbl_DM_NCC.FirstOrDefault(x => x.Id == id);
            if (ncc == null)
            {
                return NotFound();
            }
            return View(ncc);
        }
        public IActionResult DeleteConfirmed(int id)
        {
            var ncc = _db.tbl_DM_NCC.Find(id);
            if (ncc == null)
            {
                return NotFound();
            }
            // Cập nhật các sản phẩm có khóa ngoại Don_Vi_Tin_ID trỏ đến đơn vị tính này
            var PNList = _db.tbl_DM_Nhap_Kho.Where(sp => sp.NCC_ID == id).ToList();

            foreach (var x in PNList)
            {
                x.NCC_ID = 1;
            }
            _db.tbl_DM_NCC.Remove(ncc);
            _db.SaveChanges();
            TempData["success"] = "Xóa Thành Công";
            return RedirectToAction("Index");
        }
    }
}
