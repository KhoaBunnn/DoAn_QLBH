using QLKhoHang.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLKhoHang.Repositories
{
    public interface IHangHoaRepository
    {
        Task<IEnumerable<HangHoa>> GetAllAsync();
        Task<HangHoa> GetByIdAsync(string id);
        Task AddAsync(HangHoa hangHoa);
        void Update(HangHoa hangHoa);
        void Delete(HangHoa hangHoa);
        Task SaveChangesAsync();
    }
}
