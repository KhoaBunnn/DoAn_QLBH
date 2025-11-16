using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QLKhoHang.Models
{
    public class NhanVien
    {
        [Key]
        [StringLength(10)]
        public string MaNV { get; set; }

        [StringLength(100)]
        public string TenNV { get; set; }

        [StringLength(20)]
        public string SDT { get; set; }

        [StringLength(255)]
        public string DiaChi { get; set; }

        public ICollection<PhieuNhap> PhieuNhap { get; set; }
        public ICollection<PhieuXuat> PhieuXuat { get; set; }
    }
}
