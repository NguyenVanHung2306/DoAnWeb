using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DoAnLapTrinhWeb.Models
{
    public class tbLichSu
    {
        [Key]
        public int lichSuId { get; set; }

        public DateTime thoiGianDoc { get; set; }

        public int sachId { get; set; }
        public tbSach Sach { get; set; }

        public string userId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
