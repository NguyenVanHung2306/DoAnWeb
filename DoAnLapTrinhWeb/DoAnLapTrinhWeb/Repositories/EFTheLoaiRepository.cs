using Microsoft.EntityFrameworkCore;
using DoAnLapTrinhWeb.Models;

namespace DoAnLapTrinhWeb.Repositories
{
    public class EFTheLoaiRepository : ITheLoaiRepository
    {
        private readonly ApplicationDbContext _context;
        public EFTheLoaiRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<tbTheLoai>> GetAllAsync()
        {
            // return await _context.Products.ToListAsync();
            return await _context.tbTheLoai
            .Include(p => p.Sachs) // Include thông tin về category
            .ToListAsync();
        }
        public async Task<tbTheLoai> GetByIdAsync(int id)
        {
            // return await _context.Products.FindAsync(id);
            // lấy thông tin kèm theo category
            return await _context.tbTheLoai.Include(p => p.Sachs).FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task AddAsync(tbTheLoai theLoai)
        {
            _context.tbTheLoai.Add(theLoai);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(tbTheLoai theLoai)
        {
            _context.tbTheLoai.Update(theLoai);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var theLoai = await _context.tbTheLoai.FindAsync(id);
            _context.tbTheLoai.Remove(theLoai);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> IsTenTheLoaiExisted(string TenTheLoai)
        {
            return await _context.tbTheLoai.AnyAsync(t => t.tenTheLoai == TenTheLoai);
        }
    }
}

