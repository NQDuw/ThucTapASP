using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaiTapThucTap.Models
{
    public class DBContext: IdentityDbContext<IdentityUser>
    {
        public DBContext(DbContextOptions<DBContext> op) : base(op) { }
        public DbSet<BaiTap1> tbl_DM_Don_Vi_Tinh { set; get; }
        public DbSet<BaiTap2> tbl_DM_Loai_San_Pham { set; get; }
        public DbSet<BaiTap3> tbl_DM_San_Pham { set; get; }
        public DbSet<BaiTap4> tbl_DM_NCC{ set; get; }
        public DbSet<BaiTap5> tbl_DM_Kho { set; get; }
        public DbSet<BaiTap6> tbl_DM_Kho_User { set; get; }
        public DbSet<BaiTap7> tbl_DM_Nhap_Kho { set; get; }
        public DbSet<BaiTap7_2> tbl_DM_Nhap_Kho_Raw_Data { set; get; }
        public DbSet<BaiTap8> tbl_XNK_Nhap_Kho { set; get; }
        public DbSet<BaiTap11> tbl_DM_Xuat_Kho { set; get; }
        public DbSet<BaiTapModel11_2> tbl_DM_Xuat_Kho_Raw_Data { set; get; }
        public DbSet<BaiTapModel12> tbl_XNK_Xuat_Kho { set; get; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BaiTap1>().HasData(
            new BaiTap1 { Id = 1, Ten_Don_Vi_Tinh = "Tấn" },
            new BaiTap1 { Id = 2, Ten_Don_Vi_Tinh = "Kg" },
            new BaiTap1 { Id = 3, Ten_Don_Vi_Tinh = "g" });

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BaiTap2>().HasData(
            new BaiTap2 { Id = 1,Ma_LSP="LSPT", Ten_LSP = "Thực Phẩm Tươi" },
            new BaiTap2 { Id = 2,Ma_LSP="LSPK", Ten_LSP = "Thực Phẩm Khô" });

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BaiTap3>().HasData(
            new BaiTap3 { Id = 1, Ma_San_Pham = "TPT1", Ten_San_Pham = "Cá", Don_Vi_Tin_ID = 1, Loai_San_Pham_ID = 1 },
            new BaiTap3 { Id = 2, Ma_San_Pham = "TPT2", Ten_San_Pham = "Tôm", Don_Vi_Tin_ID = 3, Loai_San_Pham_ID = 1 },
            new BaiTap3 { Id = 3, Ma_San_Pham = "TPT3", Ten_San_Pham = "Cua", Don_Vi_Tin_ID = 3, Loai_San_Pham_ID = 1 },
            new BaiTap3 { Id = 4, Ma_San_Pham = "TPK1", Ten_San_Pham = "Khô Cá", Don_Vi_Tin_ID = 2, Loai_San_Pham_ID = 2 },
            new BaiTap3 { Id = 5, Ma_San_Pham = "TPK2", Ten_San_Pham = "Lạp Xưởng", Don_Vi_Tin_ID = 2, Loai_San_Pham_ID = 2 },
            new BaiTap3 { Id = 6, Ma_San_Pham = "TPK3", Ten_San_Pham = "Thịt Khô", Don_Vi_Tin_ID = 3, Loai_San_Pham_ID = 2 });

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BaiTap4>().HasData(
            new BaiTap4 { Id = 1, Ma_NCC = "CTK", Ten_NCC = "Công Ty Khô", },
            new BaiTap4 { Id = 2, Ma_NCC = "CTT", Ten_NCC = "Công Ty Tươi", });

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BaiTap5>().HasData(
            new BaiTap5 { Id = 1,Ten_Kho="Đông Lạnh A" },
            new BaiTap5 { Id = 2,Ten_Kho ="Hầm Chứa B"});

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BaiTap7>().HasData(
            new BaiTap7 { Id = 1, So_Phieu_Nhap_Kho = "SPN1", Kho_ID = 1, NCC_ID = 1, Ngay_Nhap_Kho = new DateTime(2024, 10, 8) },
            new BaiTap7 { Id = 2, So_Phieu_Nhap_Kho = "SPN2", Kho_ID = 2, NCC_ID = 2, Ngay_Nhap_Kho = new DateTime(2024, 11, 8) });

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BaiTap7_2>().HasData(
            new BaiTap7_2 { Id = 1, Nhap_Kho_ID = 1, San_Pham_ID = 1, SL_Nhap = 3, Don_Gia_Nhap = 1500000 },
            new BaiTap7_2 { Id = 2, Nhap_Kho_ID = 2, San_Pham_ID = 2, SL_Nhap = 15, Don_Gia_Nhap = 8500000 });


            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BaiTap11>().HasData(
            new BaiTap11 { Id = 1, So_Phieu_Xuat_Kho = "SPX1", Kho_ID = 1, Ngay_Xuat_Kho = new DateTime(2024, 10,11) },
            new BaiTap11 { Id = 2, So_Phieu_Xuat_Kho = "SPX2", Kho_ID = 2, Ngay_Xuat_Kho = new DateTime(2024, 11, 11) });

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BaiTapModel11_2>().HasData(
            new BaiTapModel11_2 { Id = 1, Xuat_Kho_ID = 1, San_Pham_ID = 1, SL_Xuat = 3, Don_Gia_Xuat = 1500000 },
            new BaiTapModel11_2 { Id = 2, Xuat_Kho_ID = 2, San_Pham_ID = 2, SL_Xuat = 9, Don_Gia_Xuat = 7600000 });

        }
    }
}
