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
    public class BaiTap8Controller : Controller
    {
        private readonly DBContext _db;
        private readonly IHostingEnvironment _hosting;
        public BaiTap8Controller(DBContext db, IHostingEnvironment hosting)
        {
            _db = db;
            _hosting = hosting;
        }
        public IActionResult Index()
        {
            var listBai7 = _db.tbl_DM_Nhap_Kho.Include(x => x.Kho).Include(x => x.NCC);
            var listBai7_2 = _db.tbl_DM_Nhap_Kho_Raw_Data.Include(x => x.sanpham);
            var viewModelList = listBai7.Join(listBai7_2,
            bai7 => bai7.Id,
            bai7_2 => bai7_2.Id,
            (bai7, bai7_2) => new BaiTap8
            {
                Bai7 = bai7,
                Bai7_2 = bai7_2,
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

            ViewBag.NCCList = _db.tbl_DM_NCC.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Ten_NCC
            }).ToList();

            ViewBag.KhoList = _db.tbl_DM_Kho.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Ten_Kho
            }).ToList();
        }
        public IActionResult Edit(int bai7Id, int bai7_2Id)
        {
            var nk = _db.tbl_DM_Nhap_Kho.FirstOrDefault(x => x.Id == bai7Id);
            var nkr = _db.tbl_DM_Nhap_Kho_Raw_Data.FirstOrDefault(x => x.Id == bai7_2Id);
            if (nk == null || nkr == null)
            {
                return NotFound();
            }
            var viewModel = new BaiTap8
            {
                Bai7 = nk,
                Bai7_2 = nkr
            };
            LoadViewBag();
            return View(viewModel);
        }
        [HttpPost]

        public IActionResult Edit(BaiTap8 model, int bai7Id, int bai7_2Id)
        {
            var nk = _db.tbl_DM_Nhap_Kho.Find(bai7Id);
            var nkr = _db.tbl_DM_Nhap_Kho_Raw_Data.Find(bai7_2Id);

            if (nk == null || nkr == null)
            {
                return NotFound();
            }

            if (string.IsNullOrWhiteSpace(model.Bai7.So_Phieu_Nhap_Kho))
            {
                ModelState.AddModelError("So_Phieu_Nhap_Kho", "số phiếu nhập không được để trống.");
            }
            else
            {
                var ktrMa = _db.tbl_DM_Nhap_Kho
                              .FirstOrDefault(d => d.So_Phieu_Nhap_Kho.Trim().ToLower() == model.Bai7.So_Phieu_Nhap_Kho.Trim().ToLower());

                if (ktrMa != null)
                {
                    ModelState.AddModelError("So_Phieu_Nhap_Kho", "số phiếu nhập đã tồn tại đã tồn tại.");
                }
            }
            LoadViewBag();
            if (model.Bai7.Kho_ID <= 0)
            {
                ModelState.AddModelError("Kho_ID", "Vui lòng chọn Kho");
            }

            if (model.Bai7.NCC_ID <= 0)
            {
                ModelState.AddModelError("NCC_ID", "Vui lòng chọn nhà cung cấp");
            }
            if (model.Bai7_2.San_Pham_ID <= 0)
            {
                ModelState.AddModelError("San_Pham_ID", "Vui lòng chọn sản phẩm");
            }

           
                nk.So_Phieu_Nhap_Kho = model.Bai7.So_Phieu_Nhap_Kho;
                if (nkr != null)
                {
                    nkr.Nhap_Kho_ID = nk.Id;
                }
                nkr.San_Pham_ID = model.Bai7_2.San_Pham_ID;
                nk.Kho_ID = model.Bai7.Kho_ID;
                nk.NCC_ID = model.Bai7.NCC_ID;
                nk.Ngay_Nhap_Kho = model.Bai7.Ngay_Nhap_Kho;
                nkr.Don_Gia_Nhap = model.Bai7_2.Don_Gia_Nhap;
                nkr.SL_Nhap = model.Bai7_2.SL_Nhap;
                nk.Ghi_Chu = model.Bai7.Ghi_Chu;
                // Tạo đối tượng mới cho tbl_XNK_Nhap_Kho
                var xnk = new BaiTap8
                {
                    So_Phieu_Nhap_Kho = nk.So_Phieu_Nhap_Kho,
                    San_Pham_ID = nkr.San_Pham_ID,
                    Kho_ID = nk.Kho_ID,
                    NCC_ID = nk.NCC_ID,
                    Ngay_Nhap_Kho = nk.Ngay_Nhap_Kho,
                    Don_Gia_Nhap = nkr.Don_Gia_Nhap,
                    SL_Nhap = nkr.SL_Nhap,
                    Ghi_Chu = nk.Ghi_Chu
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
