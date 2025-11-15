using System;
using System.ComponentModel.DataAnnotations;

namespace QuanLyKho.Models
{
    public class GiaoDich
    {
        [Key]
        public int GiaoDichId { get; set; }

        public DateTime NgayGiaoDich { get; set; }

        public string LoaiGiaoDich { get; set; } // Nhập / Xuất

        public int NhanVienId { get; set; }
        public int KhoHangId { get; set; }

        public string GhiChu { get; set; }
    }
}
