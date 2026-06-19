using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class KhachHang
    {
        [Key]
        public int KhachHangId { get; set; }

        [Required(ErrorMessage = "Tên không được để trống")]
        public string Ten { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string? Email { get; set; }

        public virtual ICollection<DonHang> DonHangs { get; set; } = new List<DonHang>();
    }
}