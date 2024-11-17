using BaiTapThucTap.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaiTapThucTap.Controllers
{
    public class BaiTap17Controller : Controller
    {
        private readonly DBContext _db;

        public BaiTap17Controller(DBContext db)
        {
            _db = db;
        }

        public class CombinedViewModel
        {
            public BaiTap17 SearchModel { get; set; }
            public List<BaiTap17> ListSearchSP { get; set; }

        }

        public IActionResult Index()
        {
            var model = new CombinedViewModel();
            var ngayNhap = new BaiTap17
            {
                NgayBatDau = new DateTime(2024, 1, 1),
                NgayKetThuc = new DateTime(2024, 12, 30)
            };
            var dauKyTruoc = ngayNhap.NgayBatDau.Month == 1
                     ? new DateTime(ngayNhap.NgayBatDau.Year - 1, 12, ngayNhap.NgayBatDau.Day)
                     : new DateTime(ngayNhap.NgayBatDau.Year, ngayNhap.NgayBatDau.Month - 1, ngayNhap.NgayBatDau.Day);

            var cuoiKyTruoc = ngayNhap.NgayKetThuc.Month == 1
                ? new DateTime(ngayNhap.NgayKetThuc.Year - 1, 12, ngayNhap.NgayKetThuc.Day == 31 ? 30 : ngayNhap.NgayKetThuc.Day)
                : new DateTime(ngayNhap.NgayKetThuc.Year, ngayNhap.NgayKetThuc.Month - 1, ngayNhap.NgayKetThuc.Day == 31 ? 30 : ngayNhap.NgayKetThuc.Day);

            // Lấy danh sách dữ liệu
            var listBai7_2 = _db.tbl_DM_Nhap_Kho_Raw_Data
                .Include(x => x.sanpham)
                .Include(x => x.NhapKho.Kho)
                .Include(x => x.NhapKho.NCC)
                .Where(x => x.NhapKho.Ngay_Nhap_Kho >= ngayNhap.NgayBatDau && x.NhapKho.Ngay_Nhap_Kho <= ngayNhap.NgayKetThuc)
                .ToList();

            var listBai11_2 = _db.tbl_DM_Xuat_Kho_Raw_Data
                .Include(x => x.sanpham)
                .Include(x => x.XuatKho.Kho)
                .Where(x => x.XuatKho.Ngay_Xuat_Kho >= ngayNhap.NgayBatDau && x.XuatKho.Ngay_Xuat_Kho <= ngayNhap.NgayKetThuc)
                    .ToList();

            var listBai3 = _db.tbl_DM_San_Pham
                .Include(x => x.DonViTinh)
                .Include(x => x.LoaiSP).ToList();
    
            var viewBai17TheoSP = listBai3.Select(bai3=>  new BaiTap17
                {
                    Ma_San_Pham = bai3.Ma_San_Pham,
                    Ten_San_Pham = bai3.Ten_San_Pham,
                    // Tìm các giá trị từ viewModelList liên quan đến sản phẩm
                    SL_Dau_Ky =  0,
                    SL_Nhap = listBai7_2.Where(x => x.sanpham.Id == bai3.Id).Sum(x=>x?.SL_Nhap ?? 0),
                    SL_Xuat = listBai11_2.Where(x => x.sanpham.Id == bai3.Id).Sum(x => x?.SL_Xuat ?? 0),
                    SL_Cuoi_Ky = listBai7_2.Where(x => x.sanpham.Id == bai3.Id).Sum(x => x?.SL_Nhap ?? 0)
                                -
                                listBai11_2.Where(x => x.sanpham.Id == bai3.Id).Sum(x => x?.SL_Xuat ?? 0)
            })
                .ToList();
            model.SearchModel = ngayNhap;
            model.ListSearchSP = viewBai17TheoSP;
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(CombinedViewModel model)
        {
            if(model.SearchModel.NgayBatDau > model.SearchModel.NgayKetThuc)
            {
                return View(model);
            }
            var cuoiKyTruoc = model.SearchModel.NgayBatDau.AddDays(-1); ;
            
            // Lấy danh sách dữ liệu
            var listBai7_2 = _db.tbl_DM_Nhap_Kho_Raw_Data
                .Include(x => x.sanpham)
                .Include(x => x.NhapKho.Kho)
                .Include(x => x.NhapKho.NCC)
                .Where(x => x.NhapKho.Ngay_Nhap_Kho >= model.SearchModel.NgayBatDau && x.NhapKho.Ngay_Nhap_Kho <= model.SearchModel.NgayKetThuc)
                .ToList();

            var listBai11_2 = _db.tbl_DM_Xuat_Kho_Raw_Data
                .Include(x => x.sanpham)
                .Include(x => x.XuatKho.Kho)
                .Where(x => x.XuatKho.Ngay_Xuat_Kho >= model.SearchModel.NgayBatDau && x.XuatKho.Ngay_Xuat_Kho <= model.SearchModel.NgayKetThuc)
                    .ToList();

            var listBai3 = _db.tbl_DM_San_Pham
                .Include(x => x.DonViTinh)
                .Include(x => x.LoaiSP).ToList();

            // Kết hợp danh sách
            var List7KyTruoc = _db.tbl_DM_Nhap_Kho_Raw_Data
                .Include(x => x.sanpham)
                .Include(x => x.NhapKho.Kho)
                .Include(x => x.NhapKho.NCC)
                .Where(x => x.NhapKho.Ngay_Nhap_Kho <= cuoiKyTruoc)
                .ToList();

            var List11KyTruoc = _db.tbl_DM_Xuat_Kho_Raw_Data
                .Include(x => x.sanpham)
                .Include(x => x.XuatKho.Kho)
                .Where(x => x.XuatKho.Ngay_Xuat_Kho <= cuoiKyTruoc)
                .ToList();

            var viewKyTruocTheoSP = listBai3.Select(bai3 => new BaiTap17
            {
                Bai3=bai3,
                Ma_San_Pham = bai3.Ma_San_Pham,
                Ten_San_Pham = bai3.Ten_San_Pham,
                // Tìm các giá trị từ viewModelList liên quan đến sản phẩm
                SL_Dau_Ky = 0,
                SL_Nhap = List7KyTruoc.Where(x => x.sanpham.Id == bai3.Id).Sum(x => x?.SL_Nhap ?? 0),
                SL_Xuat = List11KyTruoc.Where(x => x.sanpham.Id == bai3.Id).Sum(x => x?.SL_Xuat ?? 0),
                SL_Cuoi_Ky = List7KyTruoc.Where(x => x.sanpham.Id == bai3.Id).Sum(x => x?.SL_Nhap ?? 0)
                            -
                            List11KyTruoc.Where(x => x.sanpham.Id == bai3.Id).Sum(x => x?.SL_Xuat ?? 0)
            }) .ToList();

            var viewBai17TheoSP = listBai3.Select(bai3 => new BaiTap17
            {
                Bai3=bai3,
                Ma_San_Pham = bai3.Ma_San_Pham,
                Ten_San_Pham = bai3.Ten_San_Pham,
                // Tìm các giá trị từ viewModelList liên quan đến sản phẩm
                SL_Dau_Ky = viewKyTruocTheoSP.Where(x => x.Bai3.Id== bai3.Id).Sum(x => x?.SL_Cuoi_Ky ?? 0),
                SL_Nhap = listBai7_2.Where(x => x.sanpham.Id == bai3.Id).Sum(x=>x?.SL_Nhap ?? 0),
                SL_Xuat = listBai11_2.Where(x => x.sanpham.Id == bai3.Id).Sum(x=>x?.SL_Xuat ?? 0),
                SL_Cuoi_Ky = viewKyTruocTheoSP.Where(x => x.Bai3.Id == bai3.Id).Sum(x => x?.SL_Cuoi_Ky ?? 0)
                            +
                             listBai7_2.Where(x => x.sanpham.Id == bai3.Id).Sum(x => x?.SL_Nhap ?? 0)
                            -
                             listBai11_2.Where(x => x.sanpham.Id == bai3.Id).Sum(x => x?.SL_Xuat ?? 0)
            }).ToList();
            model.ListSearchSP = viewBai17TheoSP;
           
            return View(model);
        }
           
    }
}
