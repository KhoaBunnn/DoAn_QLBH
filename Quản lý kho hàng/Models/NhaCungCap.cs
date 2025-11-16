using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QLKhoHang.Models
{
    public class NhaCungCap
    {
        [Key]
        [StringLength(10)]
        public string MaNCC { get; set; }

        [Required]
        [StringLength(100)]
        public string TenNCC { get; set; }

        [StringLength(20)]
        public string SDT { get; set; }

        [StringLength(255)]
        public string DiaChi { get; set; }

        public ICollection<PhieuNhap> PhieuNhap { get; set; }
    }
}
