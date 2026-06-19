using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class ThietBi
    {
        public int ThietBiId { get; set; }

        [Required(ErrorMessage = "Tên thiết bị không được để trống")]
        public string TenThietBi { get; set; } = string.Empty;

        // Khóa ngoại
        [Required]
        public int LoaiThietBiId { get; set; }

        [Required]
        public int NhaCungCapId { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Giá phải >= 0")]
        public decimal GiaBan { get; set; }

        [Range(0, int.MaxValue)]
        public int SoLuongTon { get; set; }

        // Navigation
        public virtual LoaiThietBi? LoaiThietBi { get; set; }
        public virtual NhaCungCap? NhaCungCap { get; set; }

        public virtual ICollection<ChiTietNhap>? ChiTietNhaps { get; set; }
        public virtual ICollection<ChiTietXuat>? ChiTietXuats { get; set; }
    }
}