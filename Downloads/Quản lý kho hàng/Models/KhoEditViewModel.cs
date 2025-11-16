using System.ComponentModel.DataAnnotations;

public class KhoEditViewModel
{
    [Required]
    [StringLength(100)]
    public string TenKho { get; set; }

    [StringLength(255)]
    public string DiaChiKho { get; set; }

    public string MaKho { get; set; } // hidden
}
