// File: NCCController.cs

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System;
using QuanLyKho.Models; 
// Đảm bảo using QuanLyKho.Models; là đúng với namespace của bạn

namespace QuanLyKho.Controllers
{
    // ĐỊNH NGHĨA DTO (Đã loại bỏ MaNhom)
    public class NhaCungCapFilterDTO
    {
        // ❌ ĐÃ XÓA: public int? MaNhom { get; set; }
        public string SearchTerm { get; set; }
        public decimal? TongMuaMin { get; set; }
        public decimal? TongMuaMax { get; set; }
        public decimal? NoHienTaiMin { get; set; }
        public decimal? NoHienTaiMax { get; set; }
        public string Status { get; set; } 
    }

    public class NCCController : Controller
    {
        private readonly QuanLyKhoContext _context; 

        public NCCController(QuanLyKhoContext context)
        {
            _context = context;
        }

        // ------------------------------------------------------------------
        // ACTION 1: Hiển thị View DanhSachNCC (Đã loại bỏ logic nhóm)
        // ------------------------------------------------------------------
        public async Task<IActionResult> DanhSachNCC()
        {
            var nccs = await _context.NCCs.ToListAsync();
            // ❌ ĐÃ XÓA: ViewBag.NhomNCCs = await _context.NhomNhaCungCaps.ToListAsync();
            return View(nccs); 
        }

        // ------------------------------------------------------------------
        // ACTION 2: Xử lý AJAX POST để TRUY VẤN/LỌC Danh sách NCC (Đã loại bỏ logic nhóm)
        // ------------------------------------------------------------------
        [HttpPost] 
        public async Task<IActionResult> GetDanhSachNCC([FromBody] NhaCungCapFilterDTO filters)
        {
            // ❌ KHÔNG CẦN .Include(n => n.Nhom) nữa
            IQueryable<NCC> query = _context.NCCs; 

            // ❌ ĐÃ XÓA: Logic lọc theo MaNhom

            if (filters.TongMuaMin.HasValue)
            {
                query = query.Where(n => n.TongMua >= filters.TongMuaMin.Value);
            }
            if (filters.TongMuaMax.HasValue)
            {
                query = query.Where(n => n.TongMua <= filters.TongMuaMax.Value);
            }
            if (filters.NoHienTaiMin.HasValue)
            {
                query = query.Where(n => n.NoHienTai >= filters.NoHienTaiMin.Value);
            }
            if (filters.NoHienTaiMax.HasValue)
            {
                query = query.Where(n => n.NoHienTai <= filters.NoHienTaiMax.Value);
            }
            if (!string.IsNullOrEmpty(filters.Status) && filters.Status != "all")
            {
                query = query.Where(n => n.TrangThai == filters.Status); 
            }
            if (!string.IsNullOrEmpty(filters.SearchTerm))
            {
                string search = filters.SearchTerm.ToLower();
                query = query.Where(n => 
                    n.MaNCC.ToLower().Contains(search) || 
                    n.TenNCC.ToLower().Contains(search) || 
                    (n.DienThoai != null && n.DienThoai.Contains(search)));
            }

            var filteredList = await query.OrderBy(n => n.TenNCC).ToListAsync(); 
            
            // Loại bỏ trường 'nhom' khỏi JSON trả về
            var result = filteredList
                .Select(n => new {
                    ma = n.MaNCC,
                    ten = n.TenNCC,
                    // ❌ ĐÃ XÓA: nhom = "...", 
                    dienthoai = n.DienThoai,
                    noHienTai = n.NoHienTai,
                    tongMua = n.TongMua,
                    trangThai = n.TrangThai
                })
                .ToList();

            return Json(result);
        }
        
        // ------------------------------------------------------------------
        // ACTION 3: Xử lý AJAX POST để THÊM MỚI Nhà Cung Cấp (Đã loại bỏ logic nhóm)
        // ------------------------------------------------------------------
        [HttpPost]
        public async Task<IActionResult> AddSupplier([FromBody] NCC newSupplier)
        {
            if (!ModelState.IsValid || newSupplier.MaNCC == null || newSupplier.TenNCC == null)
            {
                return BadRequest(new { success = false, message = "Dữ liệu không hợp lệ. Vui lòng kiểm tra Mã và Tên NCC." });
            }

            try
            {
                bool exists = await _context.NCCs.AnyAsync(n => n.MaNCC == newSupplier.MaNCC);
                if (exists)
                {
                    return Conflict(new { success = false, message = $"Mã NCC '{newSupplier.MaNCC}' đã tồn tại." });
                }
                
                if (string.IsNullOrEmpty(newSupplier.TrangThai))
                {
                    newSupplier.TrangThai = "Đang hoạt động";
                }

                _context.NCCs.Add(newSupplier);
                await _context.SaveChangesAsync();

                // ❌ ĐÃ XÓA: Logic tìm nhóm
                
                return Ok(new 
                { 
                    success = true, 
                    message = "Thêm NCC thành công!", 
                    data = new 
                    {
                        ma = newSupplier.MaNCC,
                        ten = newSupplier.TenNCC,
                        // ❌ ĐÃ XÓA: nhom = "...", 
                        dienthoai = newSupplier.DienThoai,
                        noHienTai = newSupplier.NoHienTai,
                        tongMua = newSupplier.TongMua,
                        trangThai = newSupplier.TrangThai
                    }
                });
            }
            catch (Exception ex) 
            {
                string innerError = ex.InnerException?.Message ?? ex.Message;
                return StatusCode(500, new { success = false, message = "Lỗi Server khi lưu dữ liệu. Chi tiết: " + innerError, error = ex.Message });
            }

        }

         public async Task<IActionResult> Nhap()
        {
            return View(); 
        }

         public async Task<IActionResult> Xuat()
        {
            return View(); 
        }
    }
   
}