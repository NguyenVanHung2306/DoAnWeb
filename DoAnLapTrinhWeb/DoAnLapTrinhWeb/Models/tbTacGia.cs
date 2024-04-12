using System.ComponentModel.DataAnnotations;

namespace DoAnLapTrinhWeb.Models
{
    public class tbTacGia
    {
        //[Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string TenTacGia { get; set; }

        public List<tbSach>? Saches { get; set; }
    }
}
