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
    public class BaiTap16Controller : Controller
    {
        private readonly DBContext _db;
        public BaiTap16Controller(DBContext db)
        {
            _db = db;
        }
        public class CombinedViewModel
        {
            public BaiTap16 SearchModel { get; set; }
            public List<BaiTapModel11_2> ListSearch { get; set; }
        }
        public IActionResult Index()
        {
            var model = new CombinedViewModel();
            var ngayNhap = new BaiTap16
            {
                NgayBatDau = new DateTime(2024, 01, 01),
                NgayKetThuc = new DateTime(2024, 12, 31)

            };
            var listBai11_2 = _db.tbl_DM_Xuat_Kho_Raw_Data
                .Include(x => x.sanpham)
                .Include(x => x.XuatKho)
                .Where(x => x.XuatKho.Ngay_Xuat_Kho >= ngayNhap.NgayBatDau && x.XuatKho.Ngay_Xuat_Kho <= ngayNhap.NgayKetThuc)
                .ToList();
            model.ListSearch = listBai11_2;
            model.SearchModel = ngayNhap;

            return View(model);
        }

        [HttpPost]
        public IActionResult Index(CombinedViewModel model)
        {
            if (ModelState.IsValid) // Kiểm tra tính hợp lệ của model
            {
                var listBai11_2 = _db.tbl_DM_Xuat_Kho_Raw_Data
                    .Include(x => x.sanpham)
                    .Include(x => x.XuatKho)
                    .Where(x => x.XuatKho.Ngay_Xuat_Kho >= model.SearchModel.NgayBatDau && x.XuatKho.Ngay_Xuat_Kho <= model.SearchModel.NgayKetThuc)
                    .ToList();
                model.ListSearch = listBai11_2; // Cập nhật ListSearch với kết quả tìm kiếm
            }

            return View(model); // Trả về view với model đã cập nhật
        }
    }
}
