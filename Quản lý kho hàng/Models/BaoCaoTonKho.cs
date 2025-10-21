using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLKhoHang.Models
{
    public class BaoCaoTonKho
    {
        [Key]
        [StringLength(10)]
        public string MaBCT { get; set; }

        public DateTime NgayLap { get; set; }

        [StringLength(10)]
        public string MaHH { get; set; }

        [ForeignKey("MaHH")]
        public HangHoa HangHoa { get; set; }

        public int SoLuongTon { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal GiaTriTon { get; set; }
    }
}
