using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyKho.Migrations
{
    /// <inheritdoc />
    public partial class addDienThoaiTrangThaiToEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DienThoai",
                table: "Employee",
                type: "varchar(20)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ViTri",
                table: "Employee",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DienThoai",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "ViTri",
                table: "Employee");
        }
    }
}
