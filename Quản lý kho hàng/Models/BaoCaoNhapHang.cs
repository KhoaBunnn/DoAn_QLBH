using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLKhoHang.Models
{
    public class BaoCaoNhapHang
    {
        [Key]
        [StringLength(10)]
        public string MaBCN { get; set; }

        public DateTime NgayLap { get; set; }

        [StringLength(10)]
        public string MaHH { get; set; }

        [ForeignKey("MaHH")]
        public HangHoa HangHoa { get; set; }

        public int TongSoLuongNhap { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TongGiaTriNhap { get; set; }

        [StringLength(200)]
        public string GhiChu { get; set; }
    }
}
