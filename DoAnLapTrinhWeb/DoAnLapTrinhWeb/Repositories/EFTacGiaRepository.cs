using Microsoft.EntityFrameworkCore;
using DoAnLapTrinhWeb.Models;

namespace DoAnLapTrinhWeb.Repositories
{
    public class EFTacGiaRepository : ITacGiaRepository
    {
        private readonly ApplicationDbContext _context;
        public EFTacGiaRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<tbTacGia>> GetAllAsync()
        {
            // return await _context.Products.ToListAsync();
            return await _context.tbTacGia
            .Include(p => p.Saches) // Include thông tin về category
            .ToListAsync();
        }
        public async Task<tbTacGia> GetByIdAsync(int id)
        {
            // return await _context.Products.FindAsync(id);
            // lấy thông tin kèm theo category
            return await _context.tbTacGia.Include(p => p.Saches).FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task AddAsync(tbTacGia tacgia)
        {
            _context.tbTacGia.Add(tacgia);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(tbTacGia tacGia)
        {
            _context.tbTacGia.Update(tacGia);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var tacGia = await _context.tbTacGia.FindAsync(id);
            _context.tbTacGia.Remove(tacGia);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> IsTenTacGiaExisted(string tenTacGia)
        {
            return await _context.tbTacGia.AnyAsync(t => t.TenTacGia == tenTacGia);
        }

    }
}