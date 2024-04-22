namespace DoAnLapTrinhWeb.Models
{
    public class TbTrang
    {
        public int Id { get; set; }
        public int? SachId { get; set; }
        public tbSach? sach { get; set; }
        public string? Noidung { get; set; }
    }
}
