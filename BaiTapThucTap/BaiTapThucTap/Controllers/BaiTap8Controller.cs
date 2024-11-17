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

            ViewBag.KhoList = _db.tbl_DM_Kho.Where(x=>x.Id!=1).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Ten_Kho
            }).ToList();
        }

        public IActionResult Index()
        {
            var listBai7 = _db.tbl_DM_Nhap_Kho.Include(x => x.Kho).Include(x => x.NCC).ToList();
            LoadViewBag();
            return View(listBai7);
        }
        public IActionResult Edit(int bai7Id)
        {
            var nk = _db.tbl_DM_Nhap_Kho.FirstOrDefault(x => x.Id == bai7Id);
            if (nk == null )
            {
                return NotFound();
            }
            var viewModel = new BaiTap8
            {
                Bai7 = nk
            };
            LoadViewBag();
            return View(viewModel);
        }
        [HttpPost]

        public IActionResult Edit(BaiTap8 model, int bai7Id)
        {
            var nk = _db.tbl_DM_Nhap_Kho.Find(bai7Id);

            if (nk == null )
            {
                return NotFound();
            }

            if (string.IsNullOrWhiteSpace(model.Bai7.So_Phieu_Nhap_Kho))
            {
                ModelState.AddModelError("Bai7.So_Phieu_Nhap_Kho", "Số Phiếu Nhập không được để trống.");
            }
            else
            {
                var oldSoPhieu = nk.So_Phieu_Nhap_Kho;
                var ktrMaTrung = _db.tbl_DM_Nhap_Kho
                             .FirstOrDefault(d => d.So_Phieu_Nhap_Kho.Trim().ToLower() == nk.So_Phieu_Nhap_Kho.Trim().ToLower());
                if (model.Bai7.So_Phieu_Nhap_Kho != oldSoPhieu)
                {
                    if (ktrMaTrung != null)
                    {
                        ModelState.AddModelError("Bai7.So_Phieu_Nhap_Kho", "Số Phiếu Nhập đã tồn tại.");
                    }
                    else
                    {
                        nk.So_Phieu_Nhap_Kho = model.Bai7.So_Phieu_Nhap_Kho;
                    }
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

            var listKho = _db.tbl_DM_Kho.ToList();

            if (listKho.Find(x => x.Id == model.Bai7.Kho_ID) == null)
            {
                ModelState.AddModelError("Bai7.Kho_ID", "Kho đã bị xóa");
            }
            var listdonvitin = _db.tbl_DM_Loai_San_Pham.ToList();
            if (listdonvitin.Find(x => x.Id == model.Bai7.NCC_ID) == null)
            {
                ModelState.AddModelError("Bai7.NCC_ID", "nhà cung cấp đã bị xóa");
            }
            if (ModelState.IsValid)
            {
                nk.So_Phieu_Nhap_Kho = model.Bai7.So_Phieu_Nhap_Kho;
                nk.Kho_ID = model.Bai7.Kho_ID;
                nk.NCC_ID = model.Bai7.NCC_ID;
                nk.Ngay_Nhap_Kho = model.Bai7.Ngay_Nhap_Kho;
                nk.Ghi_Chu = model.Bai7.Ghi_Chu;
                // Tạo đối tượng mới cho tbl_XNK_Nhap_Kho
                var xnk = new BaiTap8
                {
                    So_Phieu_Nhap_Kho = nk.So_Phieu_Nhap_Kho,
                    Kho_ID = nk.Kho_ID,
                    NCC_ID = nk.NCC_ID,
                    Ngay_Nhap_Kho = nk.Ngay_Nhap_Kho,
                    Ghi_Chu = nk.Ghi_Chu
                };
                // Thêm đối tượng mới vào DbSet
                _db.tbl_XNK_Nhap_Kho.Add(xnk);
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
                var listBai7 = _db.tbl_DM_Nhap_Kho
                    .Include(x => x.Kho)
                    .Include(x => x.NCC)
                    .Where(x => x.Kho_ID == khoId)  // Lọc theo Kho_ID
                    .ToList();

                // Trả về partial view chỉ với kho đã chọn
                return PartialView("ListTheoKho", listBai7);
            }
            else
            {
                // Nếu không có khoId, lấy tất cả dữ liệu (toàn bộ kho)
                var listBai7 = _db.tbl_DM_Nhap_Kho
                    .Include(x => x.Kho)
                    .Include(x => x.NCC)
                    .ToList();

                // Trả về partial view với tất cả dữ liệu
                return PartialView("ListTheoKho", listBai7);
            }

        }
    }
    
}
