namespace QuanLyKho.Models
{
    public class XuatHang
    {
        public int Id { get; set; }
        public int HangHoaId { get; set; }
        public int SoLuong { get; set; }
        public DateTime NgayXuat { get; set; }

        public HangHoa? HangHoa { get; set; }
    }
}
