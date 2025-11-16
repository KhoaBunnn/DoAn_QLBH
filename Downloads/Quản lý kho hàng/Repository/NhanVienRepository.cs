using Microsoft.EntityFrameworkCore;
using QLKhoHang.Data;
using QLKhoHang.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLKhoHang.Repositories
{
    public class NhanVienRepository : INhanVienRepository
    {
        private readonly QLKhoHangContext _context;

        public NhanVienRepository(QLKhoHangContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<NhanVien>> GetAllAsync()
        {
            return await _context.NhanVien.ToListAsync();
        }

        public async Task<NhanVien> GetByIdAsync(string id)
        {
            return await _context.NhanVien.FindAsync(id);
        }

        public async Task AddAsync(NhanVien nhanVien)
        {
            await _context.NhanVien.AddAsync(nhanVien);
        }

        public void Update(NhanVien nhanVien)
        {
            _context.NhanVien.Update(nhanVien);
        }

        public void Delete(NhanVien nhanVien)
        {
            _context.NhanVien.Remove(nhanVien);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        // Auto mã NV theo format NV001, NV002,...
        public async Task<string> GenerateNewIdAsync()
        {
            var last = await _context.NhanVien
                .OrderByDescending(x => x.MaNV)
                .Select(x => x.MaNV)
                .FirstOrDefaultAsync();

            if (string.IsNullOrEmpty(last))
                return "NV001";

            int number = int.Parse(last.Substring(2));
            number++;

            return "NV" + number.ToString("D3");
        }
    }
}
