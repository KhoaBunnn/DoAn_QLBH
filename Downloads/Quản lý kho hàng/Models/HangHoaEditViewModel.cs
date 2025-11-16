using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace QLKhoHang.Models
{
    public class HangHoaEditViewModel
    {
        [Required]
        [StringLength(10)]
        public string MaHang { get; set; }

        [Required]
        [StringLength(100)]
        public string TenHang { get; set; }

        [StringLength(20)]
        public string DonViTinh { get; set; }

        [Range(0, int.MaxValue)]
        public int SoLuongTon { get; set; } = 0;

        [Required]
        [Range(0, double.MaxValue)]
        public decimal GiaNhap { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal GiaXuat { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn loại hàng")]
        public string MaLoai { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn kho")]
        public string MaKho { get; set; }

        // Dropdown lists
        public IEnumerable<SelectListItem> LoaiHangList { get; set; }
        public IEnumerable<SelectListItem> KhoList { get; set; }
    }
}
