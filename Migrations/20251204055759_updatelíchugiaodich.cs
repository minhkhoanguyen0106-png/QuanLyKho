using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyKho.Migrations
{
    /// <inheritdoc />
    public partial class updatelíchugiaodich : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChiNhanh",
                table: "LichSuGiaoDich");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChiNhanh",
                table: "LichSuGiaoDich",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");
        }
    }
}
