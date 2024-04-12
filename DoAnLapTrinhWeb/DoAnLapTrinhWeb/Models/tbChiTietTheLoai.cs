using Microsoft.AspNetCore.Identity;

namespace DoAnLapTrinhWeb.Models
{
    public class tbChiTietTheLoai
    {
        public int Id { get; set; }

        public int sachId { get; set; }

        public tbSach Sach { get; set; }

        public int theLoaiId { get; set; }

        public tbTheLoai? TheLoai { get; set; }
    }
}
