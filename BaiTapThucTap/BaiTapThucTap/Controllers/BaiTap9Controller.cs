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
    public class BaiTap9Controller : Controller
    {
        private readonly DBContext _db;
        private readonly IHostingEnvironment _hosting;
        public BaiTap9Controller(DBContext db, IHostingEnvironment hosting)
        {
            _db = db;
            _hosting = hosting;
        }
        public class CombinedViewModel
        {
            public BaiTap9 BaiTap9Model { get; set; }
            public List<ViewModelBai7> ViewModelList { get; set; }
        }
        public IActionResult Index(BaiTap9 model, int bai7Id, int bai7_2Id)
        {
            var nk = _db.tbl_DM_Nhap_Kho.Include(x=>x.Kho).Include(x=>x.NCC).FirstOrDefault(x => x.Id == bai7Id);
            var nkr = _db.tbl_DM_Nhap_Kho_Raw_Data.Include(x=>x.sanpham).FirstOrDefault(x => x.Id == bai7_2Id);
            if (nk == null || nkr == null)
            {
                return NotFound();
            }
            var viewModel = new BaiTap9
            {
                Bai7 = nk,
                Bai7_2 = nkr,
                TriGia = nkr.SL_Nhap * nkr.Don_Gia_Nhap
            };
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
        .ToList();
            var combinedViewModel = new CombinedViewModel
            {
                BaiTap9Model = viewModel,
                ViewModelList = viewModelList
            };
            return View(combinedViewModel);
        }
    }
}
