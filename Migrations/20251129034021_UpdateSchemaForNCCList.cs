using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyKho.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSchemaForNCCList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NCCs");

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

            migrationBuilder.CreateTable(
                name: "NhaCungCap",
                columns: table => new
                {
                    MaNCC = table.Column<string>(type: "varchar(20)", nullable: false),
                    TenNCC = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DienThoai = table.Column<string>(type: "varchar(20)", nullable: false),
                    NoHienTai = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    TongMua = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    TrangThai = table.Column<string>(type: "varchar(50)", nullable: false),
                    MaNhom = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhaCungCap", x => x.MaNCC);
                    table.ForeignKey(
                        name: "FK_NhaCungCap_NhomNhaCungCap_MaNhom",
                        column: x => x.MaNhom,
                        principalTable: "NhomNhaCungCap",
                        principalColumn: "MaNhom",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NhaCungCap_MaNhom",
                table: "NhaCungCap",
                column: "MaNhom");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NhaCungCap");

            migrationBuilder.DropTable(
                name: "NhomNhaCungCap");

            migrationBuilder.CreateTable(
                name: "NCCs",
                columns: table => new
                {
                    NCCId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SDT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenNCC = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NCCs", x => x.NCCId);
                });
        }
    }
}
