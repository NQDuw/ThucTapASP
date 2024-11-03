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
    public class BaiTap15Controller : Controller
    {
        private readonly DBContext _db;
        public BaiTap15Controller(DBContext db)
        {
            _db = db;
        }
        public class CombinedViewModel
        {
            public BaiTap15 SearchModel { get; set; }
            public List<ViewModelBai7> ListSearch { get; set; }
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
                var listBai7 = _db.tbl_DM_Nhap_Kho.Include(x => x.Kho).Include(x => x.NCC);
                var listBai7_2 = _db.tbl_DM_Nhap_Kho_Raw_Data.Include(x => x.sanpham);

                var viewModelList = listBai7.Join(listBai7_2,
                    bai7 => bai7.Id,
                    bai7_2 => bai7_2.Id,
                    (bai7, bai7_2) => new ViewModelBai7
                    {
                        Bai7 = bai7,
                        Bai7_2 = bai7_2,
                        TriGia = (decimal)bai7_2.Don_Gia_Nhap * (decimal)bai7_2.SL_Nhap
                    })
                    .Where(x => x.Bai7.Ngay_Nhap_Kho >= model.SearchModel.NgayBatDau && x.Bai7.Ngay_Nhap_Kho <= model.SearchModel.NgayKetThuc)
                    .ToList();

                model.ListSearch = viewModelList; // Cập nhật ListSearch với kết quả tìm kiếm
            }

            return View(model); // Trả về view với model đã cập nhật
        }
    }
}
