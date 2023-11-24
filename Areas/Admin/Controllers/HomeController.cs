using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QuanLySinhVien.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Admin")]
    [Route("quan-tri-vien")]
    public class HomeController : Controller
    {
        [Route("danh-sach")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("them")]
        public IActionResult Them()
        {
            return View();
        }
        [Route("cap-nhat")]
        public IActionResult CapNhat()
        {
            return View();
        }
    }
}
