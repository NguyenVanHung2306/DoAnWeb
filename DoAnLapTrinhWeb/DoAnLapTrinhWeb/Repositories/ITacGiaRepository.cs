using DoAnLapTrinhWeb.Models;

namespace DoAnLapTrinhWeb.Repositories
{
    public interface ITacGiaRepository
    {
        Task<IEnumerable<tbTacGia>> GetAllAsync();
        Task<tbTacGia> GetByIdAsync(int id);
        Task AddAsync(tbTacGia tacGia);
        Task UpdateAsync(tbTacGia tacGia);
        Task DeleteAsync(int id);
        Task<bool> IsTenTacGiaExisted(string tenTacGia);
    }
}
