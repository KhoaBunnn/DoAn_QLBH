using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLKhoHang.Models
{
    public class CT_PhieuXuat
    {
        [StringLength(10)]
        [Required]
        public string MaPX { get; set; }

        [ForeignKey("MaPX")]
        public PhieuXuat PhieuXuat { get; set; }

        [StringLength(10)]
        [Required]
        public string MaHang { get; set; }

        [ForeignKey("MaHang")]
        public HangHoa HangHoa { get; set; }

        public int SoLuong { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal DonGiaXuat { get; set; }
    }
}
