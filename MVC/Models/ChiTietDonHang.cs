using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class ChiTietDonHang
    {
        [Key]
        public int ChiTietDonHangId { get; set; }

        [Required]
        public int DonHangId { get; set; }

        [Required]
        public int SanPhamId { get; set; }

        [Range(1, 1000, ErrorMessage = "Số lượng phải từ 1 đến 1000")]
        public int SoLuong { get; set; }

        public virtual DonHang DonHang { get; set; } = null!;
        public virtual SanPham SanPham { get; set; } = null!;
    }
}