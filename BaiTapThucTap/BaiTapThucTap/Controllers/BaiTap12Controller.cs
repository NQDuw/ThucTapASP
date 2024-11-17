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
        private void LoadViewBag()
        {
            ViewBag.SPList = _db.tbl_DM_San_Pham.Where(x => x.Id != 1).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Ten_San_Pham
            }).ToList();

            ViewBag.NCCList = _db.tbl_DM_NCC.Where(x => x.Id != 1).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Ten_NCC
            }).ToList();

            ViewBag.KhoList = _db.tbl_DM_Kho.Where(x => x.Id != 1).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Ten_Kho
            }).ToList();
        }

        public IActionResult Index()
        {
            var listBai11 = _db.tbl_DM_Xuat_Kho.Include(x => x.Kho).ToList();
            LoadViewBag();
            return View(listBai11);
        }
        public IActionResult Edit(int bai11Id)
        {
            var nk = _db.tbl_DM_Xuat_Kho.FirstOrDefault(x => x.Id == bai11Id);
            if (nk == null)
            {
                return NotFound();
            }
            var viewModel = new BaiTapModel12
            {
                Bai11 = nk
            };
            LoadViewBag();
            return View(viewModel);
        }
        [HttpPost]

        public IActionResult Edit(BaiTapModel12 model, int bai11Id)
        {
            var nk = _db.tbl_DM_Xuat_Kho.Find(bai11Id);

            if (nk == null)
            {
                return NotFound();
            }

            if (string.IsNullOrWhiteSpace(model.Bai11.So_Phieu_Xuat_Kho))
            {
                ModelState.AddModelError("Bai11.So_Phieu_Xuat_Kho", "Số Phiếu xuất không được để trống.");
            }
            else
            {
                var oldSoPhieu = nk.So_Phieu_Xuat_Kho;
                var ktrMaTrung = _db.tbl_DM_Xuat_Kho
                             .FirstOrDefault(d => d.So_Phieu_Xuat_Kho.Trim().ToLower() == nk.So_Phieu_Xuat_Kho.Trim().ToLower());
                if (model.Bai11.So_Phieu_Xuat_Kho != oldSoPhieu)
                {
                    if (ktrMaTrung != null)
                    {
                        ModelState.AddModelError("Bai11.So_Phieu_Xuat_Kho", "Số Phiếu xuất đã tồn tại.");
                    }
                    else
                    {
                        nk.So_Phieu_Xuat_Kho = model.Bai11.So_Phieu_Xuat_Kho;
                    }
                }
            }

            LoadViewBag();
            if (model.Bai11.Kho_ID <= 0)
            {
                ModelState.AddModelError("Kho_ID", "Vui lòng chọn Kho");
            }
            var listKho = _db.tbl_DM_Kho.ToList();

            if (listKho.Find(x => x.Id == model.Bai11.Kho_ID) == null)
            {
                ModelState.AddModelError("Bai11.Kho_ID", "Kho đã bị xóa");
            }
            
            if (ModelState.IsValid)
            {

                nk.So_Phieu_Xuat_Kho = model.Bai11.So_Phieu_Xuat_Kho;
                nk.Kho_ID = model.Bai11.Kho_ID;
                nk.Ngay_Xuat_Kho = model.Bai11.Ngay_Xuat_Kho;
                nk.Ghi_Chu = model.Bai11.Ghi_Chu;
                // Tạo đối tượng mới cho tbl_XNK_Nhap_Kho
                var xnk = new BaiTapModel12
                {
                    So_Phieu_Xuat_Kho = nk.So_Phieu_Xuat_Kho,
                    Kho_ID = nk.Kho_ID ?? 0,
                    Ngay_Xuat_Kho = nk.Ngay_Xuat_Kho,
                    Ghi_Chu = nk.Ghi_Chu
                };
                // Thêm đối tượng mới vào DbSet
                _db.tbl_XNK_Xuat_Kho.Add(xnk);
                _db.SaveChanges();
                TempData["success"] = "Sửa Thành Công";
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult GetListKhoByKho(int? khoId)
        {
            if (khoId.HasValue && khoId.Value > 0)
            {
                // Nếu có khoId, lọc dữ liệu theo kho
                var listBai11 = _db.tbl_DM_Xuat_Kho
                    .Include(x => x.Kho)
                    .Where(x => x.Kho_ID == khoId)  // Lọc theo Kho_ID
                    .ToList();

                // Trả về partial view chỉ với kho đã chọn
                return PartialView("ListTheoKho", listBai11);
            }
            else
            {
                // Nếu không có khoId, lấy tất cả dữ liệu (toàn bộ kho)
                var listBai11 = _db.tbl_DM_Xuat_Kho
                    .Include(x => x.Kho)
                    .ToList();

                // Trả về partial view với tất cả dữ liệu
                return PartialView("ListTheoKho", listBai11);
            }

        }
    }

}
