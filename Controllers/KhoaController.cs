using Microsoft.AspNetCore.Mvc;
using QuanLySinhVien.Models.Entity;

namespace QuanLySinhVien.Controllers
{
    public class KhoaController : Controller
    {
        private readonly QuanLySinhVienContext _context;
        public KhoaController(QuanLySinhVienContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var items = _context.Khoas.ToList();
            return View(items);
        }
        public IActionResult Them()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Them(string makhoa, string tenkhoa, string sodienthoai)
        {
            //tạo Guid
            //Guid id = Guid.NewGuid();
            if (!string.IsNullOrEmpty(makhoa))
            {
                Khoa khoa = new Khoa();
                khoa.Id = Guid.NewGuid().ToString();
                khoa.MaKhoa = makhoa;
                khoa.TenKhoa = tenkhoa;
                khoa.Sdt = sodienthoai;

                _context.Add(khoa);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult CapNhat(string id)
        {
            var item = _context.Khoas.FirstOrDefault(x => x.Id == id);
            return View(item);
        }
        [HttpPost]
        public IActionResult CapNhat(string id, string makhoa, string tenkhoa, string sodienthoai)
        {
            var item = _context.Khoas.FirstOrDefault(x => x.Id == id);
            item.MaKhoa = makhoa;
            item.TenKhoa = tenkhoa;
            item.Sdt = sodienthoai;
            _context.Update(item);
            _context.SaveChanges();
            return View(item);
        }
        public IActionResult Xoa(string id)
        {
            var item = _context.Khoas.FirstOrDefault(x => x.Id == id);
            _context.Remove(item);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
