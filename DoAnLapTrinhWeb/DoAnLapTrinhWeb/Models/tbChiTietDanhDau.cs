using Microsoft.AspNetCore.Identity;

namespace DoAnLapTrinhWeb.Models
{
    public class tbChiTietDanhDau
    {
        public int Id { get; set; }

        public int sachId { get; set; }
        public tbSach Sach { get; set; }

        public int danhDauId { get; set; }
        public tbLoaiDanhDau DanhDau { get; set; }

        public string userId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
