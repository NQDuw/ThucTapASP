using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BaiTapThucTap.Migrations
{
    public partial class Bai1_Bai13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_DM_Don_Vi_Tinh",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten_Don_Vi_Tinh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ghi_Chu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_DM_Don_Vi_Tinh", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_DM_Kho",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten_Kho = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ghi_Chu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_DM_Kho", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_DM_Loai_San_Pham",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ma_LSP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ten_LSP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ghi_Chu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_DM_Loai_San_Pham", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_DM_NCC",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ma_NCC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ten_NCC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ghi_Chu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_DM_NCC", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_XNK_Xuat_Kho",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ghi_Chu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kho_ID = table.Column<int>(type: "int", nullable: false),
                    NCC_ID = table.Column<int>(type: "int", nullable: false),
                    Ngay_Xuat_Kho = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SL_Xuat = table.Column<int>(type: "int", nullable: false),
                    Don_Gia_Xuat = table.Column<int>(type: "int", nullable: false),
                    San_Pham_ID = table.Column<int>(type: "int", nullable: false),
                    So_Phieu_Xuat_Kho = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_XNK_Xuat_Kho", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_DM_Kho_User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ma_Dang_Nhap = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kho_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_DM_Kho_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_DM_Kho_User_tbl_DM_Kho_Kho_ID",
                        column: x => x.Kho_ID,
                        principalTable: "tbl_DM_Kho",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_DM_Xuat_Kho",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    So_Phieu_Xuat_Kho = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kho_ID = table.Column<int>(type: "int", nullable: false),
                    Ngay_Xuat_Kho = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ghi_Chu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_DM_Xuat_Kho", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_DM_Xuat_Kho_tbl_DM_Kho_Kho_ID",
                        column: x => x.Kho_ID,
                        principalTable: "tbl_DM_Kho",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_DM_San_Pham",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ma_San_Pham = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ten_San_Pham = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Loai_San_Pham_ID = table.Column<int>(type: "int", nullable: false),
                    Don_Vi_Tin_ID = table.Column<int>(type: "int", nullable: false),
                    Ghi_Chu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_DM_San_Pham", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_DM_San_Pham_tbl_DM_Don_Vi_Tinh_Don_Vi_Tin_ID",
                        column: x => x.Don_Vi_Tin_ID,
                        principalTable: "tbl_DM_Don_Vi_Tinh",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_DM_San_Pham_tbl_DM_Loai_San_Pham_Loai_San_Pham_ID",
                        column: x => x.Loai_San_Pham_ID,
                        principalTable: "tbl_DM_Loai_San_Pham",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_DM_Nhap_Kho",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    So_Phieu_Nhap_Kho = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kho_ID = table.Column<int>(type: "int", nullable: false),
                    NCC_ID = table.Column<int>(type: "int", nullable: false),
                    Ngay_Nhap_Kho = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ghi_Chu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_DM_Nhap_Kho", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_DM_Nhap_Kho_tbl_DM_Kho_Kho_ID",
                        column: x => x.Kho_ID,
                        principalTable: "tbl_DM_Kho",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_DM_Nhap_Kho_tbl_DM_NCC_NCC_ID",
                        column: x => x.NCC_ID,
                        principalTable: "tbl_DM_NCC",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_DM_Xuat_Kho_Raw_Data",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Xuat_Kho_ID = table.Column<int>(type: "int", nullable: false),
                    San_Pham_ID = table.Column<int>(type: "int", nullable: false),
                    SL_Xuat = table.Column<int>(type: "int", nullable: false),
                    Don_Gia_Xuat = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_DM_Xuat_Kho_Raw_Data", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_DM_Xuat_Kho_Raw_Data_tbl_DM_San_Pham_San_Pham_ID",
                        column: x => x.San_Pham_ID,
                        principalTable: "tbl_DM_San_Pham",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_DM_Xuat_Kho_Raw_Data_tbl_DM_Xuat_Kho_Xuat_Kho_ID",
                        column: x => x.Xuat_Kho_ID,
                        principalTable: "tbl_DM_Xuat_Kho",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_DM_Nhap_Kho_Raw_Data",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nhap_Kho_ID = table.Column<int>(type: "int", nullable: false),
                    San_Pham_ID = table.Column<int>(type: "int", nullable: false),
                    SL_Nhap = table.Column<int>(type: "int", nullable: false),
                    Don_Gia_Nhap = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_DM_Nhap_Kho_Raw_Data", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_DM_Nhap_Kho_Raw_Data_tbl_DM_Nhap_Kho_Nhap_Kho_ID",
                        column: x => x.Nhap_Kho_ID,
                        principalTable: "tbl_DM_Nhap_Kho",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_DM_Nhap_Kho_Raw_Data_tbl_DM_San_Pham_San_Pham_ID",
                        column: x => x.San_Pham_ID,
                        principalTable: "tbl_DM_San_Pham",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_XNK_Nhap_Kho",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ghi_Chu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kho_ID = table.Column<int>(type: "int", nullable: false),
                    NCC_ID = table.Column<int>(type: "int", nullable: false),
                    Ngay_Nhap_Kho = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SL_Nhap = table.Column<int>(type: "int", nullable: false),
                    Don_Gia_Nhap = table.Column<int>(type: "int", nullable: false),
                    San_Pham_ID = table.Column<int>(type: "int", nullable: false),
                    So_Phieu_Nhap_Kho = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bai7Id = table.Column<int>(type: "int", nullable: true),
                    Bai7_2Id = table.Column<int>(type: "int", nullable: true),
                    TriGia = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_XNK_Nhap_Kho", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_XNK_Nhap_Kho_tbl_DM_Nhap_Kho_Bai7Id",
                        column: x => x.Bai7Id,
                        principalTable: "tbl_DM_Nhap_Kho",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_XNK_Nhap_Kho_tbl_DM_Nhap_Kho_Raw_Data_Bai7_2Id",
                        column: x => x.Bai7_2Id,
                        principalTable: "tbl_DM_Nhap_Kho_Raw_Data",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "tbl_DM_Don_Vi_Tinh",
                columns: new[] { "Id", "Ghi_Chu", "Ten_Don_Vi_Tinh" },
                values: new object[,]
                {
                    { 1, null, "Tấn" },
                    { 2, null, "Kg" },
                    { 3, null, "g" }
                });

            migrationBuilder.InsertData(
                table: "tbl_DM_Kho",
                columns: new[] { "Id", "Ghi_Chu", "Ten_Kho" },
                values: new object[,]
                {
                    { 1, null, "Đông Lạnh A" },
                    { 2, null, "Hầm Chứa B" }
                });

            migrationBuilder.InsertData(
                table: "tbl_DM_Loai_San_Pham",
                columns: new[] { "Id", "Ghi_Chu", "Ma_LSP", "Ten_LSP" },
                values: new object[,]
                {
                    { 1, null, "LSPT", "Thực Phẩm Tươi" },
                    { 2, null, "LSPK", "Thực Phẩm Khô" }
                });

            migrationBuilder.InsertData(
                table: "tbl_DM_NCC",
                columns: new[] { "Id", "Ghi_Chu", "Ma_NCC", "Ten_NCC" },
                values: new object[,]
                {
                    { 1, null, "CTK", "Công Ty Khô" },
                    { 2, null, "CTT", "Công Ty Tươi" }
                });

            migrationBuilder.InsertData(
                table: "tbl_DM_Nhap_Kho",
                columns: new[] { "Id", "Ghi_Chu", "Kho_ID", "NCC_ID", "Ngay_Nhap_Kho", "So_Phieu_Nhap_Kho" },
                values: new object[,]
                {
                    { 1, null, 1, 1, new DateTime(2024, 10, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "SPN1" },
                    { 2, null, 2, 2, new DateTime(2024, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "SPN2" }
                });

            migrationBuilder.InsertData(
                table: "tbl_DM_San_Pham",
                columns: new[] { "Id", "Don_Vi_Tin_ID", "Ghi_Chu", "Loai_San_Pham_ID", "Ma_San_Pham", "Ten_San_Pham" },
                values: new object[,]
                {
                    { 1, 1, null, 1, "TPT1", "Cá" },
                    { 2, 3, null, 1, "TPT2", "Tôm" },
                    { 3, 3, null, 1, "TPT3", "Cua" },
                    { 4, 2, null, 2, "TPK1", "Khô Cá" },
                    { 5, 2, null, 2, "TPK2", "Lạp Xưởng" },
                    { 6, 3, null, 2, "TPK3", "Thịt Khô" }
                });

            migrationBuilder.InsertData(
                table: "tbl_DM_Xuat_Kho",
                columns: new[] { "Id", "Ghi_Chu", "Kho_ID", "Ngay_Xuat_Kho", "So_Phieu_Xuat_Kho" },
                values: new object[,]
                {
                    { 1, null, 1, new DateTime(2024, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "SPX1" },
                    { 2, null, 2, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "SPX2" }
                });

            migrationBuilder.InsertData(
                table: "tbl_DM_Nhap_Kho_Raw_Data",
                columns: new[] { "Id", "Don_Gia_Nhap", "Nhap_Kho_ID", "SL_Nhap", "San_Pham_ID" },
                values: new object[,]
                {
                    { 1, 1500000, 1, 3, 1 },
                    { 2, 8500000, 2, 15, 2 }
                });

            migrationBuilder.InsertData(
                table: "tbl_DM_Xuat_Kho_Raw_Data",
                columns: new[] { "Id", "Don_Gia_Xuat", "SL_Xuat", "San_Pham_ID", "Xuat_Kho_ID" },
                values: new object[,]
                {
                    { 1, 1500000, 3, 1, 1 },
                    { 2, 7600000, 9, 2, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_DM_Kho_User_Kho_ID",
                table: "tbl_DM_Kho_User",
                column: "Kho_ID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_DM_Nhap_Kho_Kho_ID",
                table: "tbl_DM_Nhap_Kho",
                column: "Kho_ID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_DM_Nhap_Kho_NCC_ID",
                table: "tbl_DM_Nhap_Kho",
                column: "NCC_ID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_DM_Nhap_Kho_Raw_Data_Nhap_Kho_ID",
                table: "tbl_DM_Nhap_Kho_Raw_Data",
                column: "Nhap_Kho_ID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_DM_Nhap_Kho_Raw_Data_San_Pham_ID",
                table: "tbl_DM_Nhap_Kho_Raw_Data",
                column: "San_Pham_ID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_DM_San_Pham_Don_Vi_Tin_ID",
                table: "tbl_DM_San_Pham",
                column: "Don_Vi_Tin_ID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_DM_San_Pham_Loai_San_Pham_ID",
                table: "tbl_DM_San_Pham",
                column: "Loai_San_Pham_ID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_DM_Xuat_Kho_Kho_ID",
                table: "tbl_DM_Xuat_Kho",
                column: "Kho_ID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_DM_Xuat_Kho_Raw_Data_San_Pham_ID",
                table: "tbl_DM_Xuat_Kho_Raw_Data",
                column: "San_Pham_ID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_DM_Xuat_Kho_Raw_Data_Xuat_Kho_ID",
                table: "tbl_DM_Xuat_Kho_Raw_Data",
                column: "Xuat_Kho_ID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_XNK_Nhap_Kho_Bai7_2Id",
                table: "tbl_XNK_Nhap_Kho",
                column: "Bai7_2Id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_XNK_Nhap_Kho_Bai7Id",
                table: "tbl_XNK_Nhap_Kho",
                column: "Bai7Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "tbl_DM_Kho_User");

            migrationBuilder.DropTable(
                name: "tbl_DM_Xuat_Kho_Raw_Data");

            migrationBuilder.DropTable(
                name: "tbl_XNK_Nhap_Kho");

            migrationBuilder.DropTable(
                name: "tbl_XNK_Xuat_Kho");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "tbl_DM_Xuat_Kho");

            migrationBuilder.DropTable(
                name: "tbl_DM_Nhap_Kho_Raw_Data");

            migrationBuilder.DropTable(
                name: "tbl_DM_Nhap_Kho");

            migrationBuilder.DropTable(
                name: "tbl_DM_San_Pham");

            migrationBuilder.DropTable(
                name: "tbl_DM_Kho");

            migrationBuilder.DropTable(
                name: "tbl_DM_NCC");

            migrationBuilder.DropTable(
                name: "tbl_DM_Don_Vi_Tinh");

            migrationBuilder.DropTable(
                name: "tbl_DM_Loai_San_Pham");
        }
    }
}
