﻿// <auto-generated />
using System;
using BaiTapThucTap.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BaiTapThucTap.Migrations
{
    [DbContext(typeof(DBContext))]
    [Migration("20241115092757_thuctapdb")]
    partial class thuctapdb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BaiTapThucTap.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<int>("Kho_ID")
                        .HasColumnType("int");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("BaiTapThucTap.Models.BaiTap1", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Ghi_Chu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ten_Don_Vi_Tinh")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("tbl_DM_Don_Vi_Tinh");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Ten_Don_Vi_Tinh = "đơn vị tính đã bị xóa"
                        },
                        new
                        {
                            Id = 2,
                            Ten_Don_Vi_Tinh = "Tấn"
                        },
                        new
                        {
                            Id = 3,
                            Ten_Don_Vi_Tinh = "Kg"
                        },
                        new
                        {
                            Id = 4,
                            Ten_Don_Vi_Tinh = "g"
                        });
                });

            modelBuilder.Entity("BaiTapThucTap.Models.BaiTap11", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Ghi_Chu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Kho_ID")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<DateTime>("Ngay_Xuat_Kho")
                        .HasColumnType("datetime2");

                    b.Property<string>("So_Phieu_Xuat_Kho")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Kho_ID");

                    b.ToTable("tbl_DM_Xuat_Kho");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Kho_ID = 2,
                            Ngay_Xuat_Kho = new DateTime(2024, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            So_Phieu_Xuat_Kho = "SPX1"
                        },
                        new
                        {
                            Id = 2,
                            Kho_ID = 3,
                            Ngay_Xuat_Kho = new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            So_Phieu_Xuat_Kho = "SPX2"
                        });
                });

            modelBuilder.Entity("BaiTapThucTap.Models.BaiTap2", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Ghi_Chu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ma_LSP")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ten_LSP")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("tbl_DM_Loai_San_Pham");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Ma_LSP = "Loại Sản Phẩm đã bị xóa",
                            Ten_LSP = "Loại Sản Phẩm đã bị xóa"
                        },
                        new
                        {
                            Id = 2,
                            Ma_LSP = "LSPT",
                            Ten_LSP = "Thực Phẩm Tươi"
                        },
                        new
                        {
                            Id = 3,
                            Ma_LSP = "LSPK",
                            Ten_LSP = "Thực Phẩm Khô"
                        });
                });

            modelBuilder.Entity("BaiTapThucTap.Models.BaiTap3", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Don_Vi_Tin_ID")
                        .HasColumnType("int");

                    b.Property<string>("Ghi_Chu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Loai_San_Pham_ID")
                        .HasColumnType("int");

                    b.Property<string>("Ma_San_Pham")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ten_San_Pham")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Don_Vi_Tin_ID");

                    b.HasIndex("Loai_San_Pham_ID");

                    b.ToTable("tbl_DM_San_Pham");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Don_Vi_Tin_ID = 1,
                            Loai_San_Pham_ID = 1,
                            Ma_San_Pham = "Sản phẩm đã bị xóa",
                            Ten_San_Pham = "sản phẩm đã bị xóa"
                        },
                        new
                        {
                            Id = 2,
                            Don_Vi_Tin_ID = 2,
                            Loai_San_Pham_ID = 2,
                            Ma_San_Pham = "TPT1",
                            Ten_San_Pham = "Cá"
                        },
                        new
                        {
                            Id = 3,
                            Don_Vi_Tin_ID = 4,
                            Loai_San_Pham_ID = 2,
                            Ma_San_Pham = "TPT2",
                            Ten_San_Pham = "Tôm"
                        },
                        new
                        {
                            Id = 4,
                            Don_Vi_Tin_ID = 4,
                            Loai_San_Pham_ID = 2,
                            Ma_San_Pham = "TPT3",
                            Ten_San_Pham = "Cua"
                        },
                        new
                        {
                            Id = 5,
                            Don_Vi_Tin_ID = 3,
                            Loai_San_Pham_ID = 3,
                            Ma_San_Pham = "TPK1",
                            Ten_San_Pham = "Khô Cá"
                        },
                        new
                        {
                            Id = 6,
                            Don_Vi_Tin_ID = 3,
                            Loai_San_Pham_ID = 3,
                            Ma_San_Pham = "TPK2",
                            Ten_San_Pham = "Lạp Xưởng"
                        },
                        new
                        {
                            Id = 7,
                            Don_Vi_Tin_ID = 4,
                            Loai_San_Pham_ID = 3,
                            Ma_San_Pham = "TPK3",
                            Ten_San_Pham = "Thịt Khô"
                        });
                });

            modelBuilder.Entity("BaiTapThucTap.Models.BaiTap4", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Ghi_Chu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ma_NCC")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ten_NCC")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("tbl_DM_NCC");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Ma_NCC = "Nhà cung cấp đã bị xóa",
                            Ten_NCC = "nhà cung cấp đã bị xóa"
                        },
                        new
                        {
                            Id = 2,
                            Ma_NCC = "CTK",
                            Ten_NCC = "Công Ty Khô"
                        },
                        new
                        {
                            Id = 3,
                            Ma_NCC = "CTT",
                            Ten_NCC = "Công Ty Tươi"
                        });
                });

            modelBuilder.Entity("BaiTapThucTap.Models.BaiTap5", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Ghi_Chu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ten_Kho")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("tbl_DM_Kho");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Ten_Kho = "Kho đã bị xóa"
                        },
                        new
                        {
                            Id = 2,
                            Ten_Kho = "Đông Lạnh A"
                        },
                        new
                        {
                            Id = 3,
                            Ten_Kho = "Hầm Chứa B"
                        });
                });

            modelBuilder.Entity("BaiTapThucTap.Models.BaiTap6", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("Kho_ID")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("Ma_Dang_Nhap")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Kho_ID");

                    b.HasIndex("Ma_Dang_Nhap");

                    b.ToTable("tbl_DM_Kho_User");
                });

            modelBuilder.Entity("BaiTapThucTap.Models.BaiTap7", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Ghi_Chu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Kho_ID")
                        .HasColumnType("int");

                    b.Property<int>("NCC_ID")
                        .HasColumnType("int");

                    b.Property<DateTime>("Ngay_Nhap_Kho")
                        .HasColumnType("datetime2");

                    b.Property<string>("So_Phieu_Nhap_Kho")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Kho_ID");

                    b.HasIndex("NCC_ID");

                    b.ToTable("tbl_DM_Nhap_Kho");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Kho_ID = 2,
                            NCC_ID = 2,
                            Ngay_Nhap_Kho = new DateTime(2024, 10, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            So_Phieu_Nhap_Kho = "SPN1"
                        },
                        new
                        {
                            Id = 2,
                            Kho_ID = 3,
                            NCC_ID = 3,
                            Ngay_Nhap_Kho = new DateTime(2024, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            So_Phieu_Nhap_Kho = "SPN2"
                        });
                });

            modelBuilder.Entity("BaiTapThucTap.Models.BaiTap7_2", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Don_Gia_Nhap")
                        .HasColumnType("int");

                    b.Property<int>("Nhap_Kho_ID")
                        .HasColumnType("int");

                    b.Property<int>("SL_Nhap")
                        .HasColumnType("int");

                    b.Property<int>("San_Pham_ID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Nhap_Kho_ID");

                    b.HasIndex("San_Pham_ID");

                    b.ToTable("tbl_DM_Nhap_Kho_Raw_Data");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Don_Gia_Nhap = 1500000,
                            Nhap_Kho_ID = 1,
                            SL_Nhap = 3,
                            San_Pham_ID = 2
                        },
                        new
                        {
                            Id = 2,
                            Don_Gia_Nhap = 8500000,
                            Nhap_Kho_ID = 2,
                            SL_Nhap = 15,
                            San_Pham_ID = 3
                        });
                });

            modelBuilder.Entity("BaiTapThucTap.Models.BaiTap8", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Bai7Id")
                        .HasColumnType("int");

                    b.Property<int?>("Bai7_2Id")
                        .HasColumnType("int");

                    b.Property<string>("Ghi_Chu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Kho_ID")
                        .HasColumnType("int");

                    b.Property<int>("NCC_ID")
                        .HasColumnType("int");

                    b.Property<DateTime>("Ngay_Nhap_Kho")
                        .HasColumnType("datetime2");

                    b.Property<string>("So_Phieu_Nhap_Kho")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TriGia")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("Bai7Id");

                    b.HasIndex("Bai7_2Id");

                    b.ToTable("tbl_XNK_Nhap_Kho");
                });

            modelBuilder.Entity("BaiTapThucTap.Models.BaiTapModel11_2", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Don_Gia_Xuat")
                        .HasColumnType("int");

                    b.Property<int>("SL_Xuat")
                        .HasColumnType("int");

                    b.Property<int>("San_Pham_ID")
                        .HasColumnType("int");

                    b.Property<int>("Xuat_Kho_ID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("San_Pham_ID");

                    b.HasIndex("Xuat_Kho_ID");

                    b.ToTable("tbl_DM_Xuat_Kho_Raw_Data");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Don_Gia_Xuat = 1500000,
                            SL_Xuat = 3,
                            San_Pham_ID = 2,
                            Xuat_Kho_ID = 1
                        },
                        new
                        {
                            Id = 2,
                            Don_Gia_Xuat = 7600000,
                            SL_Xuat = 9,
                            San_Pham_ID = 3,
                            Xuat_Kho_ID = 2
                        });
                });

            modelBuilder.Entity("BaiTapThucTap.Models.BaiTapModel12", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Bai11Id")
                        .HasColumnType("int");

                    b.Property<int?>("Bai11_2Id")
                        .HasColumnType("int");

                    b.Property<string>("Ghi_Chu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Kho_ID")
                        .HasColumnType("int");

                    b.Property<int>("NCC_ID")
                        .HasColumnType("int");

                    b.Property<DateTime>("Ngay_Xuat_Kho")
                        .HasColumnType("datetime2");

                    b.Property<string>("So_Phieu_Xuat_Kho")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TriGia")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("Bai11Id");

                    b.HasIndex("Bai11_2Id");

                    b.ToTable("tbl_XNK_Xuat_Kho");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("BaiTapThucTap.Models.BaiTap11", b =>
                {
                    b.HasOne("BaiTapThucTap.Models.BaiTap5", "Kho")
                        .WithMany()
                        .HasForeignKey("Kho_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kho");
                });

            modelBuilder.Entity("BaiTapThucTap.Models.BaiTap3", b =>
                {
                    b.HasOne("BaiTapThucTap.Models.BaiTap1", "DonViTinh")
                        .WithMany("SanPhams")
                        .HasForeignKey("Don_Vi_Tin_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BaiTapThucTap.Models.BaiTap2", "LoaiSP")
                        .WithMany()
                        .HasForeignKey("Loai_San_Pham_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DonViTinh");

                    b.Navigation("LoaiSP");
                });

            modelBuilder.Entity("BaiTapThucTap.Models.BaiTap6", b =>
                {
                    b.HasOne("BaiTapThucTap.Models.BaiTap5", "Kho")
                        .WithMany()
                        .HasForeignKey("Kho_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BaiTapThucTap.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("Ma_Dang_Nhap")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kho");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BaiTapThucTap.Models.BaiTap7", b =>
                {
                    b.HasOne("BaiTapThucTap.Models.BaiTap5", "Kho")
                        .WithMany()
                        .HasForeignKey("Kho_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BaiTapThucTap.Models.BaiTap4", "NCC")
                        .WithMany()
                        .HasForeignKey("NCC_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kho");

                    b.Navigation("NCC");
                });

            modelBuilder.Entity("BaiTapThucTap.Models.BaiTap7_2", b =>
                {
                    b.HasOne("BaiTapThucTap.Models.BaiTap7", "NhapKho")
                        .WithMany()
                        .HasForeignKey("Nhap_Kho_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BaiTapThucTap.Models.BaiTap3", "sanpham")
                        .WithMany()
                        .HasForeignKey("San_Pham_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NhapKho");

                    b.Navigation("sanpham");
                });

            modelBuilder.Entity("BaiTapThucTap.Models.BaiTap8", b =>
                {
                    b.HasOne("BaiTapThucTap.Models.BaiTap7", "Bai7")
                        .WithMany()
                        .HasForeignKey("Bai7Id");

                    b.HasOne("BaiTapThucTap.Models.BaiTap7_2", "Bai7_2")
                        .WithMany()
                        .HasForeignKey("Bai7_2Id");

                    b.Navigation("Bai7");

                    b.Navigation("Bai7_2");
                });

            modelBuilder.Entity("BaiTapThucTap.Models.BaiTapModel11_2", b =>
                {
                    b.HasOne("BaiTapThucTap.Models.BaiTap3", "sanpham")
                        .WithMany()
                        .HasForeignKey("San_Pham_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BaiTapThucTap.Models.BaiTap11", "XuatKho")
                        .WithMany()
                        .HasForeignKey("Xuat_Kho_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("sanpham");

                    b.Navigation("XuatKho");
                });

            modelBuilder.Entity("BaiTapThucTap.Models.BaiTapModel12", b =>
                {
                    b.HasOne("BaiTapThucTap.Models.BaiTap11", "Bai11")
                        .WithMany()
                        .HasForeignKey("Bai11Id");

                    b.HasOne("BaiTapThucTap.Models.BaiTapModel11_2", "Bai11_2")
                        .WithMany()
                        .HasForeignKey("Bai11_2Id");

                    b.Navigation("Bai11");

                    b.Navigation("Bai11_2");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("BaiTapThucTap.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("BaiTapThucTap.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BaiTapThucTap.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("BaiTapThucTap.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BaiTapThucTap.Models.BaiTap1", b =>
                {
                    b.Navigation("SanPhams");
                });
#pragma warning restore 612, 618
        }
    }
}
