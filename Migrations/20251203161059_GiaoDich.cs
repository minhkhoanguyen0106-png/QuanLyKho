using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyKho.Migrations
{
    /// <inheritdoc />
    public partial class GiaoDich : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TongGiaTri",
                table: "PhieuXuat",
                type: "decimal(18,0)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayXuat",
                table: "PhieuXuat",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<string>(
                name: "GhiChu",
                table: "PhieuXuat",
                type: "nvarchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<decimal>(
                name: "TongGiaTri",
                table: "PhieuNhap",
                type: "decimal(18,0)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayNhap",
                table: "PhieuNhap",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<string>(
                name: "GhiChu",
                table: "PhieuNhap",
                type: "nvarchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<string>(
                name: "MaPhieuDat",
                table: "PhieuNhap",
                type: "varchar(20)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DatHangNhap",
                columns: table => new
                {
                    MaPhieuDat = table.Column<string>(type: "varchar(20)", nullable: false),
                    ThoiGian = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaNCC = table.Column<string>(type: "varchar(20)", nullable: false),
                    NgayNhapDuKien = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SoNgayCho = table.Column<int>(type: "int", nullable: false),
                    TongTien = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatHangNhap", x => x.MaPhieuDat);
                    table.ForeignKey(
                        name: "FK_DatHangNhap_NhaCungCap_MaNCC",
                        column: x => x.MaNCC,
                        principalTable: "NhaCungCap",
                        principalColumn: "MaNCC",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LichSuGiaoDich",
                columns: table => new
                {
                    MaGiaoDich = table.Column<string>(type: "varchar(20)", nullable: false),
                    LoaiGiaoDich = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    ThoiGian = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DoiTac = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    ChiNhanh = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    GiaTri = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NguoiThucHien = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LichSuGiaoDich", x => x.MaGiaoDich);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhieuNhap_MaPhieuDat",
                table: "PhieuNhap",
                column: "MaPhieuDat",
                unique: true,
                filter: "[MaPhieuDat] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DatHangNhap_MaNCC",
                table: "DatHangNhap",
                column: "MaNCC");

            migrationBuilder.AddForeignKey(
                name: "FK_PhieuNhap_DatHangNhap_MaPhieuDat",
                table: "PhieuNhap",
                column: "MaPhieuDat",
                principalTable: "DatHangNhap",
                principalColumn: "MaPhieuDat");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhieuNhap_DatHangNhap_MaPhieuDat",
                table: "PhieuNhap");

            migrationBuilder.DropTable(
                name: "DatHangNhap");

            migrationBuilder.DropTable(
                name: "LichSuGiaoDich");

            migrationBuilder.DropIndex(
                name: "IX_PhieuNhap_MaPhieuDat",
                table: "PhieuNhap");

            migrationBuilder.DropColumn(
                name: "MaPhieuDat",
                table: "PhieuNhap");

            migrationBuilder.AlterColumn<decimal>(
                name: "TongGiaTri",
                table: "PhieuXuat",
                type: "decimal(18,0)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayXuat",
                table: "PhieuXuat",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GhiChu",
                table: "PhieuXuat",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TongGiaTri",
                table: "PhieuNhap",
                type: "decimal(18,0)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayNhap",
                table: "PhieuNhap",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GhiChu",
                table: "PhieuNhap",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldNullable: true);
        }
    }
}
