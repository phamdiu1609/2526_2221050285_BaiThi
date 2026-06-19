using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class PhieuXuat
    {
        public int PhieuXuatId { get; set; }

        public DateTime NgayXuat { get; set; } = DateTime.Now;

        public decimal TongTien { get; set; }

        // Navigation
        public virtual ICollection<ChiTietXuat>? ChiTietXuats { get; set; }
    }
}