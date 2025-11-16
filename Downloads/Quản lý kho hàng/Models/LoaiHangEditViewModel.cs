using System.ComponentModel.DataAnnotations;

namespace QLKhoHang.Models
{
    public class LoaiHangEditViewModel
    {
        [Required]
        [StringLength(10)]
        public string MaLoai { get; set; }  // Giữ nguyên, không chỉnh sửa trong view nhưng cần bind để POST

        [Required(ErrorMessage = "Tên loại hàng không được để trống.")]
        [StringLength(100, ErrorMessage = "Tên loại hàng tối đa 100 ký tự.")]
        [Display(Name = "Tên loại hàng")]
        public string TenLoai { get; set; }
    }
}
