using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DoAnLapTrinhWeb.Models;

namespace DoAnLapTrinhWeb.Repositories
{
    public class EFUserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public EFUserRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllAsync()
        {
            var usersInRole = await _userManager.GetUsersInRoleAsync("Customer");
            return usersInRole.ToList();
        }

        public async Task DeleteAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
        }

        public async Task<ApplicationUser> GetByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }
    }
}
