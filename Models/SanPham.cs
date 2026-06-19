using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class SanPham
    {
        [Key]
        public int SanPhamId { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        public string TenSanPham { get; set; } = string.Empty;

        [Range(0, double.MaxValue, ErrorMessage = "Giá phải >= 0")]
        public double Gia { get; set; }

        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();
    }
}