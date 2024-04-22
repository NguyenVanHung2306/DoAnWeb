using DoAnLapTrinhWeb.Models;

namespace DoAnLapTrinhWeb.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<ApplicationUser>> GetAllAsync();
        Task<ApplicationUser> GetByIdAsync(string id);
        Task DeleteAsync(string id);
    }
}
