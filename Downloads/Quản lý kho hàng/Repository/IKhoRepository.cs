using QLKhoHang.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLKhoHang.Repositories
{
    public interface IKhoRepository
    {
        Task<IEnumerable<Kho>> GetAllAsync();
        Task<Kho> GetByIdAsync(string maKho);
        Task AddAsync(Kho kho);
        void Update(Kho kho);
        void Delete(Kho kho);
        Task SaveChangesAsync();
    }
}
