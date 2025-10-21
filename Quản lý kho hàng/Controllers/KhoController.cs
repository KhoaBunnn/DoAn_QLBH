using Microsoft.AspNetCore.Mvc;
using QLKhoHang.Models;
using QLKhoHang.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace QLKhoHang.Controllers
{
    public class KhoController : Controller
    {
        private readonly IRepository<Kho> _repo;

        public KhoController(IRepository<Kho> repo)
        {
            _repo = repo;
        }

        // GET: Kho
        public async Task<IActionResult> Index()
        {
            var khos = await _repo.GetAllAsync();
            return View(khos);
        }

        // GET: Kho/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Kho/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenKho,DiaChiKho")] Kho kho)
        {
            if (ModelState.IsValid)
            {
                // Lấy danh sách tất cả kho hiện có
                var khos = await _repo.GetAllAsync();

                // Lấy mã lớn nhất (ví dụ KHO005)
                var lastKho = khos.OrderByDescending(k => k.MaKho).FirstOrDefault();

                int nextNumber = 1;
                if (lastKho != null)
                {
                    // Cắt phần số ra (bỏ "KHO")
                    string numberPart = lastKho.MaKho.Substring(3);
                    if (int.TryParse(numberPart, out int parsed))
                    {
                        nextNumber = parsed + 1;
                    }
                }

                // Sinh mã mới
                kho.MaKho = "KHO" + nextNumber.ToString("000");

                // Lưu vào DB
                await _repo.AddAsync(kho);
                await _repo.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(kho);
        }

        // GET: Kho/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var kho = await _repo.GetByIdAsync(id);
            if (kho == null)
                return NotFound();

            return View(kho);
        }

        // POST: Kho/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("MaKho,TenKho,DiaChiKho")] Kho kho)
        {
            if (ModelState.IsValid)
            {
                _repo.Update(kho);
                await _repo.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kho);
        }

        // GET: Kho/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var kho = await _repo.GetByIdAsync(id);
            if (kho == null)
                return NotFound();

            return View(kho);
        }

        // POST: Kho/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string MaKho)
        {
            var kho = await _repo.GetByIdAsync(MaKho);
            if (kho != null)
            {
                _repo.Delete(kho);
                await _repo.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Kho/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var kho = await _repo.GetByIdAsync(id);
            if (kho == null)
                return NotFound();

            return View(kho);
        }
    }
}
