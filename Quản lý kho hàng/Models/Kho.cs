using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLKhoHang.Models
{
    [Table("Kho")]
    public class Kho
    {
        [Key]
        [StringLength(10)]
        public string MaKho { get; set; }

        [Required]
        [StringLength(100)]
        public string TenKho { get; set; }

        [StringLength(255)]
        public string DiaChiKho { get; set; }

        
    }
}
