using Microsoft.AspNetCore.Mvc;

namespace QuanLyKho.Controllers
{
    public class NhapHangController : Controller
    {
        // Trang danh sách phiếu nhập
        public IActionResult Index()
        {
            return View();
        }

        // Trang thêm phiếu nhập mới
        public IActionResult Create()
        {
            return View();
        }
    }
}