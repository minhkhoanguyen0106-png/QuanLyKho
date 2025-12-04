using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyKho.Migrations
{
    /// <inheritdoc />
    public partial class Fix_KiemKeKho_FK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KhoHangs");

            migrationBuilder.CreateTable(
                name: "KiemKeKhos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HangHoaId = table.Column<int>(type: "int", nullable: false),
                    MaHang = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Kho = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TonHeThong = table.Column<int>(type: "int", nullable: false),
                    TonThucTe = table.Column<int>(type: "int", nullable: false),
                    NgayKiemKe = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KiemKeKhos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KiemKeKhos_HangHoas_HangHoaId",
                        column: x => x.HangHoaId,
                        principalTable: "HangHoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KiemKeKhos_HangHoaId",
                table: "KiemKeKhos",
                column: "HangHoaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KiemKeKhos");

            migrationBuilder.CreateTable(
                name: "KhoHangs",
                columns: table => new
                {
                    KhoHangId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DonGia = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DonViTinh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    TenHang = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhoHangs", x => x.KhoHangId);
                });
        }
    }
}
