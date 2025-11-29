using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyKho.Migrations
{
    /// <inheritdoc />
    public partial class RemoveMaNhomFromNCC : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NhaCungCap_NhomNhaCungCap_MaNhom",
                table: "NhaCungCap");

            migrationBuilder.DropTable(
                name: "NhomNhaCungCap");

            migrationBuilder.DropIndex(
                name: "IX_NhaCungCap_MaNhom",
                table: "NhaCungCap");

            migrationBuilder.DropColumn(
                name: "MaNhom",
                table: "NhaCungCap");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaNhom",
                table: "NhaCungCap",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "NhomNhaCungCap",
                columns: table => new
                {
                    MaNhom = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenNhom = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhomNhaCungCap", x => x.MaNhom);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NhaCungCap_MaNhom",
                table: "NhaCungCap",
                column: "MaNhom");

            migrationBuilder.AddForeignKey(
                name: "FK_NhaCungCap_NhomNhaCungCap_MaNhom",
                table: "NhaCungCap",
                column: "MaNhom",
                principalTable: "NhomNhaCungCap",
                principalColumn: "MaNhom",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
