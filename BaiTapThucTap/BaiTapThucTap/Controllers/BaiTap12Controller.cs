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
    public class BaiTap12Controller : Controller
    {
        private readonly DBContext _db;
        private readonly IHostingEnvironment _hosting;
        public BaiTap12Controller(DBContext db, IHostingEnvironment hosting)
        {
            _db = db;
            _hosting = hosting;
        }
        public IActionResult Index()
        {
            var listBai11 = _db.tbl_DM_Xuat_Kho.Include(x => x.Kho);
            var listBai11_2 = _db.tbl_DM_Xuat_Kho_Raw_Data.Include(x => x.sanpham);
            var viewModelList = listBai11.Join(listBai11_2,
            bai11 => bai11.Id,
            bai11_2 => bai11_2.Id,
            (bai11, bai11_2) => new BaiTapModel12
            {
                Bai11 = bai11,
                Bai11_2 = bai11_2,
            })
        .ToList();
            return View(viewModelList);
        }
        private void LoadViewBag()
        {
            ViewBag.SPList = _db.tbl_DM_San_Pham.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Ten_San_Pham
            }).ToList();

            ViewBag.KhoList = _db.tbl_DM_Kho.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Ten_Kho
            }).ToList();
        }
        public IActionResult Edit(int bai11Id, int bai11_2Id)
        {
            var xk = _db.tbl_DM_Xuat_Kho.FirstOrDefault(x => x.Id == bai11Id);
            var xkr = _db.tbl_DM_Xuat_Kho_Raw_Data.FirstOrDefault(x => x.Id == bai11_2Id);
            if (xk == null || xkr == null)
            {
                return NotFound();
            }
            var viewModel = new BaiTapModel12
            {
                Bai11 = xk,
                Bai11_2 = xkr
            };
            LoadViewBag();
            return View(viewModel);
        }
        [HttpPost]

        public IActionResult Edit(BaiTapModel12 model, int bai11Id, int bai11_2Id)
        {
            var xk = _db.tbl_DM_Xuat_Kho.Find(bai11Id);
            var xkr = _db.tbl_DM_Xuat_Kho_Raw_Data.Find(bai11_2Id);

            if (xk == null || xkr == null)
            {
                return NotFound();
            }

            if (string.IsNullOrWhiteSpace(model.Bai11.So_Phieu_Xuat_Kho))
            {
                ModelState.AddModelError("So_Phieu_Xuat_Kho", "số phiếu xuất không được để trống.");
            }
            else
            {
                var ktrMa = _db.tbl_DM_Xuat_Kho
                              .FirstOrDefault(d => d.So_Phieu_Xuat_Kho.Trim().ToLower() == model.Bai11.So_Phieu_Xuat_Kho.Trim().ToLower());

                if (ktrMa != null)
                {
                    ModelState.AddModelError("So_Phieu_Xuat_Kho", "số phiếu xuất đã tồn tại đã tồn tại.");
                }
            }
            LoadViewBag();
            if (model.Bai11.Kho_ID <= 0)
            {
                ModelState.AddModelError("Kho_ID", "Vui lòng chọn Kho");
            }

            if (model.Bai11_2.San_Pham_ID <= 0)
            {
                ModelState.AddModelError("San_Pham_ID", "Vui lòng chọn Sản phẩm");
            }


                xk.So_Phieu_Xuat_Kho = model.Bai11.So_Phieu_Xuat_Kho;
                if (xkr != null)
                {
                xkr.Xuat_Kho_ID = xk.Id;
                }
                xkr.San_Pham_ID = model.Bai11_2.San_Pham_ID;
                xk.Kho_ID = model.Bai11.Kho_ID;
                xk.Ngay_Xuat_Kho = model.Bai11.Ngay_Xuat_Kho;
                xkr.Don_Gia_Xuat = model.Bai11_2.Don_Gia_Xuat;
                xkr.SL_Xuat = model.Bai11_2.SL_Xuat;
                xk.Ghi_Chu = model.Bai11.Ghi_Chu;
                // Tạo đối tượng mới cho tbl_XNK_Nhap_Kho
                var xnk = new BaiTap8
                {
                    So_Phieu_Nhap_Kho = xk.So_Phieu_Xuat_Kho,
                    San_Pham_ID = xkr.San_Pham_ID,
                    Kho_ID = xk.Kho_ID,
                    Ngay_Nhap_Kho = xk.Ngay_Xuat_Kho,
                    Don_Gia_Nhap = xkr.Don_Gia_Xuat,
                    SL_Nhap = xkr.SL_Xuat,
                    Ghi_Chu = xk.Ghi_Chu
                };
                // Thêm đối tượng mới vào DbSet
                _db.tbl_XNK_Nhap_Kho.Add(xnk);
                _db.SaveChanges();
                TempData["success"] = "Sửa Thành Công";
                return RedirectToAction("Index");
            TempData["success"] = $"{ModelState.IsValid}";
            return View(model);
        }
    }
}
