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
using WebDienThoai.Models;

namespace BaiTapThucTap.Controllers
{
    public class BaiTap6Controller : Controller
    {
        private readonly DBContext _db;
        private readonly IHostingEnvironment _hosting;
        private readonly UserManager<ApplicationUser> _userManager;
        public BaiTap6Controller(DBContext db, IHostingEnvironment hosting, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _hosting = hosting;
            _userManager = userManager;
        }
        private void LoadViewBag()
        {
            ViewBag.UserList = _userManager.Users.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Email
            }).ToList();

            ViewBag.KhoList = _db.tbl_DM_Kho.Where(x=>x.Id != 1).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Ten_Kho
            }).ToList();
        }
        public IActionResult Index()
        {
            var listKho = _db.tbl_DM_Kho_User.Include(x => x.Kho).Include(x=>x.User).ToList();
            LoadViewBag();
            return View(listKho);
        }

        public IActionResult Add()
        {
            LoadViewBag();
            return View();
        }

        [HttpPost]
        public IActionResult Add(BaiTap6 model)
        {
            if (string.IsNullOrEmpty(model.Id))
            {
                model.Id = Guid.NewGuid().ToString();
            }
            if ( model.Ma_Dang_Nhap == "1")
            {
                var ktrMa = _db.tbl_DM_Kho_User
                                    .Where(d => d.Ma_Dang_Nhap == model.Ma_Dang_Nhap).Select(x=>x.Kho_ID).ToList();
                // Kiểm tra xem model.Kho_ID có nằm trong danh sách ktrMa không
                if (ktrMa.Contains(model.Kho_ID))
                {
                    ModelState.AddModelError("Kho_ID", "User đã được gán cho Kho Này");

                }
                var listkho = _db.tbl_DM_Kho.ToList();

                if (listkho.Find(x => x.Id == model.Kho_ID) == null)
                {
                    ModelState.AddModelError("Kho_ID", "Kho đã bị xóa");
                }
                if (ModelState.IsValid)
                {
                    _db.tbl_DM_Kho_User.Add(model);
                    _db.SaveChanges();
                    TempData["success"] = "Thêm Thành Công";

                    return RedirectToAction("Index");
                }
            } 
            else 
            { 
                var ktrMa = _db.tbl_DM_Kho_User
                                .FirstOrDefault(d => d.Ma_Dang_Nhap == model.Ma_Dang_Nhap);

                if (ktrMa != null)
                {
                    ModelState.AddModelError("User.Email", "mã đăng nhập đã tồn tại.");
                }
                var listUser = _db.tbl_DM_Kho_User.ToList();


                var listkho = _db.tbl_DM_Kho.ToList();

                if (listkho.Find(x => x.Id == model.Kho_ID) == null)
                {
                    ModelState.AddModelError("Kho_ID", "Kho đã bị xóa");
                }
                if (ModelState.IsValid)
                {
                    _db.tbl_DM_Kho_User.Add(model);
                    _db.SaveChanges();
                    TempData["success"] = "Thêm Thành Công";
                    return RedirectToAction("Index");
                } 
            }
            LoadViewBag();
            return View(model);
        }
        public IActionResult Edit(string Id)
        {
            var user = _db.tbl_DM_Kho_User.Include(x => x.Kho).Include(x => x.User).FirstOrDefault(x => x.Id == Id);
            if (user == null)
            {
                return NotFound();
            }
            LoadViewBag();
            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(BaiTap6 model,string Id)
        {
            var user = _db.tbl_DM_Kho_User.Include(x=>x.User).FirstOrDefault(x => x.Id == Id);
            LoadViewBag();
            // Kiểm tra nếu user không tồn tại
            if (user == null)
            {
                TempData["error"] = "User không tồn tại.";
                return RedirectToAction("Index");
            }
            // gán giá trị cho model.user.email
            model.User = user.User;
            // Kiểm tra nếu model.User là null hoặc email không hợp lệ
            if (model.User == null || string.IsNullOrEmpty(model.User.Email))
            {
                ModelState.AddModelError("User.Email", "Email của user không hợp lệ.");
                // Truyền model vào view
                model.Id = user.Id;
                model.User = user.User;
                LoadViewBag();
                return View(model);
            }
            if (model.User.Email.Trim().ToLower() == "admin@gmail.com")
            {
                //// Kiểm tra xem model.Kho_ID có nằm trong danh sách ktrMa không
                //ModelState.AddModelError("User.Email", $"{user.Kho_ID}   {model.Kho_ID}");

                //if (user.Kho_ID == model.Kho_ID)
                //{
                //    ModelState.AddModelError("Kho_ID", "User đã được gán cho Kho Này");

                //}
                var ktrMa = _db.tbl_DM_Kho_User
                                   .Where(d => d.User.Email.Trim().ToLower() == model.User.Email.Trim().ToLower()).Select(x => x.Kho_ID).ToList();

                // Kiểm tra xem model.Kho_ID có nằm trong danh sách ktrMa không
                if (ktrMa.Contains(model.Kho_ID))
                {
                    ModelState.AddModelError("Kho_ID", "User đã được gán cho Kho Này");
                    // Truyền model vào view
                    model.Id = user.Id;
                    model.User = user.User;
                    LoadViewBag();
                    return View(model);
                }
                var listkho = _db.tbl_DM_Kho.ToList();

                if (listkho.Find(x => x.Id == model.Kho_ID) == null)
                {
                    ModelState.AddModelError("Kho_ID", "Sản phẩm đã bị xóa");
                    // Truyền model vào view
                    model.Id = user.Id;
                    model.User = user.User;
                    LoadViewBag();
                    return View(model);
                }
                user.Kho_ID = model.Kho_ID;
                    _db.SaveChanges();
                    TempData["success"] = "Sửa Thành Công";
                    return RedirectToAction("Index");
               
            }
            else
            {
                if (user == null)
                {
                    TempData["error"] = $"Sửa kho thất bại";
                    return RedirectToAction("Index");
                }
                
                var listkho = _db.tbl_DM_Kho.ToList();

                if (listkho.Find(x => x.Id == model.Kho_ID) == null)
                {
                    ModelState.AddModelError("Kho_ID", "Sản phẩm đã bị xóa");
                    // Truyền model vào view
                    model.Id = user.Id;
                    model.User = user.User;
                    LoadViewBag();
                    return View(model);
                }
                user.Kho_ID = model.Kho_ID;
                    _db.SaveChanges();
                    TempData["success"] = "Sửa Thành Công";
                    return RedirectToAction("Index");
                
            }
            return View(model);
        }

        //Hiển thị form xác nhận xóa chủng loại
        public IActionResult Delete(string Id)
        {
            var uk = _db.tbl_DM_Kho_User.Include(x => x.Kho).Include(x => x.User).FirstOrDefault(x => x.Id == Id);
            if (uk == null)
            {
                return NotFound();
            }
            LoadViewBag();
            return View(uk);
        }
        // Xử lý xóa chủng loại
        public IActionResult DeleteConfirmed(string id)
        {
            var uk = _db.tbl_DM_Kho_User.FirstOrDefault(x=>x.Id == id);
            if (uk == null)
            {
                return NotFound();
            }
            _db.tbl_DM_Kho_User.Remove(uk);
            _db.SaveChanges();
            TempData["success"] = "Xóa Thành Công";
            return RedirectToAction("Index");
        }
    }
}
