using QLKhoHang.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLKhoHang.Repositories
{
    public interface INhanVienRepository
    {
        Task<IEnumerable<NhanVien>> GetAllAsync();
        Task<NhanVien> GetByIdAsync(string id);
        Task AddAsync(NhanVien nhanVien);
        void Update(NhanVien nhanVien);
        void Delete(NhanVien nhanVien);
        Task SaveChangesAsync();

        Task<string> GenerateNewIdAsync(); // Auto mã nhân viên: NV001
    }
}
