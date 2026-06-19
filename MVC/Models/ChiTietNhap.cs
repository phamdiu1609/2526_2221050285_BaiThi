using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class ChiTietNhap
    {
        public int ChiTietNhapId { get; set; }
        public int PhieuNhapId { get; set; }
        public int ThietBiId { get; set; }
        public decimal DonGiaNhap { get; set; }
        public int SoLuong { get; set; }
        public decimal ThanhTien { get; set; }

        public PhieuNhap PhieuNhap { get; set; }
        public ThietBi ThietBi { get; set; }
    }
}
