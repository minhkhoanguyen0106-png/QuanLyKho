using Microsoft.AspNetCore.Mvc;

namespace QuanLyKho.Controllers
{
    public class XuatHangController : Controller
    {
        // Trang danh sách phiếu xuất
        public IActionResult Index()
        {
            return View();
        }

        // Trang tạo phiếu xuất (chưa xử lý backend)
        public IActionResult Create()
        {
            return View();
        }

        // Trang chi tiết phiếu xuất (tùy chọn)
        public IActionResult Details(string id)
        {
            ViewBag.MaPhieu = id;
            return View();
        }
    }
}