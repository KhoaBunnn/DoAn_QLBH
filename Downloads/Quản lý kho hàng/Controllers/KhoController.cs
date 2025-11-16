using Microsoft.AspNetCore.Mvc;
using QLKhoHang.Models;
using QLKhoHang.Repositories;
using Quản_lý_kho_hàng.Models;
using System.Threading.Tasks;

namespace QLKhoHang.Controllers
{
    public class KhoController : Controller
    {
        private readonly IKhoRepository _khoRepo;

        public KhoController(IKhoRepository khoRepo)
        {
            _khoRepo = khoRepo;
        }

        // -------------------------------------------------------------------
        // GET: Kho
        // -------------------------------------------------------------------
        public async Task<IActionResult> Index()
        {
            var khos = await _khoRepo.GetAllAsync();
            return View(khos);
        }

        // -------------------------------------------------------------------
        // GET: Kho/Details/5
        // -------------------------------------------------------------------
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var kho = await _khoRepo.GetByIdAsync(id);
            if (kho == null) return NotFound();

            return View(kho);
        }

        // -------------------------------------------------------------------
        // GET: Kho/Create
        // -------------------------------------------------------------------
        public IActionResult Create()
        {
            return View();
        }

        // -------------------------------------------------------------------
        // POST: Kho/Create
        // -------------------------------------------------------------------
        // -------------------------------------------------------------------
        // POST: Kho/Create
        // -------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KhoCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Sinh MaKho
            string prefix = "KHO";
            int nextNumber = 1;
            var khos = await _khoRepo.GetAllAsync();
            if (khos.Any())
            {
                var maxCode = khos.Max(k => k.MaKho);
                if (maxCode.StartsWith(prefix) && int.TryParse(maxCode.Substring(prefix.Length), out int current))
                    nextNumber = current + 1;
            }

            var kho = new Kho
            {
                MaKho = prefix + nextNumber.ToString("D3"),
                TenKho = model.TenKho,
                DiaChiKho = model.DiaChiKho
            };

            await _khoRepo.AddAsync(kho);
            await _khoRepo.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        // -------------------------------------------------------------------
        // GET: Kho/Edit/5
        // -------------------------------------------------------------------
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var kho = await _khoRepo.GetByIdAsync(id);
            if (kho == null) return NotFound();

            return View(kho);
        }

        // -------------------------------------------------------------------
        // POST: Kho/Edit/5
        // -------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(KhoEditViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var kho = await _khoRepo.GetByIdAsync(model.MaKho);
            if (kho == null) return NotFound();

            kho.TenKho = model.TenKho;
            kho.DiaChiKho = model.DiaChiKho;

            _khoRepo.Update(kho);
            await _khoRepo.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // -------------------------------------------------------------------
        // GET: Kho/Delete/5
        // -------------------------------------------------------------------
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var kho = await _khoRepo.GetByIdAsync(id);
            if (kho == null) return NotFound();

            return View(kho);
        }

        // -------------------------------------------------------------------
        // POST: Kho/Delete/5
        // -------------------------------------------------------------------
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var kho = await _khoRepo.GetByIdAsync(id);
            if (kho == null) return NotFound();

            _khoRepo.Delete(kho);
            await _khoRepo.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
