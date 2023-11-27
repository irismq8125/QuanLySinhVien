using Common.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Common;
using NuGet.Protocol;
using QuanLySinhVien.Common;
using QuanLySinhVien.Models.Entity;
using QuanLySinhVien.Models.Khoa;
using System.Net.Http;
using System.Net.Http.Headers;

namespace QuanLySinhVien.Controllers
{
    [Route("/quan-ly-khoa")]
    public class KhoaController : Controller
    {
        private static string wwwroot = Directory.GetCurrentDirectory() + "\\wwwroot";
        private readonly QuanLySinhVienContext _context;
        public KhoaController(QuanLySinhVienContext context)
        {
            _context = context;
        }
        //[Route("danh-sach")]
        //public IActionResult Index(string? timkiem)
        //{
        //    var items = _context.Khoas.Where(c => c.Filter.Contains((timkiem ?? "").ToLower())).ToList();
        //    return View(items);
        //}

        [Route("danh-sach")]
        public async Task<IActionResult> Index()
        {
            //call du lieu tu api
            var items = await getKhoa();
            return View(items);
        }

        public async Task<List<ViewModelKhoa>> getKhoa()
        {
            string url = "http://localhost:5275/api/Khoa/danh-sach-khoa";
            using (var client = new HttpClient())
			{
                
				var res = await client.GetAsync(url);
                var resDele = await client.DeleteAsync(url);

                if (res.IsSuccessStatusCode)
                {
                    var viewkhoa = new List<ViewModelKhoa>();
                    var listitems = res.Content.ReadAsAsync<List<ResultApiKhoa>>().Result;
                    foreach(var item in listitems)
                    {
                        ViewModelKhoa khoa = new ViewModelKhoa();
                        khoa.Id = item.Id;
                        khoa.MaKhoa = item.MaKhoa;
                        khoa.TenKhoa = item.TenKhoa;
                        khoa.SDT = item.SDT;
                        khoa.UrlImages = JsonConvert.DeserializeObject<List<ResultApiImageKhoa>>(item.UrlImages);
                        viewkhoa.Add(khoa);
                    }
                    return viewkhoa;
                }
            }
            return null;
        }

        [Route("them-khoa", Name = "them")]
        public IActionResult Them()
        {
            return View();
        }

        
        [Route("them-khoa", Name = "them")]
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        //public IActionResult Them(Khoa input)
        public async Task<IActionResult> Them(InputKhoa input)
        {
            string url = "http://localhost:5275/api/Khoa/them-moi-khoa";
            using(var client = new HttpClient())
            {
                var data = new MultipartFormDataContent();
                data.Add(new StringContent(input.MaKhoa), "MaKhoa");
                data.Add(new StringContent(input.TenKhoa), "TenKhoa");
                data.Add(new StringContent(input.Sdt), "SDT");

                var listimg = new List<string>();
                foreach(var img in input.hinhanh)
                {
                    var imgPath = UploadFiles.SaveImage(img);
                    listimg.Add(imgPath);
                    var imgStream = new MemoryStream(System.IO.File.ReadAllBytes(wwwroot + imgPath));
                    var imgContent = new ByteArrayContent(imgStream.ToArray());
                    data.Add(imgContent, "Images", img.FileName);
                }

                var res = await client.PostAsync(url, data);
                var resPut = await client.PutAsync(url, data);
                if (res.IsSuccessStatusCode)
                {
                    foreach (var path in listimg)
                    {
                        UploadFiles.RemoveImage(path);
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    //co the them cac doan thong bao loi~
                    return View();
                }

            }
            //if (ModelState.IsValid)
            //{
            //    Khoa khoa = new Khoa();
            //    khoa.Id = Guid.NewGuid().ToString();
            //    khoa.MaKhoa = input.MaKhoa;
            //    khoa.TenKhoa = input.TenKhoa;
            //    khoa.Sdt = input.Sdt;
            //    khoa.Filter = input.MaKhoa + " " + input.TenKhoa.ToLower() + " " + Utility.ConvertToUnsign(input.TenKhoa.ToLower()) + " " + input.Sdt;
            //    khoa.UrlImage = UploadFiles.SaveImage(input.hinhanh);

            //    _context.Add(khoa);
            //    _context.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            return View();
        }

        //[Route("them-khoa")]
        //[HttpPost]
        ////public IActionResult Them(Khoa input)
        //public IActionResult Them(string makhoa, string tenkhoa, string sodienthoai, IFormFile hinhanh)
        //{
        //    //tạo Guid
        //    //Guid id = Guid.NewGuid();
        //    if (!string.IsNullOrEmpty(makhoa))
        //    {
        //        Khoa khoa = new Khoa();
        //        khoa.Id = Guid.NewGuid().ToString();
        //        khoa.MaKhoa = makhoa;
        //        khoa.TenKhoa = tenkhoa;
        //        khoa.Sdt = sodienthoai;
        //        khoa.Filter = makhoa + " " + tenkhoa.ToLower() + " " + Utility.ConvertToUnsign(tenkhoa.ToLower()) + " " + sodienthoai;
        //        khoa.UrlImage = UploadFiles.SaveImage(hinhanh);

        //        _context.Add(khoa);
        //        _context.SaveChanges();
        //        return RedirectToAction("Index");
        //        //return Redirect("/Khoa/Index");
        //    }
        //    return View();
        //}
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
            return RedirectToAction("Index");
        }
        //[Route("xoa-khoa")]
        public IActionResult Xoa(string id)
        {
            var item = _context.Khoas.FirstOrDefault(x => x.Id == id);

            bool check = UploadFiles.RemoveImage(item.UrlImage);

            if(check)
            {
                _context.Remove(item);
                _context.SaveChanges();
            }
            
            return RedirectToAction("Index");
        }
    }
}
