using Microsoft.EntityFrameworkCore;
using DoAnLapTrinhWeb.Models;

namespace DoAnLapTrinhWeb.Repositories
{
    public class EFSachRepository : ISachRepository
    {
        private readonly ApplicationDbContext _context;
        public EFSachRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<tbSach>> GetAllAsync()
        {
            // return await _context.Products.ToListAsync();
            return await _context.tbSach.Include(p => p.theLoai) // Include thông tin về category
            .ToListAsync();
        }
        public async Task<tbSach> GetByIdAsync(int id)
        {
            // return await _context.Products.FindAsync(id);
            // lấy thông tin kèm theo category
            return await _context.tbSach.Include(p => p.theLoai).FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task AddAsync(tbSach sach)
        {
            _context.tbSach.Add(sach);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(tbSach sach)
        {
            _context.tbSach.Update(sach);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var sach = await _context.tbSach.FindAsync(id);
            _context.tbSach.Remove(sach);
            await _context.SaveChangesAsync();
        }
    }
}

