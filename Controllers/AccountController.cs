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
        return RedirectToAction("Index", "Home");
    }
    else if (username == "nhanvien" && password == "123" && role == "employee")
    {
        HttpContext.Session.SetString("User", username);
        HttpContext.Session.SetString("Role", "Employee");
        return RedirectToAction("Index", "Home");
    }

    ViewBag.Error = "Sai tài khoản, mật khẩu hoặc vai trò!";
            return View();
        }

        public IActionResult Logout()
        {
            // Xóa toàn bộ session (đăng xuất)
    HttpContext.Session.Clear();

    // Chuyển hướng về trang đăng nhập
    return RedirectToAction("Login", "Account");
        }
    }
}
