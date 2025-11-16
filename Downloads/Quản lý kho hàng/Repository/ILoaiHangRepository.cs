using QLKhoHang.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLKhoHang.Repositories
{
    public interface ILoaiHangRepository
    {
        Task<IEnumerable<LoaiHang>> GetAllAsync();
        Task<LoaiHang> GetByIdAsync(string id);
        Task AddAsync(LoaiHang loai);
        void Update(LoaiHang loai);
        void Delete(LoaiHang loai);
        Task SaveChangesAsync();
    }
}
