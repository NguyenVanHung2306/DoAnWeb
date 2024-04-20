using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAnLapTrinhWeb.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "binhluan",
                table: "tbPhieuDanhGia",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "binhluan",
                table: "tbPhieuDanhGia");
        }
    }
}
