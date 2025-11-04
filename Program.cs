var builder = WebApplication.CreateBuilder(args);

// Đăng ký MVC và Session
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

var app = builder.Build();

// Cấu hình pipeline
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

// ✅ ROUTE MẶC ĐỊNH: trỏ tới Account/Login
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
