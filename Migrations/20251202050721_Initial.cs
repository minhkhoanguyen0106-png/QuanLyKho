using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyKho.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountId);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    MaNV = table.Column<string>(type: "varchar(20)", nullable: false),
                    TenNV = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TrangThai = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.MaNV);
                });

            migrationBuilder.CreateTable(
                name: "HangHoas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaHang = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TenHang = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LoaiHang = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GiaBan = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GiaVon = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TonKho = table.Column<int>(type: "int", nullable: false),
                    KhachDat = table.Column<int>(type: "int", nullable: false),
                    DatNCC = table.Column<int>(type: "int", nullable: false),
                    ThoiGianTao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HangHoas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KhoHangs",
                columns: table => new
                {
                    KhoHangId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenHang = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    DonViTinh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DonGia = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhoHangs", x => x.KhoHangId);
                });

            migrationBuilder.CreateTable(
                name: "NhaCungCap",
                columns: table => new
                {
                    MaNCC = table.Column<string>(type: "varchar(20)", nullable: false),
                    TenNCC = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DienThoai = table.Column<string>(type: "varchar(20)", nullable: false),
                    NoHienTai = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    TongMua = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhaCungCap", x => x.MaNCC);
                });

            migrationBuilder.CreateTable(
                name: "NhanViens",
                columns: table => new
                {
                    NhanVienId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChucVu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SDT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanViens", x => x.NhanVienId);
                });

            migrationBuilder.CreateTable(
                name: "NhapHangs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HangHoaId = table.Column<int>(type: "int", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    DonGia = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    NgayNhap = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhapHangs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NhapHangs_HangHoas_HangHoaId",
                        column: x => x.HangHoaId,
                        principalTable: "HangHoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhieuNhap",
                columns: table => new
                {
                    MaPN = table.Column<string>(type: "varchar(20)", nullable: false),
                    NgayNhap = table.Column<DateTime>(type: "date", nullable: false),
                    TongGiaTri = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    MaNCC = table.Column<string>(type: "varchar(20)", nullable: false),
                    MaNV = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhieuNhap", x => x.MaPN);
                    table.ForeignKey(
                        name: "FK_PhieuNhap_Employee_MaNV",
                        column: x => x.MaNV,
                        principalTable: "Employee",
                        principalColumn: "MaNV",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhieuNhap_NhaCungCap_MaNCC",
                        column: x => x.MaNCC,
                        principalTable: "NhaCungCap",
                        principalColumn: "MaNCC",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhieuXuat",
                columns: table => new
                {
                    MaPX = table.Column<string>(type: "varchar(20)", nullable: false),
                    NgayXuat = table.Column<DateTime>(type: "date", nullable: false),
                    TongGiaTri = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    MaNCC = table.Column<string>(type: "varchar(20)", nullable: false),
                    MaNV = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhieuXuat", x => x.MaPX);
                    table.ForeignKey(
                        name: "FK_PhieuXuat_Employee_MaNV",
                        column: x => x.MaNV,
                        principalTable: "Employee",
                        principalColumn: "MaNV",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhieuXuat_NhaCungCap_MaNCC",
                        column: x => x.MaNCC,
                        principalTable: "NhaCungCap",
                        principalColumn: "MaNCC",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NhapHangs_HangHoaId",
                table: "NhapHangs",
                column: "HangHoaId");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuNhap_MaNCC",
                table: "PhieuNhap",
                column: "MaNCC");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuNhap_MaNV",
                table: "PhieuNhap",
                column: "MaNV");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuXuat_MaNCC",
                table: "PhieuXuat",
                column: "MaNCC");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuXuat_MaNV",
                table: "PhieuXuat",
                column: "MaNV");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "KhoHangs");

            migrationBuilder.DropTable(
                name: "NhanViens");

            migrationBuilder.DropTable(
                name: "NhapHangs");

            migrationBuilder.DropTable(
                name: "PhieuNhap");

            migrationBuilder.DropTable(
                name: "PhieuXuat");

            migrationBuilder.DropTable(
                name: "HangHoas");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "NhaCungCap");
        }
    }
}
