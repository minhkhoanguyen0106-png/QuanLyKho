// File: PhieuXuat.cs

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyKho.Models;

[Table("PhieuXuat")] 
public class PhieuXuat
{
    [Key]
    [Column(TypeName = "varchar(20)")]
    public string MaPX { get; set; } // Tương đương 'Mã phiếu' (PX0001, PC0001)

    [Required]
    [Column(TypeName = "date")]
    public DateTime NgayXuat { get; set; } // Tương đương 'Ngày xuất' (02/11/2025)

    [Required]
    [Column(TypeName = "decimal(18, 0)")]
    public decimal TongGiaTri { get; set; } // Tương đương 'Tổng giá trị xuất' (12350000)

    [MaxLength(500)]
    public string GhiChu { get; set; } // Tương đương 'Ghi chú' (Xuất pin và sạc)

    [Required]
    [Column(TypeName = "varchar(50)")]
    public string TrangThai { get; set; } // Cần thiết cho logic lọc Trạng thái

    // --- FOREIGN KEY ---

    // 1. Khóa ngoại đến Nhà Cung Cấp (NCC)
    [Required]
    [Column(TypeName = "varchar(20)")]
    public string MaNCC { get; set; } // Liên kết với bảng NCC

    [ForeignKey("MaNCC")]
    public NCC NhaCungCap { get; set; }

    // 2. Khóa ngoại đến Nhân Viên (Employee)
    [Required]
    [Column(TypeName = "varchar(20)")]
    public string MaNV { get; set; } // Liên kết với bảng Employee

    [ForeignKey("MaNV")]
    public Employee NhanVien { get; set; }
}