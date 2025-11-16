using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace QLKhoHang.Models
{
    public class KhachHang
    {
        [Key]
        [StringLength(10)]
        [BindNever]
        public string MaKH { get; set; }

        [Required(ErrorMessage = "Tên khách hàng không được để trống")]
        [StringLength(100)]
        public string TenKH { get; set; }

        [StringLength(20)]
        public string SDT { get; set; }

        [StringLength(255)]
        public string DiaChi { get; set; }

        [BindNever]
        public ICollection<PhieuXuat> PhieuXuat { get; set; }
    }
}