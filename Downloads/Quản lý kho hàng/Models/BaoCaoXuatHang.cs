using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLKhoHang.Models
{
    public class BaoCaoXuatHang
    {
        [Key]
        [StringLength(10)]
        public string MaBCX { get; set; }

        public DateTime NgayLap { get; set; }

        [StringLength(10)]
        public string MaHH { get; set; }

        [ForeignKey("MaHH")]
        public HangHoa HangHoa { get; set; }

        public int TongSoLuongXuat { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TongGiaTriXuat { get; set; }

        [StringLength(200)]
        public string GhiChu { get; set; }
    }
}
