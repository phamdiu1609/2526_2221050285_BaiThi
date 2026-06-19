using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class Student
    {
        [Key] // 👈 THÊM DÒNG NÀY
        [Required(ErrorMessage = "Mã sinh viên không được để trống")]
        [StringLength(10, ErrorMessage = "Mã SV tối đa 10 ký tự")]
        public string StudentCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Họ tên không được để trống")]
        [StringLength(50, ErrorMessage = "Tên tối đa 50 ký tự")]
        public string FullName { get; set; } = string.Empty;

        [Range(18, 60, ErrorMessage = "Tuổi phải từ 18 đến 60")]
        public int Age { get; set; }

        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; } = string.Empty;
    }
}