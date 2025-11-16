using Microsoft.AspNetCore.Mvc;
using QLKhoHang.Models;
using QLKhoHang.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace QLKhoHang.Controllers
{
    public class LoaiHangController : Controller
    {
        private readonly ILoaiHangRepository _repo;

        public LoaiHangController(ILoaiHangRepository repo)
        {
            _repo = repo;
        }

        // GET: LoaiHangHoa
        public async Task<IActionResult> Index()
        {
            var list = await _repo.GetAllAsync();
            return View(list);
        }

        // GET: LoaiHangHoa/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();
            var loai = await _repo.GetByIdAsync(id);
            if (loai == null) return NotFound();
            return View(loai);
        }

        // GET: LoaiHangHoa/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LoaiHangHoa/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LoaiHangCreateViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            // Tự sinh MaLoai
            string prefix = "LHH";
            int nextNumber = 1;
            var loais = await _repo.GetAllAsync();
            if (loais.Any())
            {
                var maxCode = loais.Max(l => l.MaLoai);
                if (maxCode.StartsWith(prefix) && int.TryParse(maxCode.Substring(prefix.Length), out int current))
                    nextNumber = current + 1;
            }

            var loai = new LoaiHang
            {
                MaLoai = prefix + nextNumber.ToString("D3"),
                TenLoai = model.TenLoai
            };

            await _repo.AddAsync(loai);
            await _repo.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: LoaiHangHoa/Edit/5
        // GET: LoaiHang/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var loai = await _repo.GetByIdAsync(id);
            if (loai == null) return NotFound();

            // Map entity sang ViewModel
            var model = new LoaiHangEditViewModel
            {
                MaLoai = loai.MaLoai,
                TenLoai = loai.TenLoai
            };

            return View(model);
        }

        // POST: LoaiHang/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(LoaiHangEditViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var loai = await _repo.GetByIdAsync(model.MaLoai);
            if (loai == null) return NotFound();

            // Cập nhật thông tin
            loai.TenLoai = model.TenLoai;

            _repo.Update(loai);
            await _repo.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        // GET: LoaiHangHoa/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();
            var loai = await _repo.GetByIdAsync(id);
            if (loai == null) return NotFound();
            return View(loai);
        }

        // POST: LoaiHangHoa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var loai = await _repo.GetByIdAsync(id);
            if (loai == null) return NotFound();

            _repo.Delete(loai);
            await _repo.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
