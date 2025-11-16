using System.ComponentModel.DataAnnotations;

namespace QLKhoHang.Models
{
    public class LoaiHangCreateViewModel
    {
        [Required]
        [StringLength(100)]
        public string TenLoai { get; set; }
    }
}
