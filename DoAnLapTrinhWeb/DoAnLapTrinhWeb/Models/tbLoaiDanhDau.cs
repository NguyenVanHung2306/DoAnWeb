using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DoAnLapTrinhWeb.Models
{
    public class tbLoaiDanhDau
    {
        //[Key]
        public int Id { get; set; }

        public string TenDanhDau { get; set; }

        public List<tbChiTietDanhDau> chiTietDanhDaus { get; set; }

    }
}
