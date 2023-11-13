using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuanLySinhVien.Models.Khoa
{
    public class InputKhoa
    {
        //[Required(ErrorMessage = "Không được phép để trống!")]
        public string MaKhoa { get; set; }
        //[Required(ErrorMessage = "Không được phép để trống!")]
        public string TenKhoa { get; set; }

        public string Sdt{ get; set; }
        public IFormFileCollection hinhanh { get; set; }
    }
}
