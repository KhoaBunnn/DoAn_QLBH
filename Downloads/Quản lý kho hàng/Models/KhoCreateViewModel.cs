using System.ComponentModel.DataAnnotations;

public class KhoCreateViewModel
{
    [Required]
    [StringLength(100)]
    public string TenKho { get; set; }

    [StringLength(255)]
    public string DiaChiKho { get; set; }
}
