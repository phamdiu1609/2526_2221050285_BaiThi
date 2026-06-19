using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class ChiTietXuat
    {
        public int ChiTietXuatId { get; set; }

        [Required]
        public int PhieuXuatId { get; set; }

        [Required]
        public int ThietBiId { get; set; }

        [Range(0, double.MaxValue)]
        public decimal DonGiaXuat { get; set; }

        [Range(1, int.MaxValue)]
        public int SoLuong { get; set; }

        // 👉 Tính tự động (không lưu DB nếu không cần)
        public decimal ThanhTien => DonGiaXuat * SoLuong;

        // Navigation
        public virtual PhieuXuat? PhieuXuat { get; set; }
        public virtual ThietBi? ThietBi { get; set; }
    }
}