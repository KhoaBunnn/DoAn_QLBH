using Microsoft.EntityFrameworkCore;
using QLKhoHang.Models;

namespace QLKhoHang.Data
{
    public class QLKhoHangContext : DbContext
    {
        public QLKhoHangContext(DbContextOptions<QLKhoHangContext> options)
            : base(options)
        {
        }

        public DbSet<Kho> Khos { get; set; }
        public DbSet<LoaiHang> LoaiHangs { get; set; }
        public DbSet<HangHoa> HangHoas { get; set; }
        public DbSet<NhanVien> NhanViens { get; set; }
        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<NhaCungCap> NhaCungCaps { get; set; }
        public DbSet<PhieuNhap> PhieuNhaps { get; set; }
        public DbSet<CT_PhieuNhap> CT_PhieuNhaps { get; set; }
        public DbSet<PhieuXuat> PhieuXuats { get; set; }
        public DbSet<CT_PhieuXuat> CT_PhieuXuats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CT_PhieuNhap>().HasKey(c => new { c.MaPN, c.MaHang });
            modelBuilder.Entity<CT_PhieuXuat>().HasKey(c => new { c.MaPX, c.MaHang });
        }
    }
}
