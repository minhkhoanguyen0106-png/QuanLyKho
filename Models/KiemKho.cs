namespace QuanLyKho.Models
{
    public class KiemKho
    {
        public int Id { get; set; }
        public int HangHoaId { get; set; }
        public int SoLuongThucTe { get; set; }
        public DateTime NgayKiem { get; set; }

        public HangHoa? HangHoa { get; set; }
    }
}
