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
    public class BaiTap3Controller : Controller
    {
        private readonly DBContext _db;
        private readonly IHostingEnvironment _hosting;
        public BaiTap3Controller(DBContext db, IHostingEnvironment hosting)
        {
            _db = db;
            _hosting = hosting;
        }
        public IActionResult Index()
        {
            var listSanPham = _db.tbl_DM_San_Pham.Include(x => x.LoaiSP).Include(x => x.DonViTinh).Where(x => x.Id != 1).ToList();
            return View(listSanPham);
        }
        private void LoadViewBag()
        {
            ViewBag.LoaiSPList = _db.tbl_DM_Loai_San_Pham.Where(x => x.Id != 1).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Ten_LSP
            }).ToList();

            ViewBag.DonViTinhList = _db.tbl_DM_Don_Vi_Tinh.Where(x => x.Id != 1).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Ten_Don_Vi_Tinh
            }).ToList();
        }

        public IActionResult Add()
        {
            LoadViewBag();
            return View();
        }
        [HttpPost]
        public IActionResult Add(BaiTap3 danhmuc)
        {
            if (string.IsNullOrWhiteSpace(danhmuc.Ma_San_Pham))
            {
                ModelState.AddModelError("Ma_San_Pham", "Mã sản phẩm không được để trống.");
            }
            else
            {
                var ktrMa = _db.tbl_DM_San_Pham
                              .FirstOrDefault(d => d.Ma_San_Pham.Trim().ToLower() == danhmuc.Ma_San_Pham.Trim().ToLower());

                if (ktrMa != null)
                {
                    ModelState.AddModelError("Ma_San_Pham", "Mã  sản phẩm đã tồn tại.");
                }
            }

            if (string.IsNullOrWhiteSpace(danhmuc.Ten_San_Pham))
            {
                ModelState.AddModelError("Ten_San_Pham", "Tên loại sản phẩm không được để trống.");
            }
            else
            {
                var ktten = _db.tbl_DM_San_Pham
                              .FirstOrDefault(d => d.Ten_San_Pham.Trim().ToLower() == danhmuc.Ten_San_Pham.Trim().ToLower());

                if (ktten != null)
                {
                    ModelState.AddModelError("Ten_San_Pham", "Tên  sản phẩm đã tồn tại.");
                }
            }

            if (danhmuc.Loai_San_Pham_ID <= 0)
            {
                ModelState.AddModelError("Loai_San_Pham_ID", "Vui lòng chọn loại sản phẩm.");
            }
            if (danhmuc.Don_Vi_Tin_ID <= 0)
            {
                ModelState.AddModelError("Don_Vi_Tin_ID", "Vui lòng chọn đơn vị tính.");
            }
            var listLoaiSanPham = _db.tbl_DM_Loai_San_Pham.ToList();

            if (listLoaiSanPham.Find(x=>x.Id == danhmuc.Loai_San_Pham_ID) == null)
            {
                ModelState.AddModelError("Loai_San_Pham_ID", "Sản phẩm đã bị xóa");
            }
            LoadViewBag();
            var listdonvitin = _db.tbl_DM_Don_Vi_Tinh.ToList();
            if (listdonvitin.Find(x => x.Id == danhmuc.Don_Vi_Tin_ID) == null)
            {
                ModelState.AddModelError("Don_Vi_Tin_ID", "Đơn vị tính đã xóa");
            }
            if (ModelState.IsValid)
            {
                _db.tbl_DM_San_Pham.Add(danhmuc);
                _db.SaveChanges();
                TempData["success"] = "Thêm Thành Công";
                return RedirectToAction("Index");
            }
            LoadViewBag();
            return View(danhmuc);
        }

        public IActionResult Update(int id,int idDVT,int idLSP)
        {
            var sp = _db.tbl_DM_San_Pham.Find(id);
            var dvt  = _db.tbl_DM_Don_Vi_Tinh.Find(idDVT);
            var lsp = _db.tbl_DM_Loai_San_Pham.Find(idLSP);
            if (sp == null)
            {
                return NotFound();
            }
            var viewModel = new BaiTap3
            {
                Ma_San_Pham=sp.Ma_San_Pham,
                Ten_San_Pham = sp.Ten_San_Pham,
                DonViTinh = dvt,
                LoaiSP = lsp,
                Ghi_Chu=sp.Ghi_Chu
            };
            LoadViewBag();
            return View(sp);
        }
        [HttpPost]

        public IActionResult Update(BaiTap3 sp)
        {
            var existingSP = _db.tbl_DM_San_Pham.Find(sp.Id);

            if (string.IsNullOrWhiteSpace(sp.Ma_San_Pham))
            {
                ModelState.AddModelError("Ma_San_Pham", "Mã sản phẩm không được để trống.");
            }
            else
            {
                var oldMaSanPham = existingSP.Ma_San_Pham;
                var ktrMaTrung = _db.tbl_DM_San_Pham
                             .FirstOrDefault(d => d.Ma_San_Pham.Trim().ToLower() == sp.Ma_San_Pham.Trim().ToLower());
                if (sp.Ma_San_Pham != oldMaSanPham)
                {
                    if (ktrMaTrung != null)
                    {
                        ModelState.AddModelError("Ma_San_Pham", "Mã sản phẩm đã tồn tại.");
                    }
                    else
                    {
                        existingSP.Ma_San_Pham = sp.Ma_San_Pham;
                    }
                }
            }
            if (string.IsNullOrWhiteSpace(sp.Ten_San_Pham))
            {
                ModelState.AddModelError("Ten_San_Pham", "Tên loại sản phẩm không được để trống.");

            }
            else
            {
                var oldTenSanPham = existingSP.Ten_San_Pham;

                var ktrTenTrung = _db.tbl_DM_San_Pham
                                  .FirstOrDefault(d => d.Ten_San_Pham.Trim().ToLower() == sp.Ten_San_Pham.Trim().ToLower());

                if (sp.Ten_San_Pham != oldTenSanPham)
                {
                    if (ktrTenTrung != null)
                    {
                        ModelState.AddModelError("Ten_San_Pham", "Tên sản phẩm đã tồn tại.");
                    }
                    else
                    {
                        existingSP.Ten_San_Pham = sp.Ten_San_Pham;
                    }
                }
            }
            LoadViewBag();
            if (sp.Loai_San_Pham_ID <= 0)
            {
                ModelState.AddModelError("Loai_San_Pham_ID", "Vui lòng chọn loại sản phẩm.");
            }

            if (sp.Don_Vi_Tin_ID <= 0)
            {
                ModelState.AddModelError("Don_Vi_Tin_ID", "Vui lòng chọn đơn vị tính.");
            }
            var listLoaiSanPham = _db.tbl_DM_Loai_San_Pham.ToList();

            if (listLoaiSanPham.Find(x => x.Id == sp.Loai_San_Pham_ID) == null)
            {
                ModelState.AddModelError("Loai_San_Pham_ID", "Sản phẩm đã bị xóa");
            }
            var listdonvitin = _db.tbl_DM_Loai_San_Pham.ToList();
            if (listdonvitin.Find(x => x.Id == sp.Don_Vi_Tin_ID) == null)
            {
                ModelState.AddModelError("Don_Vi_Tin_ID", "Đơn vị tính đã xóa");
            }
            // Cập nhật các sản phẩm có khóa ngoại Don_Vi_Tin_ID trỏ đến đơn vị tính này
            var sanPhamList7 = _db.tbl_DM_Nhap_Kho_Raw_Data.Where(x => x.San_Pham_ID == sp.Id).Count();
            // Cập nhật các sản phẩm có khóa ngoại Don_Vi_Tin_ID trỏ đến đơn vị tính này
            var sanPhamList11 = _db.tbl_DM_Xuat_Kho_Raw_Data.Where(x => x.San_Pham_ID == sp.Id).Count();
            if (sanPhamList7 > 0 || sanPhamList11 > 0)
            {
                TempData["Error"] = "Sửa thất bại do sản phẩm đã tồn tại trong kho";
                return RedirectToAction("Update", new { id = sp.Id });
            }
            if (ModelState.IsValid) {
                existingSP.Ten_San_Pham = sp.Ten_San_Pham;
                existingSP.Ma_San_Pham = sp.Ma_San_Pham;
                existingSP.Ten_San_Pham = sp.Ten_San_Pham;
                existingSP.Loai_San_Pham_ID = sp.Loai_San_Pham_ID;
                existingSP.Don_Vi_Tin_ID = sp.Don_Vi_Tin_ID;
                existingSP.Ghi_Chu = sp.Ghi_Chu;
                _db.SaveChanges();

                TempData["success"] = "Sửa Thành Công";
                return RedirectToAction("Index");
            }
                
            return View(sp);
        }
        //Hiển thị form xác nhận xóa chủng loại
        public IActionResult Delete(int id)
        {
            var lsp = _db.tbl_DM_San_Pham.FirstOrDefault(x => x.Id == id);
            if (lsp == null)
            {
                return NotFound();
            }
            return View(lsp);
        }
        [HttpPost]
        // Xử lý xóa chủng loại
        public IActionResult Delete(BaiTap3 model, int id)
        {
            var lsp = _db.tbl_DM_San_Pham.Find(id);
            if (lsp == null)
            {
                return NotFound();
            }
            // Cập nhật các sản phẩm có khóa ngoại Don_Vi_Tin_ID trỏ đến đơn vị tính này
            var sanPhamList7 = _db.tbl_DM_Nhap_Kho_Raw_Data.Where(sp => sp.San_Pham_ID == id).Count();
            // Cập nhật các sản phẩm có khóa ngoại Don_Vi_Tin_ID trỏ đến đơn vị tính này
            var sanPhamList11 = _db.tbl_DM_Xuat_Kho_Raw_Data.Where(sp => sp.San_Pham_ID == id).Count();
            if(sanPhamList7 >0 || sanPhamList11 >0)
            {
                TempData["Error"] = "Xóa thất bại do sản phẩm đã tồn tại trong kho";
                return RedirectToAction("Delete", new {id=id });
            }
            
            _db.tbl_DM_San_Pham.Remove(lsp);
            _db.SaveChanges();
            TempData["success"] = "Xóa Thành Công";
            return RedirectToAction("Index");
        }
    }
}
