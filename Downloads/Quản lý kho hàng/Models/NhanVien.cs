using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace QLKhoHang.Models
{
    public class NhanVien
    {
        [Key]
        [StringLength(10)]
        [BindNever] // QUAN TRỌNG: Không bind từ form
        public string MaNV { get; set; }

        [Required(ErrorMessage = "Tên nhân viên không được để trống")]
        [StringLength(100)]
        public string TenNV { get; set; }

        [StringLength(20)]
        public string SDT { get; set; }

        [StringLength(255)]
        public string DiaChi { get; set; }

        [BindNever] // QUAN TRỌNG: Không validate collection
        public ICollection<PhieuNhap> PhieuNhap { get; set; }

        [BindNever] // QUAN TRỌNG: Không validate collection
        public ICollection<PhieuXuat> PhieuXuat { get; set; }
    }
}