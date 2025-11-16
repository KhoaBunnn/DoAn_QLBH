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

        // =========================
        // DANH SÁCH KHO
        // =========================
        public async Task<IActionResult> Index()
        {
            var khos = await _repo.GetAllAsync();
            return View(khos);
        }

        // =========================
        // TẠO KHO MỚI
        // =========================
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaKho,TenKho,DiaChiKho")] Kho kho)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _repo.AddAsync(kho);
                    await _repo.SaveChangesAsync();
                    TempData["SuccessMessage"] = $"✅ Đã tạo thành công kho: {kho.TenKho}";
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    TempData["ErrorMessage"] = "❌ Lỗi khi lưu dữ liệu. Vui lòng thử lại!";
                }
            }

            return View(kho);
        }


        // =========================
        // CHỈNH SỬA KHO
        // =========================
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var kho = await _repo.GetByIdAsync(id);
            if (kho == null)
                return NotFound();

            return View(kho);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("MaKho,TenKho,DiaChiKho,GhiChu")] Kho kho)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repo.Update(kho);
                    await _repo.SaveChangesAsync();
                    TempData["SuccessMessage"] = $"✏️ Đã cập nhật kho: {kho.TenKho}";
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    TempData["ErrorMessage"] = "❌ Lỗi khi cập nhật kho!";
                }
            }

            return View(kho);
        }

        // =========================
        // XOÁ KHO
        // =========================
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var kho = await _repo.GetByIdAsync(id);
            if (kho == null)
                return NotFound();

            return View(kho);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string MaKho)
        {
            var kho = await _repo.GetByIdAsync(MaKho);
            if (kho != null)
            {
                try
                {
                    _repo.Delete(kho);
                    await _repo.SaveChangesAsync();
                    TempData["SuccessMessage"] = $"🗑️ Đã xoá kho: {kho.TenKho}";
                }
                catch
                {
                    TempData["ErrorMessage"] = "❌ Lỗi khi xoá kho!";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Không tìm thấy kho để xoá!";
            }

            return RedirectToAction(nameof(Index));
        }

        // =========================
        // XEM CHI TIẾT
        // =========================
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
