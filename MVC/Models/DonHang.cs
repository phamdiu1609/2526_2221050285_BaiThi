using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class DonHang
    {
        [Key]
        public int DonHangId { get; set; }

        public DateTime NgayDat { get; set; } = DateTime.Now;

        [Required]
        public int KhachHangId { get; set; }

        public virtual KhachHang KhachHang { get; set; } = null!;

        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();
    }
}