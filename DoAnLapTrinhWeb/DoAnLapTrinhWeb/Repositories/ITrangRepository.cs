using DoAnLapTrinhWeb.Models;

namespace DoAnLapTrinhWeb.Repositories
{
    public interface ITrangRepository
    {
        Task<IEnumerable<TbTrang>> GetAllAsync();
        Task<TbTrang> GetByIdAsync(int id);
        Task AddAsync(TbTrang trangCategori);
        Task DeleteAsync(int id);
    }
}
