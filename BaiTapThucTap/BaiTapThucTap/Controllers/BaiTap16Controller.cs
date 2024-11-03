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
            public List<ViewModelBai11> ListSearch { get; set; }
        }
        public IActionResult Index()
        {
            var model = new CombinedViewModel();
            return View(model); 
        }

        [HttpPost]
        public IActionResult Index(CombinedViewModel model)
        {
            if (ModelState.IsValid) // Kiểm tra tính hợp lệ của model
            {
                // Thực hiện tìm kiếm dựa trên model.SearchModel.NgayBatDau và model.SearchModel.NgayKetThuc
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
                    .Where(x => x.Bai11.Ngay_Xuat_Kho >= model.SearchModel.NgayBatDau && x.Bai11.Ngay_Xuat_Kho <= model.SearchModel.NgayKetThuc)
                    .ToList();

                model.ListSearch = viewModelList; // Cập nhật ListSearch với kết quả tìm kiếm
            }

            return View(model); // Trả về view với model đã cập nhật
        }
    }
}
