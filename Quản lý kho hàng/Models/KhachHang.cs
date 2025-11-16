using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QLKhoHang.Models
{
    public class KhachHang
    {
        [Key]
        [StringLength(10)]
        public string MaKH { get; set; }

        [StringLength(100)]
        public string TenKH { get; set; }

        [StringLength(20)]
        public string SDT { get; set; }

        [StringLength(255)]
        public string DiaChi { get; set; }

        public ICollection<PhieuXuat> PhieuXuat { get; set; }
    }
}
