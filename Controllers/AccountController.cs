using Microsoft.AspNetCore.Mvc;

namespace YourProjectName.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password, string role)
        {
            if (username == "admin" && password == "123" && role == "manager")
            {
                HttpContext.Session.SetString("User", username);
                HttpContext.Session.SetString("Role", "Manager");
                return RedirectToAction("Index", "Home"); // ← trang quản lý
            }
            else if (username == "nhanvien" && password == "123" && role == "employee")
            {
                HttpContext.Session.SetString("User", username);
                HttpContext.Session.SetString("Role", "Employee");
                return RedirectToAction("Index", "NV"); // ← trang nhân viên
            }

            ViewBag.Error = "Sai tài khoản, mật khẩu hoặc vai trò!";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}
