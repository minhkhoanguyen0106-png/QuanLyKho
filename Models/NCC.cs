using System.ComponentModel.DataAnnotations;

namespace QuanLyKho.Models
{
    public class NCC
    {
        [Key]
        public int NCCId { get; set; }

        [Required]
        public string TenNCC { get; set; }

        public string DiaChi { get; set; }

        public string SDT { get; set; }
    }
}
