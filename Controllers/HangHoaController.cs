using Microsoft.AspNetCore.Mvc;

namespace QuanLyKho.Controllers
{
    public class HangHoaController : Controller
    {
        public IActionResult QuanLyXe()
        {
            return View();
        }

         public IActionResult QuanLyLinhKien()
        {
            return View();
        }

        public IActionResult DanhSach()
        {
            return View();
        }
    
    }
}
