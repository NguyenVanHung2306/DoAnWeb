using DoAnLapTrinhWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace DoAnLapTrinhWeb.Repositories
{
    public class EFTrangRepository : ITrangRepository
    {
        private readonly ApplicationDbContext _context;

        public EFTrangRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TbTrang>> GetAllAsync()
        {
            return await _context.tbTrang.Include(p => p.sach).ToListAsync();
        }

        public async Task AddAsync(TbTrang trangCategori)
        {
            _context.tbTrang.Add(trangCategori);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var trangCategori = await _context.tbTrang.FindAsync(id);
            if (trangCategori != null)
            {
                _context.tbTrang.Remove(trangCategori);
                await _context.SaveChangesAsync();
            }
        }
        public Task<TbTrang> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
