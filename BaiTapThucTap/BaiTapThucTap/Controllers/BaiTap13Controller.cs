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
    public class BaiTap13Controller : Controller
    {
        private readonly DBContext _db;
        private readonly IHostingEnvironment _hosting;
        public BaiTap13Controller(DBContext db, IHostingEnvironment hosting)
        {
            _db = db;
            _hosting = hosting;
        }
        public class CombinedViewModel
        {
            public BaiTapModel13 BaiTap13Model { get; set; }
            public List<ViewModelBai11> ViewModelList { get; set; }
        }
        public IActionResult Index(BaiTapModel13 model, int bai11Id, int bai11_2Id)
        {
            var xk = _db.tbl_DM_Xuat_Kho.Include(x=>x.Kho).FirstOrDefault(x => x.Id == bai11Id);
            var xkr = _db.tbl_DM_Xuat_Kho_Raw_Data.Include(x=>x.sanpham).FirstOrDefault(x => x.Id == bai11_2Id);
            if (xk == null || xkr == null)
            {
                return NotFound();
            }
            var viewModel = new BaiTapModel13
            {
                Bai11 = xk,
                Bai11_2 = xkr,
                TriGia = (decimal)xkr.Don_Gia_Xuat * (decimal)xkr.SL_Xuat
            };
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
                }).ToList();
            var combinedViewModel = new CombinedViewModel
            {
                BaiTap13Model = viewModel,
                ViewModelList = viewModelList
            };
            return View(combinedViewModel);
        }
    }
}
