using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
     public class LoaiThietBi
    {
        public int LoaiThietBiId { get; set; }

        [Required(ErrorMessage = "Tên loại không được để trống")]
        [StringLength(100)]
        public string TenLoai { get; set; } = string.Empty;

        public string? MoTa { get; set; }

        // Navigation
        public virtual ICollection<ThietBi>? ThietBis { get; set; }
    }
}