using System;
using System.ComponentModel.DataAnnotations;

namespace QuanLyKho.Models
{
    public class BaoCao
    {
        [Key]
        public int BaoCaoId { get; set; }

        public DateTime NgayLap { get; set; }

        public string LoaiBaoCao { get; set; } // Báo cáo nhập / xuất

        public string NoiDung { get; set; }
    }
}
