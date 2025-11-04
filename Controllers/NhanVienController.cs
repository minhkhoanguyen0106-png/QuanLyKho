using Microsoft.AspNetCore.Mvc;

namespace QuanLyKho.Controllers
{
    public class NhanVienController : Controller
    {
        public IActionResult NhanVien()
        {
            return View();
        }

         public IActionResult MoTaiKhoan()
        {
            return View();
        }

        public IActionResult KhoaTaiKhoan()
        {
            return View();
        }
    
    }
}
