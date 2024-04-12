using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAnLapTrinhWeb.Migrations
{
    /// <inheritdoc />
    public partial class ConnectUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbChiTietTheLoai_AspNetUsers_userId",
                table: "tbChiTietTheLoai");

            migrationBuilder.DropIndex(
                name: "IX_tbChiTietTheLoai_userId",
                table: "tbChiTietTheLoai");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "tbChiTietTheLoai");

            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "tbPhieuDanhGia",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_tbPhieuDanhGia_userId",
                table: "tbPhieuDanhGia",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbPhieuDanhGia_AspNetUsers_userId",
                table: "tbPhieuDanhGia",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbPhieuDanhGia_AspNetUsers_userId",
                table: "tbPhieuDanhGia");

            migrationBuilder.DropIndex(
                name: "IX_tbPhieuDanhGia_userId",
                table: "tbPhieuDanhGia");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "tbPhieuDanhGia");

            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "tbChiTietTheLoai",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_tbChiTietTheLoai_userId",
                table: "tbChiTietTheLoai",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbChiTietTheLoai_AspNetUsers_userId",
                table: "tbChiTietTheLoai",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
