using BaiTapThucTap.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaiTapThucTap.Controllers
{
    public class BaiTap11Controller : Controller
    {
        private readonly DBContext _db;
        private readonly IHostingEnvironment _hosting;
        private readonly UserManager<ApplicationUser> _userManager;
        public BaiTap11Controller(DBContext db, IHostingEnvironment hosting, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _hosting = hosting;
            _userManager = userManager;
        }
        public class CombinedViewModel
        {
            public List<BaiTap11> Bai11ViewModel { get; set; }
            public List<ViewModelBai11> ViewModelList { get; set; }
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


        public IActionResult AddPhieu()
        {
            LoadViewBag();
            return View();
        }
        [HttpPost]
        public IActionResult AddPhieu(ViewModelBai11 model)
        {
            if (string.IsNullOrWhiteSpace(model.Bai11.So_Phieu_Xuat_Kho))
            {
                ModelState.AddModelError("Bai11.So_Phieu_Xuat_Kho", "Số Phiếu xuất không được để trống.");
            }
            else
            {
                var ktrMa = _db.tbl_DM_Xuat_Kho
                              .FirstOrDefault(d => d.So_Phieu_Xuat_Kho.Trim().ToLower() == model.Bai11.So_Phieu_Xuat_Kho.Trim().ToLower());

                if (ktrMa != null)
                {
                    ModelState.AddModelError("Bai11.So_Phieu_Xuat_Kho", "Số Phiếu xuất đã tồn tại.");
                }
            }
            LoadViewBag();
            if (model.Bai11.Kho_ID <= 0)
            {
                ModelState.AddModelError("Bai11.Kho_ID", "Vui lòng chọn Kho");
            }
            var listKho = _db.tbl_DM_Kho.ToList();

            if (listKho.Find(x => x.Id == model.Bai11.Kho_ID) == null)
            {
                ModelState.AddModelError("Bai11.Kho_ID", "Kho đã bị xóa");
            }
            
            if (ModelState.IsValid)
            {
                var xnk = new BaiTapModel12
                {
                    So_Phieu_Xuat_Kho = model.Bai11.So_Phieu_Xuat_Kho,
                    Kho_ID = model.Bai11.Kho_ID ?? 0,
                    Ngay_Xuat_Kho = model.Bai11.Ngay_Xuat_Kho,
                    Ghi_Chu = model.Bai11.Ghi_Chu
                };
                // Thêm đối tượng mới vào DbSet
                _db.tbl_XNK_Xuat_Kho.Add(xnk);
                _db.tbl_DM_Xuat_Kho.Add(model.Bai11);
                _db.SaveChanges();
                TempData["success"] = "Thêm Thành Công";
                return RedirectToAction("Index", "BaiTap13", new { bai11Id = model.Bai11.Id });

            }
            return View(model);
        }
        public IActionResult Delete(int Id)
        {
            var nk = _db.tbl_DM_Xuat_Kho.FirstOrDefault(x => x.Id == Id);
            var nkr = _db.tbl_DM_Xuat_Kho_Raw_Data.FirstOrDefault(x => x.Xuat_Kho_ID == Id);
            if (nk == null)
            {
                return NotFound();
            }
            var viewModel = new ViewModelBai11
            {
                Bai11 = nk,
                Bai11_2 = nkr
            };
            return View(viewModel);
        }
        public IActionResult DeleteConfirmed(int Id)
        {
            var nk = _db.tbl_DM_Xuat_Kho.FirstOrDefault(x => x.Id == Id);
            var nkr = _db.tbl_DM_Xuat_Kho_Raw_Data.FirstOrDefault(x => x.Xuat_Kho_ID == Id);
            var bai12 = _db.tbl_XNK_Xuat_Kho.FirstOrDefault(x => x.So_Phieu_Xuat_Kho == nk.So_Phieu_Xuat_Kho);
            if (nk == null)
            {
                return NotFound();
            }
            _db.tbl_DM_Xuat_Kho.Remove(nk);
            if (nkr != null)
            {
                _db.tbl_DM_Xuat_Kho_Raw_Data.Remove(nkr);
            }
            _db.tbl_XNK_Xuat_Kho.Remove(bai12);
            _db.SaveChanges();
            TempData["success"] = "Xóa Thành Công";
            return RedirectToAction("Index");
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
        public IActionResult InPhieu(int? khoId)
        {
            if (khoId.HasValue && khoId.Value > 0)
            {
                var listBai11_2 = _db.tbl_DM_Xuat_Kho_Raw_Data
                    .Include(x => x.sanpham)
                    .Include(x=>x.XuatKho.Kho)
                    .Where(x => x.XuatKho.Kho_ID == khoId)
                    .ToList();
                LoadViewBag();
                return PartialView("ViewInPhieu", listBai11_2);
            }
            else
            {
                var listBai11_2 = _db.tbl_DM_Xuat_Kho_Raw_Data
                    .Include(x => x.sanpham)
                    .Include(x => x.XuatKho.Kho)
                    .ToList();
                LoadViewBag();
                return PartialView("ViewInPhieu", listBai11_2);
            }

        }
    }

}

