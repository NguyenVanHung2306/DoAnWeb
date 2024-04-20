using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace DoAnLapTrinhWeb.Models
{
    public class tbSach
    {
        //[Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string? tenSach { get; set; }
        public string? imageUrl { get; set; }
        public int tacGiaId { get; set; }
        public tbTacGia? TacGia { get; set; }
        public string? moTa { get; set; }
        public int theLoaiId { get; set; }
        public tbTheLoai? theLoai { get; set; }
        public List<tbChiTietDanhDau>? chiTietDanhDaus { get; set; }
        public List<tbLichSu>? lichSus { get; set; }
        public List<tbPhieuDanhGia>? phieuDanhGias { get; set; }
        public List<TbTrang>? TbTrangs { get; set; }

    }
}
