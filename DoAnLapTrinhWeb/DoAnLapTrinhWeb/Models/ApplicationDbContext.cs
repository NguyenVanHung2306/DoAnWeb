using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DoAnLapTrinhWeb.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) : base(options)
        {
        }

        public DbSet<tbSach> tbSach { get; set; }
        public DbSet<tbTacGia> tbTacGia { get; set; }
        public DbSet<tbTheLoai> tbTheLoai { get; set; }
        public DbSet<tbChiTietTheLoai> tbChiTietTheLoai { get; set; }
        public DbSet<tbLoaiDanhDau> tbLoaiDanhDau { get; set; }
        public DbSet<tbChiTietDanhDau> tbChiTietDanhDau { get; set; }
        public DbSet<tbLichSu> tbLichSu { get; set; }
        public DbSet<tbPhieuDanhGia> tbPhieuDanhGia { get; set; }
        public DbSet<TbTrang> tbTrang { get; set; }

    }
}
