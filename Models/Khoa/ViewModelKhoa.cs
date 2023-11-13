namespace QuanLySinhVien.Models.Khoa
{
    public class ViewModelKhoa
    {
        public string Id { get; set; }
        public string MaKhoa { get; set; }
        public string TenKhoa { get; set; }
        public string SDT { get; set; }
        public List<ResultApiImageKhoa> UrlImages { get; set; }
    }
}
