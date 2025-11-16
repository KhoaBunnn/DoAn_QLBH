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

        public DbSet<Kho> Kho { get; set; }
        public DbSet<LoaiHang> LoaiHang { get; set; }
        public DbSet<HangHoa> HangHoa { get; set; }
        public DbSet<NhanVien> NhanVien { get; set; }
        public DbSet<KhachHang> KhachHang { get; set; }
        public DbSet<NhaCungCap> NhaCungCap { get; set; }
        public DbSet<PhieuNhap> PhieuNhap { get; set; }
        public DbSet<CT_PhieuNhap> CT_PhieuNhap { get; set; }
        public DbSet<PhieuXuat> PhieuXuat
        {
            get; set;
        }
        public DbSet<CT_PhieuXuat> CT_PhieuXuat { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Khóa chính ghép cho phiếu nhập
            modelBuilder.Entity<CT_PhieuNhap>()
                .HasKey(c => new { c.MaPN, c.MaHang });

            // Khóa chính ghép cho phiếu xuất
            modelBuilder.Entity<CT_PhieuXuat>()
                .HasKey(c => new { c.MaPX, c.MaHang });
        }
    }
}
