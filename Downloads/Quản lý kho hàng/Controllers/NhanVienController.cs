using Microsoft.AspNetCore.Mvc;
using QLKhoHang.Models;
using QLKhoHang.Repositories;
using System.Threading.Tasks;

namespace QLKhoHang.Controllers
{
    public class NhanVienController : Controller
    {
        private readonly INhanVienRepository _repo;

        public NhanVienController(INhanVienRepository repo)
        {
            _repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _repo.GetAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Details(string id)
        {
            var nv = await _repo.GetByIdAsync(id);
            if (nv == null) return NotFound();
            return View(nv);
        }

        // ====================== CREATE ========================
        public async Task<IActionResult> Create()
        {
            var newId = await _repo.GenerateNewIdAsync();
            var model = new NhanVien
            {
                MaNV = newId
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NhanVien nv)
        {
            // Auto-generate ID (because MaNV is [BindNever])
            if (string.IsNullOrEmpty(nv.MaNV))
                nv.MaNV = await _repo.GenerateNewIdAsync();

            // Remove auto properties
            ModelState.Remove("MaNV");
            ModelState.Remove("PhieuNhap");
            ModelState.Remove("PhieuXuat");

            if (!ModelState.IsValid)
            {
                return View(nv);
            }

            await _repo.AddAsync(nv);
            await _repo.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // ====================== EDIT ========================
        public async Task<IActionResult> Edit(string id)
        {
            var nv = await _repo.GetByIdAsync(id);
            if (nv == null) return NotFound();
            return View(nv);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(NhanVien nv)
        {
            ModelState.Remove("MaNV");
            ModelState.Remove("PhieuNhap");
            ModelState.Remove("PhieuXuat");

            if (!ModelState.IsValid)
            {
                return View(nv);
            }

            var existing = await _repo.GetByIdAsync(nv.MaNV);
            if (existing == null) return NotFound();

            existing.TenNV = nv.TenNV;
            existing.SDT = nv.SDT;
            existing.DiaChi = nv.DiaChi;

            _repo.Update(existing);
            await _repo.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // ====================== DELETE ========================
        public async Task<IActionResult> Delete(string id)
        {
            var nv = await _repo.GetByIdAsync(id);
            if (nv == null) return NotFound();
            return View(nv);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var nv = await _repo.GetByIdAsync(id);
            if (nv == null) return NotFound();

            _repo.Delete(nv);
            await _repo.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
