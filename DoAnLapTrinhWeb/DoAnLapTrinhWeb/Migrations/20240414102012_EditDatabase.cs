using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAnLapTrinhWeb.Migrations
{
    /// <inheritdoc />
    public partial class EditDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbChiTietTheLoai");

            migrationBuilder.AddColumn<int>(
                name: "theLoaiId",
                table: "tbSach",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tbSach_theLoaiId",
                table: "tbSach",
                column: "theLoaiId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbSach_tbTheLoai_theLoaiId",
                table: "tbSach",
                column: "theLoaiId",
                principalTable: "tbTheLoai",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbSach_tbTheLoai_theLoaiId",
                table: "tbSach");

            migrationBuilder.DropIndex(
                name: "IX_tbSach_theLoaiId",
                table: "tbSach");

            migrationBuilder.DropColumn(
                name: "theLoaiId",
                table: "tbSach");

            migrationBuilder.CreateTable(
                name: "tbChiTietTheLoai",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sachId = table.Column<int>(type: "int", nullable: false),
                    theLoaiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbChiTietTheLoai", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbChiTietTheLoai_tbSach_sachId",
                        column: x => x.sachId,
                        principalTable: "tbSach",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbChiTietTheLoai_tbTheLoai_theLoaiId",
                        column: x => x.theLoaiId,
                        principalTable: "tbTheLoai",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbChiTietTheLoai_sachId",
                table: "tbChiTietTheLoai",
                column: "sachId");

            migrationBuilder.CreateIndex(
                name: "IX_tbChiTietTheLoai_theLoaiId",
                table: "tbChiTietTheLoai",
                column: "theLoaiId");
        }
    }
}
