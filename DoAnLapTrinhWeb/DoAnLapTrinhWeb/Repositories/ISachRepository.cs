using DoAnLapTrinhWeb.Models;

namespace DoAnLapTrinhWeb.Repositories
{
    public interface ISachRepository
    {
        Task<IEnumerable<tbSach>> GetAllAsync();
        Task<tbSach> GetByIdAsync(int id);
        Task AddAsync(tbSach sach);
        Task UpdateAsync(tbSach sach);
        Task DeleteAsync(int id);
    }
}
