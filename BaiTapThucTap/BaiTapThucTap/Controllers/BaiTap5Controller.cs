using BaiTapThucTap.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaiTapThucTap.Controllers
{
    public class BaiTap5Controller : Controller
    {
        private readonly DBContext _db;
        public BaiTap5Controller(DBContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var listKho = _db.tbl_DM_Kho.Where(x => x.Id != 1).ToList();
            return View(listKho);
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(BaiTap5 danhmuc)
        {
            if (string.IsNullOrWhiteSpace(danhmuc.Ten_Kho))
            {
                ModelState.AddModelError("Ten_Kho", "Tên Kho không được để trống.");
            }
            else
            {
                var ktten = _db.tbl_DM_Kho
                              .FirstOrDefault(d => d.Ten_Kho.Trim().ToLower() == danhmuc.Ten_Kho.Trim().ToLower());

                if (ktten != null)
                {
                    ModelState.AddModelError("Ten_Kho", "Tên Kho đã tồn tại.");
                }
            }

            if (ModelState.IsValid)
            {
                _db.tbl_DM_Kho.Add(danhmuc);
                _db.SaveChanges();
                TempData["success"] = "Thêm Thành Công";
                return RedirectToAction("Index");
            }


            return View(danhmuc);
        }

        public IActionResult Update(int id)
        {
            var Kho = _db.tbl_DM_Kho.Find(id);
            if (Kho == null)
            {
                return NotFound();
            }
            return View(Kho);
        }
        [HttpPost]
        public IActionResult Update(BaiTap5 Kho)
        {
            var existingKho = _db.tbl_DM_Kho.Find(Kho.Id);

            if (string.IsNullOrWhiteSpace(Kho.Ten_Kho))
            {
                ModelState.AddModelError("Ten_Kho", "Tên Kho không được để trống.");
            }
            else
            {
                var oldTen = existingKho.Ten_Kho;
                var ktrTenTrung = _db.tbl_DM_Kho
                             .FirstOrDefault(d => d.Ten_Kho.Trim().ToLower() == Kho.Ten_Kho.Trim().ToLower());
                if (Kho.Ten_Kho != oldTen)
                {
                    if (ktrTenTrung != null)
                    {
                        ModelState.AddModelError("Ten_Kho", "Tên Kho đã tồn tại.");
                    }
                    else
                    {
                        existingKho.Ten_Kho = Kho.Ten_Kho;
                    }
                }
            }
            if (ModelState.IsValid)
            {
                existingKho.Ten_Kho = Kho.Ten_Kho;
                existingKho.Ghi_Chu = Kho.Ghi_Chu;
                _db.SaveChanges();
                TempData["success"] = "Sửa Thành Công";
                return RedirectToAction("Index");
            }
            return View(Kho);
        }
        public IActionResult Delete(int id)
        {
            var kho = _db.tbl_DM_Kho.FirstOrDefault(x => x.Id == id);
            if (kho == null)
            {
                return NotFound();
            }
            return View(kho);
        }
        public IActionResult DeleteConfirmed(int id)
        {
            var kho = _db.tbl_DM_Kho.Find(id);
            if (kho == null)
            {
                return NotFound();
            }

            var PNList = _db.tbl_DM_Nhap_Kho.Where(sp => sp.Kho_ID == id).ToList();

            foreach (var x in PNList)
            {
                x.Kho_ID = 1;
            }

            var PXList = _db.tbl_DM_Xuat_Kho.Where(sp => sp.Kho_ID == id).ToList();

            foreach (var x in PXList)
            {
                x.Kho_ID = 1;
            }
            var UserList = _db.tbl_DM_Kho_User.Where(sp => sp.Kho_ID == id).ToList();

            foreach (var x in UserList)
            {
                x.Kho_ID = 1;
            }
            _db.tbl_DM_Kho.Remove(kho);
            _db.SaveChanges();
            TempData["success"] = "Xóa Thành Công";
            return RedirectToAction("Index");
        }
    }
}
