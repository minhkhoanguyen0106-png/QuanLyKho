// File: GiaoDichController.cs

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyKho.Models;
using System; // Cần thiết cho DateTime
using System.Linq; // Cần thiết cho Linq (OrderByDescending, Max, FirstOrDefault...)
using System.Collections.Generic; // Cần thiết cho List<NCC> và List<DatHangNhap> (Dùng trong logic tạo dữ liệu mẫu)

namespace QuanLyKho.Controllers
{
    public class GiaoDichController : Controller
    {
        private readonly QuanLyKhoContext _context;

        public GiaoDichController(QuanLyKhoContext context)
        {
            _context = context;
        }

        // ===============================
        // 1. Danh sách Đặt Hàng Nhập (PO)
        // ===============================
        public IActionResult DatHangNhap()
        {
            // Lấy dữ liệu thật từ DB
            var datHang = _context.DatHangNhaps
                .Include(d => d.NhaCungCap)
                .OrderByDescending(d => d.ThoiGian)
                .ToList();

            var phieuNhaps = _context.PhieuNhaps
                .Include(p => p.NhaCungCap)
                .OrderByDescending(p => p.NgayNhap)
                .ToList();

            var nccs = _context.NCCs.ToList();

            // Nếu DB trống, tạo dữ liệu mẫu để test hiển thị
            if (!datHang.Any())
            {
                if (!nccs.Any())
                {
                    nccs = new List<NCC>
                    {
                        new NCC { MaNCC = "NCC01", TenNCC = "Công ty ABC" },
                        new NCC { MaNCC = "NCC02", TenNCC = "Công ty XYZ" }
                    };
                }

                datHang = new List<DatHangNhap>
                {
                    new DatHangNhap
                    {
                        MaPhieuDat = "PO001",
                        ThoiGian = DateTime.Now,
                        MaNCC = nccs[0].MaNCC,
                        NhaCungCap = nccs[0],
                        NgayNhapDuKien = DateTime.Now.AddDays(3),
                        SoNgayCho = 3,
                        TongTien = 1500000,
                        TrangThai = "Phiếu tạm"
                    },
                    new DatHangNhap
                    {
                        MaPhieuDat = "PO002",
                        ThoiGian = DateTime.Now.AddHours(-5),
                        MaNCC = nccs[1].MaNCC,
                        NhaCungCap = nccs[1],
                        NgayNhapDuKien = DateTime.Now.AddDays(5),
                        SoNgayCho = 5,
                        TongTien = 3000000,
                        TrangThai = "Đã xác nhận NCC"
                    }
                };
            }

            ViewBag.PhieuNhaps = phieuNhaps;
            ViewBag.NCCs = nccs;

            return View(datHang);
        }


        // ===============================
        // 2. Tạo phiếu đặt hàng nhập (POST) - ĐÃ FIX
        // ===============================
        [HttpPost]
        public IActionResult TaoPhieuDatHangNhap([FromBody] DatHangNhap model)
        {
            // Loại bỏ các trường được sinh tự động/là đối tượng khỏi ModelState
            ModelState.Remove("MaPhieuDat");
            ModelState.Remove("ThoiGian");
            ModelState.Remove("SoNgayCho");
            ModelState.Remove("NhaCungCap");
            
            // FIX LỖI: Loại bỏ validation cho Navigation Property PhieuNhap
            // Đây là lỗi hiển thị trong ảnh: (Chi tiết: PhieuNhap: The PhieuNhap field is required.)
            ModelState.Remove("PhieuNhap");
            
            if (ModelState.IsValid)
            {
                // 1. Tự động sinh MaPhieuDat
                var lastMaPhieu = _context.DatHangNhaps.OrderByDescending(d => d.MaPhieuDat).Select(d => d.MaPhieuDat).FirstOrDefault();
                var newIndex = 1;
                if (!string.IsNullOrEmpty(lastMaPhieu) && lastMaPhieu.StartsWith("PO"))
                {
                    if (int.TryParse(lastMaPhieu.Substring(2), out int lastIndex))
                    {
                        newIndex = lastIndex + 1;
                    }
                }
                model.MaPhieuDat = "PO" + newIndex.ToString("D3"); // Ví dụ: PO003

                // 2. Tự động gán Thời gian
                model.ThoiGian = DateTime.Now;

                // 3. Tính lại Số ngày chờ 
                TimeSpan soNgayCho = model.NgayNhapDuKien.Date - DateTime.Today.Date;
                model.SoNgayCho = Math.Max(0, (int)Math.Ceiling(soNgayCho.TotalDays)); 
                
                // 4. Lưu DatHangNhap
                _context.DatHangNhaps.Add(model);
                _context.SaveChanges();
                
                // ===============================================
                // 5. FIX LỖI: THÊM GIAO DỊCH VÀO LỊCH SỬ
                // ===============================================
                // Tìm tên NCC để lưu vào lịch sử
                var nccName = _context.NCCs.Find(model.MaNCC)?.TenNCC ?? model.MaNCC;
                
                var lichSu = new LichSuGiaoDich
                {
                    // Lưu ý: Cần đảm bảo MaGiaoDich là duy nhất (sử dụng GUID nếu cần)
                    // Tạm thời dùng: GD + Mã PO
                    MaGiaoDich = "GD-" + model.MaPhieuDat, 
                    LoaiGiaoDich = "Tạo Đặt Hàng Nhập", 
                    ThoiGian = model.ThoiGian,
                    DoiTac = nccName, 
                    // Giả định chi nhánh cố định (Cần thay bằng chi nhánh thực tế)
                    GiaTri = model.TongTien,
               
                    MaThamChieu = model.MaPhieuDat, // *** MÃ PO LÀ MÃ THAM CHIẾU ***
                    TrangThai = model.TrangThai 
                };

                _context.LichSuGiaoDichs.Add(lichSu);
                _context.SaveChanges();
                // ===============================================

                return Json(new { success = true, message = "Tạo phiếu thành công!", poCode = model.MaPhieuDat });
            }
            
            // Xử lý thông báo lỗi chi tiết hơn (cho debug)
            var errors = ModelState.Where(x => x.Value.Errors.Any())
                .Select(x => new { field = x.Key, errors = x.Value.Errors.Select(e => e.ErrorMessage).ToList() })
                .ToList();

            return Json(new { success = false, message = "Dữ liệu không hợp lệ. Vui lòng kiểm tra lại.", details = errors });
        }


        // ===============================
        // 3. Action Tra Cứu Lịch Sử Giao Dịch - ĐÃ THÊM (Fix lỗi 404)
        // ===============================
        public IActionResult TraCuu()
        {
            // Lấy toàn bộ dữ liệu lịch sử giao dịch để truyền sang View
            // Sắp xếp giảm dần theo thời gian
            var lichSuGiaoDich = _context.LichSuGiaoDichs
                .OrderByDescending(x => x.ThoiGian)
                .ToList();

            // Trả về View TraCuu.cshtml và truyền dữ liệu LichSuGiaoDich
            return View(lichSuGiaoDich);
        }
    }
}