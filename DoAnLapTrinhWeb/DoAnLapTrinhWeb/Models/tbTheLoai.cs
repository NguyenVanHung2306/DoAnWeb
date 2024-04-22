using System.ComponentModel.DataAnnotations;

namespace DoAnLapTrinhWeb.Models
{
    public class tbTheLoai
    {
        //[Key]
        public int Id { get; set; }
        public string? tenTheLoai { get; set;  }
        public List<tbSach>? Sachs { get; set; }
}
}
