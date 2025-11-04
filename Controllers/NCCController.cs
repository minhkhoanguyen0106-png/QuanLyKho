using Microsoft.AspNetCore.Mvc;

namespace QuanLyKho.Controllers
{
    public class NCCController : Controller
    {
        public IActionResult DanhSachNCC()
        {
            return View();
        }

         public IActionResult Xuat()
        {
            return View();
        }

        public IActionResult Nhap()
        {
            return View();
        }
    
    }
}
