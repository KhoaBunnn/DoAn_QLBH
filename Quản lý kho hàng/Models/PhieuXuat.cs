using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLKhoHang.Models
{
    public class PhieuXuat
    {
        [Key]
        [StringLength(10)]
        public string MaPX { get; set; }

        public DateTime NgayXuat { get; set; } = DateTime.Now;

        [StringLength(10)]
        public string MaNV { get; set; }

        [ForeignKey("MaNV")]
        public NhanVien NhanVien { get; set; }

        [StringLength(10)]
        public string MaKH { get; set; }

        [ForeignKey("MaKH")]
        public KhachHang KhachHang { get; set; }

        public ICollection<CT_PhieuXuat> CT_PhieuXuat { get; set; }
    }
}
