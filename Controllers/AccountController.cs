using Microsoft.AspNetCore.Mvc;
using QuanLyKho.Models;

namespace QuanLyKho.Controllers
{
    public class AccountController : Controller
    {
        private readonly QuanLyKhoContext _context;

        public AccountController(QuanLyKhoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _context.Accounts
                .FirstOrDefault(a => a.Username == username && a.Password == password);

            if (user != null)
            {
                // ✅ Lưu thông tin đăng nhập
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetString("Role", user.Role ?? "");

                // ✅ Chuyển hướng theo vai trò
                if (user.Role == "Manager")
                {
                    return RedirectToAction("Index", "Home"); // Trang quản lý
                }
                else if (user.Role == "Employee")
                {
                    return RedirectToAction("Index", "Home"); // Trang nhân viên
                }
                else
                {
                    ViewBag.Error = "Không xác định được vai trò người dùng!";
                    return View();
                }
            }

            // ❌ Sai thông tin
            ViewBag.Error = "Sai tên đăng nhập hoặc mật khẩu!";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
