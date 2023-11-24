using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using QuanLySinhVien.Areas.Admin.Models.TaiKhoan;
using QuanLySinhVien.Models.Entity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<QuanLySinhVienContext>(
    c => c.UseSqlServer(builder.Configuration.GetConnectionString("Default"))
);

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient("Client").AddHttpMessageHandler<AuthHandled>();
builder.Services.AddTransient<AuthHandled>();

// Add services to the container.
builder.Services.AddAuthentication(o =>
    {
        o.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        o.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        o.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    })
    .AddCookie(o =>
    {
        o.LoginPath = "/tai-khoan/dang-nhap";
        o.AccessDeniedPath = "/tai-khoan/access-denied";
    });



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
