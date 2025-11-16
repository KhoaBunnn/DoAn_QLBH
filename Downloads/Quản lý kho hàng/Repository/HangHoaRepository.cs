using Microsoft.EntityFrameworkCore;
using QLKhoHang.Data;
using QLKhoHang.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLKhoHang.Repositories
{
    public class HangHoaRepository : IHangHoaRepository
    {
        private readonly QLKhoHangContext _context;

        public HangHoaRepository(QLKhoHangContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HangHoa>> GetAllAsync()
        {
            return await _context.HangHoa
                                 .Include(h => h.Kho)
                                 .Include(h => h.LoaiHang)
                                 .ToListAsync();
        }

        public async Task<HangHoa> GetByIdAsync(string id)
        {
            return await _context.HangHoa
                                 .Include(h => h.Kho)
                                 .Include(h => h.LoaiHang)
                                 .FirstOrDefaultAsync(h => h.MaHang == id);
        }

        public async Task AddAsync(HangHoa hangHoa)
        {
            await _context.HangHoa.AddAsync(hangHoa);
        }

        public void Update(HangHoa hangHoa)
        {
            _context.HangHoa.Update(hangHoa);
        }

        public void Delete(HangHoa hangHoa)
        {
            _context.HangHoa.Remove(hangHoa);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
