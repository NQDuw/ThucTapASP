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
    public class BaiTap13Controller : Controller
    {
        private readonly DBContext _db;
        private readonly IHostingEnvironment _hosting;
        private readonly UserManager<ApplicationUser> _userManager;
        public BaiTap13Controller(DBContext db, IHostingEnvironment hosting, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _hosting = hosting;
            _userManager = userManager;
        }
        public class CombinedViewModel
        {
            public BaiTapModel13 BaiTap13Model { get; set; }
            public List<BaiTap11> ViewModelList { get; set; }
        }
        public IActionResult Index(BaiTap11 model, int bai11Id)
        {
            var nk = _db.tbl_DM_Xuat_Kho.Include(x => x.Kho).FirstOrDefault(x => x.Id == bai11Id);
            var nkr = _db.tbl_DM_Xuat_Kho_Raw_Data.Include(x => x.sanpham).Where(x => x.Xuat_Kho_ID == bai11Id).ToList();
            if (nk == null)
            {
                return NotFound();
            }
            var viewModel = new BaiTapModel13
            {
                Bai11 = nk,
                Bai11_2 = nkr,
                TriGia = nkr?.Sum(x => x.SL_Xuat * x.Don_Gia_Xuat) ?? 0
            };
            var listBai11 = _db.tbl_DM_Xuat_Kho.Include(x => x.Kho).ToList();
            LoadViewBag();
            var combinedViewModel = new CombinedViewModel
            {
                BaiTap13Model = viewModel,
                ViewModelList = listBai11
            };
            return View(combinedViewModel);
        }
        private void LoadViewBag()
        {
            ViewBag.SPList = _db.tbl_DM_San_Pham.Where(x => x.Id != 1).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Ten_San_Pham
            }).ToList() ?? new List<SelectListItem>(); // Nếu không có sản phẩm, đảm bảo trả về danh sách rỗng

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
        public IActionResult Add(int bai11Id)
        {
            ViewBag.Bai11Id = bai11Id;
            var nk = _db.tbl_DM_Xuat_Kho.FirstOrDefault(x => x.Id == bai11Id);
            var nkr = _db.tbl_DM_Xuat_Kho_Raw_Data;
            if (nk == null)
            {
                return NotFound();
            }
            var viewModel = new ViewModelBai11
            {
                Bai11 = nk
            };
            LoadViewBag();
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Add(ViewModelBai11 model, int bai11Id)
        {
            var spnk = _db.tbl_DM_Nhap_Kho_Raw_Data
                .Where(x => x.San_Pham_ID == model.Bai11_2.San_Pham_ID)
                .Sum(x => x.SL_Nhap);
            var spxk = _db.tbl_DM_Xuat_Kho_Raw_Data
                .Where(x => x.San_Pham_ID == model.Bai11_2.San_Pham_ID)
                .Sum(x => x.SL_Xuat);

            // Kiểm tra số lượng và đơn giá không âm
            if (model.Bai11_2.SL_Xuat < 0)
            {
                ModelState.AddModelError("Bai11_2.SL_Xuat", "Số lượng không được âm.");
            }
            if (model.Bai11_2.Don_Gia_Xuat  < 0)
            {
                ModelState.AddModelError("Bai11_2.Don_Gia_Xuat", "Đơn giá không được âm.");
            }
            if (model.Bai11_2.SL_Xuat  > spnk - spxk)
            {
                ModelState.AddModelError("Bai11_2.SL_Xuat", $"Số lượng xuất lớn hơn số lượng tồn kho! Tồn Kho Hiện có: {spnk - spxk}");
            }
            var listSP = _db.tbl_DM_San_Pham.ToList();

            if (listSP.Find(x => x.Id == model.Bai11_2.San_Pham_ID) == null)
            {
                ModelState.AddModelError("Bai11_2.San_Pham_ID", "sản phẩm đã bị xóa");
            }
            if (ModelState.IsValid)
            {
                var bai11_2 = new BaiTapModel11_2
                {
                    San_Pham_ID = model.Bai11_2.San_Pham_ID,
                    SL_Xuat = model.Bai11_2.SL_Xuat,
                    Don_Gia_Xuat = model.Bai11_2.Don_Gia_Xuat,
                    Xuat_Kho_ID = bai11Id
                };
                _db.tbl_DM_Xuat_Kho_Raw_Data.Add(bai11_2);
                _db.SaveChanges();
                TempData["success"] = "Thêm Thành Công";
                return RedirectToAction("Index", new { bai11Id = bai11Id });

            }
            LoadViewBag();
            // Truyền lại bai11Id khi có lỗi
            ViewBag.Bai11Id = bai11Id;
            return View(model);
        }

        public IActionResult Edit(int idSP, int bai11Id,int SLBD)
        {
            var nkr = _db.tbl_DM_Xuat_Kho_Raw_Data.Include(x => x.sanpham).FirstOrDefault(x => x.Id == idSP);
           
            LoadViewBag();
            if (nkr == null)
            {
                return NotFound();
            }
            var viewModel = new ViewModelBai11
            {
                Bai11_2 = nkr
            };
            ViewBag.Bai11Id = bai11Id;
            ViewBag.idSP = idSP;
            ViewBag.SLBD = SLBD;
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(ViewModelBai11 model, int idSP, int bai11Id, int SLBD)
        {
            var nkr = _db.tbl_DM_Xuat_Kho_Raw_Data.Include(x => x.sanpham).FirstOrDefault(x => x.Id == idSP);
            var spnk = _db.tbl_DM_Nhap_Kho_Raw_Data
              .Where(x => x.San_Pham_ID == model.Bai11_2.San_Pham_ID)
              .Sum(x => x.SL_Nhap);
            var spxk = _db.tbl_DM_Xuat_Kho_Raw_Data
                .Where(x => x.San_Pham_ID == model.Bai11_2.San_Pham_ID)
                .Sum(x => x.SL_Xuat);
            if (nkr == null)
            {
                return NotFound();
            }
            if (model.Bai11_2.San_Pham_ID <= 0)
            {
                ModelState.AddModelError("Bai11_2.San_Pham_ID", "Chọn Sản phẩm");
            }

            if (model.Bai11_2.Don_Gia_Xuat < 0)
            {
                ModelState.AddModelError("Bai11_2.Don_Gia_Xuat", "đơn giá không được âm.");
                if (model.Bai11_2.SL_Xuat < 0)
                {
                    ModelState.AddModelError("Bai11_2.SL_Xuat", "Số lượng không được âm.");
                    ViewBag.Bai11Id = bai11Id;
                     ViewBag.SLBD = SLBD;
                    ViewBag.idSP = idSP;
                    LoadViewBag();
                    return View(model);
                }
            }
            else
            {
                if (model.Bai11_2.SL_Xuat < 0)
                {
                    ModelState.AddModelError("Bai11_2.SL_Xuat", "Số lượng không được âm.");
                    ViewBag.Bai11Id = bai11Id;
                    ViewBag.SLBD = SLBD;
                    ViewBag.idSP = idSP;
                    LoadViewBag();
                    return View(model);
                }
            }
           
            if (model.Bai11_2.SL_Xuat  > spnk - spxk + SLBD)
            {
                ModelState.AddModelError("Bai11_2.SL_Xuat", $"Số lượng xuất lớn hơn số lượng tồn kho! Tồn Kho Hiện có:  {spnk - spxk + SLBD}");
                ViewBag.Bai11Id = bai11Id;
                ViewBag.SLBD = SLBD;
                ViewBag.idSP = idSP;
                LoadViewBag();
                return View(model);
            }
            var listSP = _db.tbl_DM_San_Pham.ToList();

            if (listSP.Find(x => x.Id == model.Bai11_2.San_Pham_ID) == null)
            {
                ModelState.AddModelError("Bai11_2.San_Pham_ID", "sản phẩm đã bị xóa");
                ViewBag.Bai11Id = bai11Id;
                ViewBag.SLBD = SLBD;
                ViewBag.idSP = idSP;
                LoadViewBag();
                return View(model);
            }
            nkr.San_Pham_ID = model.Bai11_2.San_Pham_ID;
            nkr.SL_Xuat = model.Bai11_2.SL_Xuat;
            nkr.Don_Gia_Xuat = model.Bai11_2.Don_Gia_Xuat;
            _db.SaveChanges();
            TempData["success"] = "Sửa Thành Công";
            return RedirectToAction("Index", new { bai11Id = bai11Id });
        }
        //Hiển thị form xác nhận xóa chủng loại
        public IActionResult Delete(int IdSP, int bai11Id)
        {
            var sp = _db.tbl_DM_Xuat_Kho_Raw_Data.Include(x => x.sanpham).FirstOrDefault(x => x.Id == IdSP);
            if (sp == null)
            {
                return NotFound();
            }
            ViewBag.Bai11Id = bai11Id;
            ViewBag.idSP = IdSP;

            return View(sp);
        }
        // Xử lý xóa chủng loại
        public IActionResult DeleteConfirmed(int IdSP, int bai11Id)
        {
            var sp = _db.tbl_DM_Xuat_Kho_Raw_Data.FirstOrDefault(x => x.Id == IdSP);
            if (sp == null)
            {
                return NotFound();
            }
            _db.tbl_DM_Xuat_Kho_Raw_Data.Remove(sp);
            ViewBag.Bai11Id = bai11Id;
            _db.SaveChanges();
            TempData["success"] = "Xóa Thành Công";
            return RedirectToAction("Index", new { bai11Id = bai11Id });
        }
    }
}
