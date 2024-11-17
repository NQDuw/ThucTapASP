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
    public class BaiTap7Controller : Controller
    {
        private readonly DBContext _db;
        private readonly IHostingEnvironment _hosting;
        private readonly UserManager<ApplicationUser> _userManager;
        public BaiTap7Controller(DBContext db, IHostingEnvironment hosting,UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _hosting = hosting;
            _userManager = userManager;
        }
        public class CombinedViewModel
        {
            public List<BaiTap7> Bai7ViewModel { get; set; }
            public List<ViewModelBai7> ViewModelList { get; set; }
        }
        

        private void LoadViewBag()
        {
            ViewBag.SPList = _db.tbl_DM_San_Pham.Where(x=>x.Id!=1).Select(x => new SelectListItem
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
            var listBai7 = _db.tbl_DM_Nhap_Kho.Include(x => x.Kho).Include(x => x.NCC).ToList();
            LoadViewBag();
            return View(listBai7);
        }
       
        
        public IActionResult AddPhieu()
        {
            LoadViewBag();
            return View();
        }
        [HttpPost]
        public IActionResult AddPhieu(ViewModelBai7 model)
        {
            if (string.IsNullOrWhiteSpace(model.Bai7.So_Phieu_Nhap_Kho))
            {
                ModelState.AddModelError("Bai7.So_Phieu_Nhap_Kho", "Số Phiếu Nhập không được để trống.");
            }
            else
            {
                var ktrMa = _db.tbl_DM_Nhap_Kho
                              .FirstOrDefault(d => d.So_Phieu_Nhap_Kho.Trim().ToLower() == model.Bai7.So_Phieu_Nhap_Kho.Trim().ToLower());

                if (ktrMa != null)
                {
                    ModelState.AddModelError("Bai7.So_Phieu_Nhap_Kho", "Số Phiếu Nhấp đã tồn tại.");
                }
            }
            LoadViewBag();
            if (model.Bai7.Kho_ID <= 0)
            {
                ModelState.AddModelError("Bai7.Kho_ID", "Vui lòng chọn Kho");
            }

            if (model.Bai7.NCC_ID <= 0)
            {
                ModelState.AddModelError("Bai7.NCC_ID", "Vui lòng chọn nhà cung cấp");
            }
            var listKho = _db.tbl_DM_Kho.ToList();

            if (listKho.Find(x => x.Id == model.Bai7.Kho_ID) == null)
            {
                ModelState.AddModelError("Bai7.Kho_ID", "Kho đã bị xóa");
            }
            var listNCC = _db.tbl_DM_NCC.ToList();
            if (listNCC.Find(x => x.Id == model.Bai7.NCC_ID) == null)
            {
                ModelState.AddModelError("Bai7.NCC_ID", "nhà cung cấp đã bị xóa");
            }
            if (ModelState.IsValid)
            {
                var xnk = new BaiTap8
                {
                    So_Phieu_Nhap_Kho = model.Bai7.So_Phieu_Nhap_Kho,
                    Kho_ID = model.Bai7.Kho_ID,
                    NCC_ID = model.Bai7.NCC_ID,
                    Ngay_Nhap_Kho = model.Bai7.Ngay_Nhap_Kho,
                    Ghi_Chu = model.Bai7.Ghi_Chu
                };
                // Thêm đối tượng mới vào DbSet
                _db.tbl_XNK_Nhap_Kho.Add(xnk);
                _db.tbl_DM_Nhap_Kho.Add(model.Bai7);
                _db.SaveChanges();
                _db.SaveChanges();
                TempData["success"] = "Thêm Thành Công";
                return RedirectToAction("Index","BaiTap9", new { bai7Id = model.Bai7.Id });

            }
            return View(model);
        }
        public IActionResult Delete(int Id)
        {
            var nk = _db.tbl_DM_Nhap_Kho.FirstOrDefault(x => x.Id == Id);
            var nkr = _db.tbl_DM_Nhap_Kho_Raw_Data.FirstOrDefault(x => x.Nhap_Kho_ID == Id);
            if (nk == null)
            {
                return NotFound();
            }
            var viewModel = new ViewModelBai7
            {
                Bai7 = nk,
                Bai7_2 = nkr
            };
            return View(viewModel);
        }
        public IActionResult DeleteConfirmed(int Id)
        {
            var nk = _db.tbl_DM_Nhap_Kho.FirstOrDefault(x => x.Id == Id);
            var nkr = _db.tbl_DM_Nhap_Kho_Raw_Data.FirstOrDefault(x => x.Nhap_Kho_ID== Id);
            var bai8 = _db.tbl_XNK_Nhap_Kho.FirstOrDefault(x => x.So_Phieu_Nhap_Kho == nk.So_Phieu_Nhap_Kho);
            if (nk == null)
            {
                return NotFound();
            }
            _db.tbl_DM_Nhap_Kho.Remove(nk);
            if (nkr != null)
            {
                _db.tbl_DM_Nhap_Kho_Raw_Data.Remove(nkr);
            }
            _db.tbl_XNK_Nhap_Kho.Remove(bai8);
            _db.SaveChanges();
            TempData["success"] = "Xóa Thành Công";
            return RedirectToAction("Index");
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
        public IActionResult InPhieu(int? khoId)
        {
            if (khoId.HasValue && khoId.Value > 0)
            {
                var listBai7_2 = _db.tbl_DM_Nhap_Kho_Raw_Data
                    .Include(x => x.sanpham)
                    .Include(x=>x.NhapKho.NCC)
                    .Where(x=>x.NhapKho.Kho_ID == khoId)
                    .ToList();
                LoadViewBag();
                return PartialView("ViewInPhieu", listBai7_2);
            }
            else
            {
                var listBai7_2 = _db.tbl_DM_Nhap_Kho_Raw_Data
                    .Include(x => x.sanpham)
                    .Include(x => x.NhapKho.NCC)
                    .ToList();
                LoadViewBag();
                return PartialView("ViewInPhieu", listBai7_2);
            }
              
        }

    }
}
