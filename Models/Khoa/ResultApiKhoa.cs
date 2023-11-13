namespace QuanLySinhVien.Models.Khoa
{
    public class ResultApiKhoa
    {
        public string Id { get; set; }
        public string MaKhoa { get; set; }
        public string TenKhoa { get; set; }
        public string SDT { get; set; }
        public string UrlImages { get; set; }

    }
    public class ResultApiImageKhoa
    {
        public string UrlImage { get; set; }
        public int Position { get; set; }
    }
}
