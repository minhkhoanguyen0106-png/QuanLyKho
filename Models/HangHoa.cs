using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyKho.Models
{
    public class HangHoa
    {
        [Key]
        public int Id { get; set; }               // Primary key

        [Required]
        [StringLength(20)]
        public string MaHang { get; set; }        // Mã hàng hiển thị

        [StringLength(200)]
        public string TenHang { get; set; }

        [StringLength(100)]
        public string LoaiHang { get; set; }

        public decimal GiaBan { get; set; }
        public decimal GiaVon { get; set; }
        public int TonKho { get; set; }

        public int KhachDat { get; set; } = 0;
        public int DatNCC { get; set; } = 0;

        public DateTime ThoiGianTao { get; set; } = DateTime.Now;

        // Navigation: 1 hàng hóa có nhiều kiểm kê
        public ICollection<KiemKeKho> KiemKeKhos { get; set; }
    }
}
