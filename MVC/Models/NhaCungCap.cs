using System.ComponentModel.DataAnnotations;
namespace MVC.Models
{
   
    public class NhaCungCap
    {
        public int NhaCungCapId { get; set; }

        [Required(ErrorMessage = "Tên nhà cung cấp không được để trống")]
        public string TenNhaCungCap { get; set; } = string.Empty;

        public string? DiaChi { get; set; }

        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public string? SoDienThoai { get; set; }

        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string? Email { get; set; }

        // Navigation
        public virtual ICollection<ThietBi>? ThietBis { get; set; }
        public virtual ICollection<PhieuNhap>? PhieuNhaps { get; set; }
    }
}
