using System.ComponentModel.DataAnnotations;

namespace QuanLyKho.Models
{
    public class KhoHang
    {
        [Key]
        public int KhoHangId { get; set; }

        [Required]
        public string TenHang { get; set; }

        public int SoLuong { get; set; }

        public string DonViTinh { get; set; }

        public decimal DonGia { get; set; }
    }
}
