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
    public class BaiTap11Controller : Controller
    {
        private readonly DBContext _db;
        private readonly IHostingEnvironment _hosting;
        public BaiTap11Controller(DBContext db, IHostingEnvironment hosting)
        {
            _db = db;
            _hosting = hosting;
        }
        public class CombinedViewModel
        {
            public ViewModelBai11 Bai11ViewModel { get; set; }
            public List<ViewModelBai11> ViewModelList { get; set; }
        }
        public IActionResult Index()
        {

            var listBai11 = _db.tbl_DM_Xuat_Kho.Include(x => x.Kho);
            var listBai11_2 = _db.tbl_DM_Xuat_Kho_Raw_Data.Include(x => x.sanpham);
            var viewModelList = listBai11.Join(listBai11_2,
            bai11 => bai11.Id,
            bai11_2 => bai11_2.Id,
            (bai11, bai11_2) => new ViewModelBai11
            {
                Bai11 = bai11,
                Bai11_2 = bai11_2,
                TriGia = (decimal)bai11_2.Don_Gia_Xuat * (decimal)bai11_2.SL_Xuat
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

        public IActionResult Add()
        {
            LoadViewBag();
            return View();
        }
        [HttpPost]
        public IActionResult Add(ViewModelBai11 model)
        {
            if (string.IsNullOrWhiteSpace(model.Bai11.So_Phieu_Xuat_Kho))
            {
                ModelState.AddModelError("So_Phieu_Xuat_Kho", "Số Phiếu xuát không được để trống.");
            }
            else
            {
                var ktrMa = _db.tbl_DM_Xuat_Kho
                              .FirstOrDefault(d => d.So_Phieu_Xuat_Kho.Trim().ToLower() == model.Bai11.So_Phieu_Xuat_Kho.Trim().ToLower());

                if (ktrMa != null)
                {
                    ModelState.AddModelError("So_Phieu_Xuat_Kho", "Số Phiếu xuất đã tồn tại.");
                }
            }
            LoadViewBag();
            if (model.Bai11.Kho_ID <= 0)
            {
                ModelState.AddModelError("Kho_ID", "Vui lòng chọn Kho");
            }
            if (model.Bai11_2.San_Pham_ID <= 0)
            {
                ModelState.AddModelError("NCC_ID", "Vui lòng chọn nhà cung cấp");
            }
            if (ModelState.IsValid)
            {
                _db.tbl_DM_Xuat_Kho.Add(model.Bai11);
                _db.SaveChanges();
                if (model.Bai11.Id > 0)
                {
                    model.Bai11_2.Xuat_Kho_ID = model.Bai11.Id;
                    _db.tbl_DM_Xuat_Kho_Raw_Data.Add(model.Bai11_2);
                    _db.SaveChanges();
                    TempData["success"] = "Thêm Thành Công";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Không thể lấy Id từ Bai11.");
                }
            }
            return View(model);
        }
        public IActionResult Delete(int bai11Id, int bai11_2Id)
        {
            var xk = _db.tbl_DM_Xuat_Kho.FirstOrDefault(x => x.Id == bai11Id);
            var xkr = _db.tbl_DM_Xuat_Kho_Raw_Data.FirstOrDefault(x => x.Id == bai11_2Id);
            if (xk == null || xkr == null)
            {
                return NotFound();
            }
            var viewModel = new ViewModelBai11
            {
                Bai11 = xk,
                Bai11_2 = xkr
            };
            return View(viewModel);
        }
        public IActionResult DeleteConfirmed(int bai11Id, int bai11_2Id)
        {
            var xk = _db.tbl_DM_Xuat_Kho.FirstOrDefault(x => x.Id == bai11Id);
            var xkr = _db.tbl_DM_Xuat_Kho_Raw_Data.FirstOrDefault(x => x.Id == bai11_2Id);
            if (xk == null || xkr == null)
            {
                return NotFound();
            }
            _db.tbl_DM_Xuat_Kho.Remove(xk);
            _db.tbl_DM_Xuat_Kho_Raw_Data.Remove(xkr);
            _db.SaveChanges();
            TempData["success"] = "Xóa Thành Công";
            return RedirectToAction("Index");
        }
    }
}
