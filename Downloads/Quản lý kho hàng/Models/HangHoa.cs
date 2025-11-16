using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLKhoHang.Models
{
    public class HangHoa
    {
        [Key]
        [StringLength(10)]
        public string MaHang { get; set; }

        [Required]
        [StringLength(100)]
        public string TenHang { get; set; }

        [StringLength(20)]
        public string DonViTinh { get; set; }

        public int SoLuongTon { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal GiaNhap { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal GiaXuat { get; set; }

        [StringLength(10)]
        public string MaLoai { get; set; }

        [ForeignKey("MaLoai")]
        public LoaiHang LoaiHang { get; set; }

        [StringLength(10)]
        public string MaKho { get; set; }

        [ForeignKey("MaKho")]
        public Kho Kho { get; set; }

        public ICollection<CT_PhieuNhap> CT_PhieuNhap { get; set; }
        public ICollection<CT_PhieuXuat> CT_PhieuXuat { get; set; }
        public ICollection<BaoCaoNhapHang> BaoCaoNhapHang { get; set; }
        public ICollection<BaoCaoXuatHang> BaoCaoXuatHang { get; set; }
        public ICollection<BaoCaoTonKho> BaoCaoTonKho { get; set; }
    }
}
