using Microsoft.EntityFrameworkCore;
using QLKhoHang.Data;
using QLKhoHang.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLKhoHang.Repositories
{
    public class KhoRepository : IKhoRepository
    {
        private readonly QLKhoHangContext _context;

        public KhoRepository(QLKhoHangContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Kho>> GetAllAsync()
        {
            return await _context.Kho.ToListAsync();
        }

        public async Task<Kho> GetByIdAsync(string id)
        {
            return await _context.Kho.FindAsync(id);
        }

        public async Task AddAsync(Kho kho)
        {
            await _context.Kho.AddAsync(kho);
        }

        public void Update(Kho kho)
        {
            _context.Kho.Update(kho);
        }

        public void Delete(Kho kho)
        {
            _context.Kho.Remove(kho);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
