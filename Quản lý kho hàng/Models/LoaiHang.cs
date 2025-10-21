using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QLKhoHang.Models
{
    public class LoaiHang
    {
        [Key]
        [StringLength(10)]
        public string MaLoai { get; set; }

        [Required]
        [StringLength(100)]
        public string TenLoai { get; set; }

        public ICollection<HangHoa> HangHoa { get; set; }
    }
}
