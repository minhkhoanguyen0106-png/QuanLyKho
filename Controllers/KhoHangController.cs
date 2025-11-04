using Microsoft.AspNetCore.Mvc;

namespace QuanLyKho.Controllers
{
    public class KhoHangController : Controller
    {
        public IActionResult ThongKeSaiLech()
        {
            return View();
        }

         public IActionResult ChuyenKho()
        {
            return View();
        }

        public IActionResult ChonKhoDeChuyen()
        {
            return View();
        }
    
    }
}
