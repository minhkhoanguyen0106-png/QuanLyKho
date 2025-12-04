using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyKho.Migrations
{
    /// <inheritdoc />
    public partial class updatemathamchieuToGiaoDich : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MaThamChieu",
                table: "LichSuGiaoDich",
                type: "varchar(20)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaThamChieu",
                table: "LichSuGiaoDich");
        }
    }
}
