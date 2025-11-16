using Microsoft.EntityFrameworkCore;
using QLKhoHang.Data;
using QLKhoHang.Models;
using QLKhoHang.Repositories;

public class LoaiHangRepository : ILoaiHangRepository
{
    private readonly QLKhoHangContext _context;

    public LoaiHangRepository(QLKhoHangContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<LoaiHang>> GetAllAsync()
    {
        return await _context.LoaiHang.ToListAsync();
    }

    public async Task<LoaiHang> GetByIdAsync(string id)
    {
        return await _context.LoaiHang.FindAsync(id);
    }

    public async Task AddAsync(LoaiHang loai)
    {
        await _context.LoaiHang.AddAsync(loai);
    }

    public void Update(LoaiHang loai)
    {
        _context.LoaiHang.Update(loai);
    }

    public void Delete(LoaiHang loai)
    {
        _context.LoaiHang.Remove(loai);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
