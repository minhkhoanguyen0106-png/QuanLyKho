using Microsoft.AspNetCore.Mvc;
using QuanLyKho.Models;
using System;
using System.Linq;
using System.ComponentModel.DataAnnotations; // Cần thiết nếu dùng các Attributes trong Model, dù không trực tiếp dùng trong Controller

namespace QuanLyKho.Controllers
{
    // Model tạm thời để nhận MaHang từ AJAX request cho chức năng xóa
    public class DeleteRequestModel
    {
        [Required]
        public string MaHang { get; set; }
    }

    public class HangHoaController : Controller
    {
        private readonly QuanLyKhoContext _context;

        public HangHoaController(QuanLyKhoContext context)
        {
            _context = context;
        }

        // ============================
        // 0. LẤY TẤT CẢ DỮ LIỆU (AJAX) - Dùng cho trang Tổng hợp (ĐÃ SỬA ĐỊNH DẠNG NGÀY)
        // ============================
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var data = _context.HangHoas
                    .OrderByDescending(x => x.ThoiGianTao)
                    .Select(x => new
                    {
                        x.MaHang,
                        x.TenHang,
                        x.LoaiHang,
                        x.GiaBan,
                        x.GiaVon,
                        x.TonKho,
                        x.KhachDat,
                        // SỬA: Dùng định dạng ISO 8601 để JavaScript hiểu
                        ThoiGianTao = x.ThoiGianTao.ToString("yyyy-MM-ddTHH:mm:ss"), 
                        x.DatNCC
                    })
                    .ToList();

