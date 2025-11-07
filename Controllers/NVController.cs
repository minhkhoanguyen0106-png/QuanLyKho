using Microsoft.AspNetCore.Mvc;

public class NVController : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "Trang nhân viên";
        return View();
    }
}
