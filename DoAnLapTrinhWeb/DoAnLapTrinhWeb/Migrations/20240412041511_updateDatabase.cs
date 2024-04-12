using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAnLapTrinhWeb.Migrations
{
    /// <inheritdoc />
    public partial class updateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbTrang_tbSach_SachId",
                table: "TbTrang");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TbTrang",
                table: "TbTrang");

            migrationBuilder.DropColumn(
                name: "fileUrl",
                table: "tbSach");

            migrationBuilder.RenameTable(
                name: "TbTrang",
                newName: "tbTrang");

            migrationBuilder.RenameIndex(
                name: "IX_TbTrang_SachId",
                table: "tbTrang",
                newName: "IX_tbTrang_SachId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbTrang",
                table: "tbTrang",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tbTrang_tbSach_SachId",
                table: "tbTrang",
                column: "SachId",
                principalTable: "tbSach",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbTrang_tbSach_SachId",
                table: "tbTrang");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tbTrang",
                table: "tbTrang");

            migrationBuilder.RenameTable(
                name: "tbTrang",
                newName: "TbTrang");

            migrationBuilder.RenameIndex(
                name: "IX_tbTrang_SachId",
                table: "TbTrang",
                newName: "IX_TbTrang_SachId");

            migrationBuilder.AddColumn<string>(
                name: "fileUrl",
                table: "tbSach",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TbTrang",
                table: "TbTrang",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TbTrang_tbSach_SachId",
                table: "TbTrang",
                column: "SachId",
                principalTable: "tbSach",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