                // Trả về Json(data) (Mảng JSON)
                return Json(data); 
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khi lấy tất cả dữ liệu: {ex.Message}");
                return StatusCode(500, new { success = false, message = "Lỗi server khi tải dữ liệu tổng hợp." });
            }
        }

        // ============================
        // 1. LẤY DỮ LIỆU XE (AJAX) - Đã sửa định dạng ngày tháng
        // ============================
        [HttpGet]
        public IActionResult GetAllXe()
        {
            try
            {
                var data = _context.HangHoas
                    .Where(x => x.LoaiHang.Contains("Xe"))
                    .OrderByDescending(x => x.ThoiGianTao)
                    .Select(x => new
                    {
                        x.MaHang,
                        x.TenHang,
                        x.LoaiHang,
                        x.GiaBan,
                        x.GiaVon,
                        x.TonKho,
                        x.KhachDat,
                        // SỬA: Dùng định dạng ISO 8601
                        ThoiGianTao = x.ThoiGianTao.ToString("yyyy-MM-ddTHH:mm:ss"), 
                        x.DatNCC
                    })
                    .ToList();

                return Json(data);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khi lấy dữ liệu xe: {ex.Message}");
                return StatusCode(500, new { success = false, message = "Lỗi server khi tải dữ liệu xe." });
            }
        }

        // ============================
        // 2. LẤY DỮ LIỆU LINH KIỆN (AJAX) - Đã sửa định dạng ngày tháng
        // ============================
        [HttpGet]
        public IActionResult GetAllLinhKien()
        {
            try
            {
                var data = _context.HangHoas
                    .Where(x => !x.LoaiHang.Contains("Xe"))
                    .OrderByDescending(x => x.ThoiGianTao)
                    .Select(x => new
                    {
                        x.MaHang,
                        x.TenHang,
                        x.LoaiHang,
                        x.GiaBan,
                        x.GiaVon,
                        x.TonKho,
                        x.KhachDat,
                        // SỬA: Dùng định dạng ISO 8601
                        ThoiGianTao = x.ThoiGianTao.ToString("yyyy-MM-ddTHH:mm:ss"),
                        x.DatNCC
                    })
                    .ToList();

                return Json(data);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khi lấy dữ liệu linh kiện: {ex.Message}");
                return StatusCode(500, new { success = false, message = "Lỗi server khi tải dữ liệu linh kiện." });
            }
        }

        // ============================
        // 3. THÊM MỚI HÀNG HÓA/LINH KIỆN (AJAX) - Giữ nguyên
        // ============================
        [HttpPost]
        public IActionResult Create([FromBody] HangHoa model)
        {
            if (!ModelState.IsValid || string.IsNullOrEmpty(model.TenHang))
                return Json(new { success = false, message = "Dữ liệu không hợp lệ." });

            if (string.IsNullOrEmpty(model.MaHang))
            {
                bool isXe = model.LoaiHang.Contains("Xe");
                string prefix = isXe ? "XD" : "LK";
                
                var lastItem = _context.HangHoas
                    .Where(x => x.MaHang.StartsWith(prefix))
                    .OrderByDescending(x => x.MaHang)
                    .FirstOrDefault();

                int lastNum = 0;
                if (lastItem != null && lastItem.MaHang.Length > prefix.Length && int.TryParse(lastItem.MaHang.Substring(prefix.Length), out int num))
                {
                    lastNum = num;
                }
                
                model.MaHang = prefix + (lastNum + 1).ToString("D3");
            }
            
            model.ThoiGianTao = DateTime.Now;
            _context.HangHoas.Add(model);
            _context.SaveChanges();

            return Json(new { success = true, maHang = model.MaHang });
        }

        // ============================
        // 4. LẤY HÀNG HÓA THEO MÃ (AJAX) - Giữ nguyên
        // ============================
        [HttpGet]
        public IActionResult GetById(string ma)
        {
            var hh = _context.HangHoas.FirstOrDefault(x => x.MaHang == ma);
            if (hh == null) return NotFound();
            return Json(hh);
        }

        // ============================
        // 5. CHỈNH SỬA HÀNG HÓA/LINH KIỆN (AJAX) - Giữ nguyên
        // ============================
        [HttpPost]
        public IActionResult EditAjax([FromBody] HangHoa model)
        {
            var hh = _context.HangHoas.FirstOrDefault(x => x.MaHang == model.MaHang);
            if (hh == null) return NotFound();

            hh.TenHang = model.TenHang;
            hh.LoaiHang = model.LoaiHang;
            hh.GiaBan = model.GiaBan;
            hh.GiaVon = model.GiaVon;
            hh.TonKho = model.TonKho;

            _context.SaveChanges();
            return Json(new { success = true });
        }
        
        // ============================
        // 6. XÓA HÀNG HÓA THEO MÃ (AJAX) - **CHỨC NĂNG MỚI** 🗑️
        // ============================
        [HttpPost] 
        // Nhận MaHang từ JSON body thông qua DeleteRequestModel
        public IActionResult DeleteAjax([FromBody] DeleteRequestModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.MaHang))
            {
                return Json(new { success = false, message = "Mã hàng không hợp lệ." });
            }

            try
            {
                string ma = model.MaHang;
                // 1. Tìm hàng hóa trong DB
                var hh = _context.HangHoas.FirstOrDefault(x => x.MaHang == ma);
                
                // 2. Kiểm tra nếu không tìm thấy
                if (hh == null) 
                    return NotFound(new { success = false, message = $"Không tìm thấy hàng hóa có mã {ma}." });

                // 3. Xóa khỏi DBContext và lưu thay đổi
                _context.HangHoas.Remove(hh);
                _context.SaveChanges();
                
                // 4. Trả về kết quả thành công
                return Json(new { success = true, message = $"Đã xóa hàng hóa có mã {ma} thành công." });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khi xóa hàng hóa {model.MaHang}: {ex.Message}");
                return StatusCode(500, new { success = false, message = "Lỗi server khi xóa dữ liệu: " + ex.Message });
            }
        }

        // ============================
        // Các Action View - Giữ nguyên
        // ============================
        public ActionResult DanhSach()
        {
            return View();
        }
        public IActionResult QuanLyXe()
        {
            return View();
        }
        public ActionResult QuanLyLinhKien()
        {
            return View();
        }
    }
}