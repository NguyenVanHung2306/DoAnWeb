using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAnLapTrinhWeb.Migrations
{
    /// <inheritdoc />
    public partial class Updatedtb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbTrang_tbSach_SachId",
                table: "tbTrang");

            migrationBuilder.AlterColumn<int>(
                name: "SachId",
                table: "tbTrang",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Noidung",
                table: "tbTrang",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "moTa",
                table: "tbSach",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_tbTrang_tbSach_SachId",
                table: "tbTrang",
                column: "SachId",
                principalTable: "tbSach",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbTrang_tbSach_SachId",
                table: "tbTrang");

            migrationBuilder.AlterColumn<int>(
                name: "SachId",
                table: "tbTrang",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Noidung",
                table: "tbTrang",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "moTa",
                table: "tbSach",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tbTrang_tbSach_SachId",
                table: "tbTrang",
                column: "SachId",
                principalTable: "tbSach",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
