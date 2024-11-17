using BaiTapThucTap.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaiTapThucTap.Controllers
{
    public class BaiTap1Controller : Controller
    {
        private readonly DBContext _db;
        public BaiTap1Controller(DBContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var listDVT = _db.tbl_DM_Don_Vi_Tinh.Where(x=>x.Id!=1).ToList();
            return View(listDVT);
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(BaiTap1 danhmuc)
        {
            if (string.IsNullOrWhiteSpace(danhmuc.Ten_Don_Vi_Tinh))
            {
                ModelState.AddModelError("Ten_Don_Vi_Tinh", "Tên Đơn Vị Tính không được để trống.");
            }
            else
            {
                var ktrMa = _db.tbl_DM_Don_Vi_Tinh
                              .FirstOrDefault(d => d.Ten_Don_Vi_Tinh.Trim().ToLower() == danhmuc.Ten_Don_Vi_Tinh.Trim().ToLower());

                if (ktrMa != null)
                {
                    ModelState.AddModelError("Ten_Don_Vi_Tinh", "Tên Đơn Vị Tính đã tồn tại.");
                }
            }

            if (ModelState.IsValid)
            {
                _db.tbl_DM_Don_Vi_Tinh.Add(danhmuc);
                _db.SaveChanges();
                TempData["success"] = "Thêm Thành Công";
                return RedirectToAction("Index");
            }
            return View(danhmuc);
        }

        public IActionResult Update(int id)
        {
            var dvt = _db.tbl_DM_Don_Vi_Tinh.Find(id);
            if (dvt == null)
            {
                return NotFound();
            }
            return View(dvt);
        }
        [HttpPost]
        public IActionResult Update(BaiTap1 dvt)
        {
            var existingKho = _db.tbl_DM_Don_Vi_Tinh.Find(dvt.Id);

            if (string.IsNullOrWhiteSpace(dvt.Ten_Don_Vi_Tinh))
            {
                ModelState.AddModelError("Ten_Don_Vi_Tinh", "Tên Đơn Vị Tính không được để trống.");
            }
            else
            {
                var oldTen = existingKho.Ten_Don_Vi_Tinh;
                var ktrMaTrung = _db.tbl_DM_Don_Vi_Tinh
                             .FirstOrDefault(d => d.Ten_Don_Vi_Tinh.Trim().ToLower() == dvt.Ten_Don_Vi_Tinh.Trim().ToLower());
                if (dvt.Ten_Don_Vi_Tinh != oldTen)
                {
                    if (ktrMaTrung != null)
                    {
                        ModelState.AddModelError("Ten_Don_Vi_Tinh", "Tên đơn vị tính đã tồn tại.");
                    }
                    else
                    {
                        existingKho.Ten_Don_Vi_Tinh = dvt.Ten_Don_Vi_Tinh;
                    }
                }
            }
            if (ModelState.IsValid)
            {
                existingKho.Ten_Don_Vi_Tinh = dvt.Ten_Don_Vi_Tinh;
                existingKho.Ghi_Chu = dvt.Ghi_Chu;
                _db.SaveChanges();
                TempData["success"] = "Sửa Thành Công";
                return RedirectToAction("Index");
            }
            return View(dvt);
        }
        public IActionResult Delete(int id)
        {
            var dvt = _db.tbl_DM_Don_Vi_Tinh.FirstOrDefault(x => x.Id == id);
            if (dvt == null)
            {
                return NotFound();
            }
            return View(dvt);
        }
        public IActionResult DeleteConfirmed(int id)
        {
            var dvt = _db.tbl_DM_Don_Vi_Tinh.Find(id);
           
            if (dvt == null)
            {
                return NotFound();
            }
            // Cập nhật các sản phẩm có khóa ngoại Don_Vi_Tin_ID trỏ đến đơn vị tính này
            var sanPhamList = _db.tbl_DM_San_Pham.Where(sp => sp.Don_Vi_Tin_ID == id).ToList();

            foreach (var sp in sanPhamList)
            {
                sp.Don_Vi_Tin_ID = 1; 
            }

            // Lưu thay đổi vào cơ sở dữ liệu
            _db.SaveChanges();
            _db.tbl_DM_Don_Vi_Tinh.Remove(dvt);
            _db.SaveChanges();
            TempData["success"] = "Xóa Thành Công";
            return RedirectToAction("Index");
        }
    }
}
