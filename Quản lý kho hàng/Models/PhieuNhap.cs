using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLKhoHang.Models
{
    public class PhieuNhap
    {
        [Key]
        [StringLength(10)]
        public string MaPN { get; set; }

        public DateTime NgayNhap { get; set; } = DateTime.Now;

        [StringLength(10)]
        public string MaNV { get; set; }

        [ForeignKey("MaNV")]
        public NhanVien NhanVien { get; set; }

        [StringLength(10)]
        public string MaNCC { get; set; }

        [ForeignKey("MaNCC")]
        public NhaCungCap NhaCungCap { get; set; }

        public ICollection<CT_PhieuNhap> CT_PhieuNhap { get; set; }
    }
}
