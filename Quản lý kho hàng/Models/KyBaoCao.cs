using System;
using System.ComponentModel.DataAnnotations;

namespace QLKhoHang.Models
{
    public class KyBaoCao
    {
        [Key]
        [StringLength(10)]
        public string MaKy { get; set; }

        public DateTime TuNgay { get; set; }

        public DateTime DenNgay { get; set; }

        [StringLength(100)]
        public string MoTa { get; set; }
    }
}
