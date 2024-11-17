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
    public class BaiTap9Controller : Controller
    {
        private readonly DBContext _db;
        private readonly IHostingEnvironment _hosting;
        private readonly UserManager<ApplicationUser> _userManager;
        public BaiTap9Controller(DBContext db, IHostingEnvironment hosting, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _hosting = hosting;
            _userManager = userManager;
        }
        public class CombinedViewModel
        {
            public BaiTap9 BaiTap9Model { get; set; }
            public List<BaiTap7> ViewModelList { get; set; }
        }
        public IActionResult Index(BaiTap9 model, int bai7Id)
        {
            var nk = _db.tbl_DM_Nhap_Kho.Include(x=>x.Kho).Include(x=>x.NCC).FirstOrDefault(x => x.Id == bai7Id);
            var nkr = _db.tbl_DM_Nhap_Kho_Raw_Data.Include(x=>x.sanpham).Where(x => x.Nhap_Kho_ID == bai7Id).ToList();
            if (nk == null)
            {
                return NotFound();
            }
            var viewModel = new BaiTap9
            {
                Bai7 = nk,
                Bai7_2 = nkr,
                TriGia = nkr?.Sum(x => x.SL_Nhap * x.Don_Gia_Nhap) ?? 0
            };
            var listBai7 = _db.tbl_DM_Nhap_Kho.Include(x => x.Kho).Include(x => x.NCC).ToList();
            LoadViewBag();
            var combinedViewModel = new CombinedViewModel
            {
                BaiTap9Model = viewModel,
                ViewModelList = listBai7
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
        public IActionResult Add(int bai7Id)
        {
            ViewBag.Bai7Id = bai7Id;
            var nk = _db.tbl_DM_Nhap_Kho.FirstOrDefault(x => x.Id == bai7Id);
            var nkr = _db.tbl_DM_Nhap_Kho_Raw_Data;
            if (nk == null)
            {
                return NotFound();
            }
            var viewModel = new ViewModelBai7
            {
                Bai7 = nk
            };
            LoadViewBag();
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Add(ViewModelBai7 model, int bai7Id)
        {
            var nkr = _db.tbl_DM_Nhap_Kho_Raw_Data.FirstOrDefault(x=>x.Nhap_Kho_ID == bai7Id);
                // Kiểm tra số lượng và đơn giá không âm
                if (model.Bai7_2.SL_Nhap < 0)
                {
                    ModelState.AddModelError("Bai7_2.SL_Nhap", "Số lượng không được âm.");
                }

                if (model.Bai7_2.Don_Gia_Nhap < 0)
                {
                    ModelState.AddModelError("Bai7_2.Don_Gia_Nhap", "Đơn giá không được âm.");
                }
                var listSP = _db.tbl_DM_San_Pham.ToList();

                if (listSP.Find(x => x.Id == model.Bai7_2.San_Pham_ID) == null)
                {
                    ModelState.AddModelError("Bai7_2.San_Pham_ID", "sản phẩm đã bị xóa");
                }
            if (ModelState.IsValid)
                {
                    var bai7_2 = new BaiTap7_2
                    {
                        San_Pham_ID = model.Bai7_2.San_Pham_ID,
                        SL_Nhap = model.Bai7_2.SL_Nhap,
                        Don_Gia_Nhap = model.Bai7_2.Don_Gia_Nhap,
                        Nhap_Kho_ID = bai7Id
                    };
                    _db.tbl_DM_Nhap_Kho_Raw_Data.Add(bai7_2);
                    _db.SaveChanges();
                    TempData["success"] = "Thêm Thành Công";
                    return RedirectToAction("Index", new { bai7Id = bai7Id });

                }
            LoadViewBag();
            // Truyền lại bai7Id khi có lỗi
            ViewBag.Bai7Id = bai7Id;
            return View(model);
        }

        public IActionResult Edit(int idSP, int bai7Id)
        {
            var nkr = _db.tbl_DM_Nhap_Kho_Raw_Data.Include(x=>x.sanpham).FirstOrDefault(x=>x.Id == idSP);
            LoadViewBag();
            if (nkr == null)
            {
                return NotFound();
            }
            var viewModel = new ViewModelBai7
            {
                Bai7_2 = nkr
            };
            ViewBag.Bai7Id = bai7Id;
            ViewBag.idSP = idSP;
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(ViewModelBai7 model, int idSP,int bai7Id)
        {
            var nkr = _db.tbl_DM_Nhap_Kho_Raw_Data.Include(x=>x.sanpham).FirstOrDefault(x=>x.Id == idSP);
            if (nkr == null)
            {
                return NotFound();
            }
            if (model.Bai7_2.San_Pham_ID <= 0)
            {
                ModelState.AddModelError("Bai7_2.San_Pham_ID", "Chọn Sản phẩm");
            }
            var listSP = _db.tbl_DM_San_Pham.ToList();

            if (listSP.Find(x => x.Id == model.Bai7_2.San_Pham_ID) == null)
            {
                ModelState.AddModelError("Bai7_2.San_Pham_ID", "sản phẩm đã bị xóa");
                ViewBag.Bai7Id = bai7Id;
                ViewBag.idSP = idSP;
                LoadViewBag();
                return View(model);
            }
            
            if (model.Bai7_2.Don_Gia_Nhap < 0)
            {
                ModelState.AddModelError("Bai7_2.Don_Gia_Nhap", "đơn giá không được âm.");
                if (model.Bai7_2.SL_Nhap < 0)
                {
                    ModelState.AddModelError("Bai7_2.SL_Nhap", "Số lượng không được âm.");
                }
                ViewBag.Bai7Id = bai7Id;
                ViewBag.idSP = idSP;
                LoadViewBag();
                return View(model);
            }
                nkr.San_Pham_ID = model.Bai7_2.San_Pham_ID;
                nkr.SL_Nhap = model.Bai7_2.SL_Nhap;
                nkr.Don_Gia_Nhap = model.Bai7_2.Don_Gia_Nhap;
                _db.SaveChanges();
                TempData["success"] = "Sửa Thành Công";
                return RedirectToAction("Index", new { bai7Id = bai7Id });
        }
        //Hiển thị form xác nhận xóa chủng loại
        public IActionResult Delete(int IdSP,int bai7Id)
        {
            var sp = _db.tbl_DM_Nhap_Kho_Raw_Data.Include(x=>x.sanpham).FirstOrDefault(x=>x.Id==IdSP);
            if (sp == null)
            {
                return NotFound();
            }
            ViewBag.idSP = IdSP;
            ViewBag.Bai7Id = bai7Id;

            return View(sp);
        }
        // Xử lý xóa chủng loại
        public IActionResult DeleteConfirmed(int IdSP, int bai7Id)
        {
            var sp = _db.tbl_DM_Nhap_Kho_Raw_Data.FirstOrDefault(x=>x.Id==IdSP);
            if (sp == null)
            {
                return NotFound();
            }
            _db.tbl_DM_Nhap_Kho_Raw_Data.Remove(sp);
            ViewBag.Bai7Id = bai7Id;
            _db.SaveChanges();
            TempData["success"] = "Xóa Thành Công";
            return RedirectToAction("Index",new {bai7Id = bai7Id });
        }
    }
}
