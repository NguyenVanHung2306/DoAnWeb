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
        public ApplicationDbContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-BGSRQD70\\SQLEXPRESS;Initial Catalog=Book-ThucHien;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            }
        }
        public DbSet<tbSach> tbSach { get; set; }
        public DbSet<tbTacGia> tbTacGia { get; set; }
        public DbSet<tbTheLoai> tbTheLoai { get; set; }
        public DbSet<tbLoaiDanhDau> tbLoaiDanhDau { get; set; }
        public DbSet<tbChiTietDanhDau> tbChiTietDanhDau { get; set; }
        public DbSet<tbLichSu> tbLichSu { get; set; }
        public DbSet<tbPhieuDanhGia> tbPhieuDanhGia { get; set; }
        public DbSet<TbTrang> tbTrang { get; set; }

    }
}
