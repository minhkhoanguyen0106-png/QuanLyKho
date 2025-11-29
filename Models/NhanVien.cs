// File: Employee.cs

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyKho.Models;

[Table("Employee")] 
public class Employee
{
    [Key]
    [Column(TypeName = "varchar(20)")]
    public string MaNV { get; set; }

    [Required]
    [MaxLength(100)]
    public string TenNV { get; set; } // Tương đương 'Nguyễn Văn Cử', 'Trần Mỹ'

    [Required]
    [Column(TypeName = "varchar(50)")]
    public string TrangThai { get; set; } // Cần có thông tin trạng thái hoạt động

    // Các trường khác như Điện thoại, Địa chỉ... có thể được thêm vào sau.
}