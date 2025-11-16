using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace QLKhoHang.Models
{
    [Table("Kho")]
    public class Kho
    {
        [Key]
        [StringLength(10)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [BindNever]// Không tự validate
        public string MaKho { get; set; }

        [Required]
        [StringLength(100)]
        public string TenKho { get; set; }

        [StringLength(255)]
        public string DiaChiKho { get; set; }

        [BindNever] // Bỏ bind collection từ form
        public ICollection<HangHoa> HangHoas { get; set; }
    }
}
