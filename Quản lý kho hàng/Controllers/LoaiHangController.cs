using Microsoft.AspNetCore.Mvc;
using QLKhoHang.Models;
using QLKhoHang.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace QLKhoHang.Controllers
{
    public class LoaiHangController : Controller
    {
        private readonly IRepository<LoaiHang> _repo;

        public LoaiHangController(IRepository<LoaiHang> repo)
        {
            _repo = repo;
        }

        // =========================
        // DANH SÁCH LOẠI HÀNG
        // =========================
        public async Task<IActionResult> Index()
        {
            var loaiHangs = await _repo.GetAllAsync();
            return View(loaiHangs);
        }

        // =========================
        // TẠO LOẠI HÀNG MỚI
        // =========================
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaLoai,TenLoai")] LoaiHang loaiHang)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _repo.AddAsync(loaiHang);
                    await _repo.SaveChangesAsync();
                    Console.WriteLine($"==> ĐÃ THÊM: {loaiHang.MaLoai} - {loaiHang.TenLoai}");
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"==> Lỗi khi lưu: {ex.Message}");
                }
            }
            else
            {
                // In ra các lỗi model cụ thể
                foreach (var error in ModelState)
                {
                    foreach (var e in error.Value.Errors)
                    {
                        Console.WriteLine($"==> Lỗi ModelState: {error.Key} - {e.ErrorMessage}");
                    }
                }
            }


            return View(loaiHang);
        }


        // =========================
        // CHỈNH SỬA LOẠI HÀNG
        // =========================
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var loaiHang = await _repo.GetByIdAsync(id);
            if (loaiHang == null)
                return NotFound();

            return View(loaiHang);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("MaLoai,TenLoai")] LoaiHang loaiHang)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repo.Update(loaiHang);
                    await _repo.SaveChangesAsync();
                    TempData["SuccessMessage"] = $"✏️ Đã cập nhật loại hàng: {loaiHang.TenLoai}";
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    TempData["ErrorMessage"] = "❌ Lỗi khi cập nhật loại hàng!";
                }
            }

            return View(loaiHang);
        }

        // =========================
        // XOÁ LOẠI HÀNG
        // =========================
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var loaiHang = await _repo.GetByIdAsync(id);
            if (loaiHang == null)
                return NotFound();

            return View(loaiHang);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string MaLoai)
        {
            var loaiHang = await _repo.GetByIdAsync(MaLoai);
            if (loaiHang != null)
            {
                try
                {
                    _repo.Delete(loaiHang);
                    await _repo.SaveChangesAsync();
                    TempData["SuccessMessage"] = $"🗑️ Đã xoá loại hàng: {loaiHang.TenLoai}";
                }
                catch
                {
                    TempData["ErrorMessage"] = "❌ Lỗi khi xoá loại hàng!";
                }
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

            var loaiHang = await _repo.GetByIdAsync(id);
            if (loaiHang == null)
                return NotFound();

            return View(loaiHang);
        }
    }
}
