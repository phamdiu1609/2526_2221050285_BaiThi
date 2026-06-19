using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class PhieuNhap
    {
        public int PhieuNhapId { get; set; }

        public DateTime NgayNhap { get; set; } = DateTime.Now;

        [Required]
        public int NhaCungCapId { get; set; }

        public decimal TongTien { get; set; }

        public virtual NhaCungCap? NhaCungCap { get; set; }

        public virtual ICollection<ChiTietNhap>? ChiTietNhaps { get; set; }
    }
}