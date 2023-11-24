using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using QuanLySinhVien.Areas.Admin.Models.TaiKhoan;
using QuanLySinhVien.Areas.Admin.Views.TaiKhoan;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace QuanLySinhVien.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("tai-khoan")]
    public class TaiKhoanController : Controller
    {
        private readonly HttpClient _httpClient;

        public TaiKhoanController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient("Client");
        }

        [Route("dang-nhap")]
        public IActionResult DangNhap()
        {
            return View();
        }

        [Route("dang-nhap")]
        [HttpPost]
        public async Task<IActionResult> DangNhap(InputTaiKhoan input)
        {
            string url = "http://localhost:5275/api/Authentication/auth";
            if (ModelState.IsValid)
            {
                var data = new MultipartFormDataContent();
                data.Add(new StringContent(input.Email), "Email");
                data.Add(new StringContent(input.Username), "Username");
                data.Add(new StringContent(EncryptPassword(input.Password)), "Password");
                data.Add(new StringContent(input.Role), "Role");
                var res = await _httpClient.PostAsync(url, data);
                if (res.IsSuccessStatusCode)
                {
                    var token = await res.Content.ReadAsAsync<OutputToken>();
                    string test = token.Token;
                    Response.Cookies.Append("Username", input.Username);
                    //Response.Cookies.Append("JwtToken", token.Token);
                    return await AccessLogin(token.Token);
                }
            }
            return View();
        }

        private async Task<IActionResult> AccessLogin(string Token)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadToken(Token) as JwtSecurityToken;
            var identity = new ClaimsIdentity(token.Claims, "Token");
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            return await CheckRole();
        }

        private async Task<IActionResult> CheckRole()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var role = identity?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (role == "admin") return RedirectToAction("Index", "Home", new { Areas = "Admin" });
            return RedirectToAction("Index", "Home", new { Areas = "" });
        }

        [Route("dang-ky")]
        public IActionResult DangKy()
        {
            return View();
        }

        [Route("dang-ky")]
        [HttpPost]
        public async Task<IActionResult> DangKy(InputTaiKhoan input)
        {
            string url = "http://localhost:5275/api/TaiKhoan/dang-ky";
            if (ModelState.IsValid)
            {
                var data = new MultipartFormDataContent();
                data.Add(new StringContent(input.Email), "Email");
                data.Add(new StringContent(input.Username), "Username");
                data.Add(new StringContent(EncryptPassword(input.Password)), "Password");
                data.Add(new StringContent(input.Role), "Role");

                var res = await _httpClient.PostAsync(url, data);
                //var result = await res.Content.ReadAsAsync<OutputDangKy>();
                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("DangNhap", "TaiKhoan", new { Areas = "Admin"});
                }
            }
            return View();
        }

        private string EncryptPassword(string password)
        {
            using(var sha256 = SHA256.Create())
            {
                var data = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(data);
                return Convert.ToBase64String(hash);
            }
        }
        [Route("access-denied")]
        public IActionResult TuChoi()
        {
            return View();
        }
    }
}
