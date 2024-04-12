using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAnLapTrinhWeb.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbLoaiDanhDau",
                columns: table => new
                {
                    MaLoaiDanhDau = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDanhDau = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbLoaiDanhDau", x => x.MaLoaiDanhDau);
                });

            migrationBuilder.CreateTable(
                name: "tbTacGia",
                columns: table => new
                {
                    TacGiaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTacGia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbTacGia", x => x.TacGiaId);
                });

            migrationBuilder.CreateTable(
                name: "tbTheLoai",
                columns: table => new
                {
                    maTheLoai = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tenTheLoai = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbTheLoai", x => x.maTheLoai);
                });

            migrationBuilder.CreateTable(
                name: "tbSach",
                columns: table => new
                {
                    sachId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tenSach = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    fileUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    imageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tacGiaId = table.Column<int>(type: "int", nullable: false),
                    tbTacGiaTacGiaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbSach", x => x.sachId);
                    table.ForeignKey(
                        name: "FK_tbSach_tbTacGia_tbTacGiaTacGiaId",
                        column: x => x.tbTacGiaTacGiaId,
                        principalTable: "tbTacGia",
                        principalColumn: "TacGiaId");
                });

            migrationBuilder.CreateTable(
                name: "tbChiTietDanhDau",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sachId = table.Column<int>(type: "int", nullable: false),
                    tbSachsachId = table.Column<int>(type: "int", nullable: false),
                    maLoaiDanhDau = table.Column<int>(type: "int", nullable: false),
                    tbLoaiDanhDauMaLoaiDanhDau = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbChiTietDanhDau", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbChiTietDanhDau_tbLoaiDanhDau_tbLoaiDanhDauMaLoaiDanhDau",
                        column: x => x.tbLoaiDanhDauMaLoaiDanhDau,
                        principalTable: "tbLoaiDanhDau",
                        principalColumn: "MaLoaiDanhDau",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbChiTietDanhDau_tbSach_tbSachsachId",
                        column: x => x.tbSachsachId,
                        principalTable: "tbSach",
                        principalColumn: "sachId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbChiTietTheLoai",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sachId = table.Column<int>(type: "int", nullable: false),
                    tbSachsachId = table.Column<int>(type: "int", nullable: false),
                    maTheLoai = table.Column<int>(type: "int", nullable: false),
                    tbTheLoaimaTheLoai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbChiTietTheLoai", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbChiTietTheLoai_tbSach_tbSachsachId",
                        column: x => x.tbSachsachId,
                        principalTable: "tbSach",
                        principalColumn: "sachId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbChiTietTheLoai_tbTheLoai_tbTheLoaimaTheLoai",
                        column: x => x.tbTheLoaimaTheLoai,
                        principalTable: "tbTheLoai",
                        principalColumn: "maTheLoai",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbLichSu",
                columns: table => new
                {
                    lichSuId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    thoiGianDoc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    sachId = table.Column<int>(type: "int", nullable: false),
                    tbSachsachId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbLichSu", x => x.lichSuId);
                    table.ForeignKey(
                        name: "FK_tbLichSu_tbSach_tbSachsachId",
                        column: x => x.tbSachsachId,
                        principalTable: "tbSach",
                        principalColumn: "sachId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbPhieuDanhGia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sachId = table.Column<int>(type: "int", nullable: false),
                    tbSachsachId = table.Column<int>(type: "int", nullable: false),
                    diem = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbPhieuDanhGia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbPhieuDanhGia_tbSach_tbSachsachId",
                        column: x => x.tbSachsachId,
                        principalTable: "tbSach",
                        principalColumn: "sachId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbChiTietDanhDau_tbLoaiDanhDauMaLoaiDanhDau",
                table: "tbChiTietDanhDau",
                column: "tbLoaiDanhDauMaLoaiDanhDau");

            migrationBuilder.CreateIndex(
                name: "IX_tbChiTietDanhDau_tbSachsachId",
                table: "tbChiTietDanhDau",
                column: "tbSachsachId");

            migrationBuilder.CreateIndex(
                name: "IX_tbChiTietTheLoai_tbSachsachId",
                table: "tbChiTietTheLoai",
                column: "tbSachsachId");

            migrationBuilder.CreateIndex(
                name: "IX_tbChiTietTheLoai_tbTheLoaimaTheLoai",
                table: "tbChiTietTheLoai",
                column: "tbTheLoaimaTheLoai");

            migrationBuilder.CreateIndex(
                name: "IX_tbLichSu_tbSachsachId",
                table: "tbLichSu",
                column: "tbSachsachId");

            migrationBuilder.CreateIndex(
                name: "IX_tbPhieuDanhGia_tbSachsachId",
                table: "tbPhieuDanhGia",
                column: "tbSachsachId");

            migrationBuilder.CreateIndex(
                name: "IX_tbSach_tbTacGiaTacGiaId",
                table: "tbSach",
                column: "tbTacGiaTacGiaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbChiTietDanhDau");

            migrationBuilder.DropTable(
                name: "tbChiTietTheLoai");

            migrationBuilder.DropTable(
                name: "tbLichSu");

            migrationBuilder.DropTable(
                name: "tbPhieuDanhGia");

            migrationBuilder.DropTable(
                name: "tbLoaiDanhDau");

            migrationBuilder.DropTable(
                name: "tbTheLoai");

            migrationBuilder.DropTable(
                name: "tbSach");

            migrationBuilder.DropTable(
                name: "tbTacGia");
        }
    }
}
