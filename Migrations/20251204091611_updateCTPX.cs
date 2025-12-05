using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyKho.Migrations
{
    /// <inheritdoc />
    public partial class updateCTPX : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietPhieuXuat_PhieuNhap_MaPX",
                table: "ChiTietPhieuXuat");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietPhieuXuat_PhieuXuat_MaPX",
                table: "ChiTietPhieuXuat",
                column: "MaPX",
                principalTable: "PhieuXuat",
                principalColumn: "MaPX",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietPhieuXuat_PhieuXuat_MaPX",
                table: "ChiTietPhieuXuat");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietPhieuXuat_PhieuNhap_MaPX",
                table: "ChiTietPhieuXuat",
                column: "MaPX",
                principalTable: "PhieuNhap",
                principalColumn: "MaPN",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
