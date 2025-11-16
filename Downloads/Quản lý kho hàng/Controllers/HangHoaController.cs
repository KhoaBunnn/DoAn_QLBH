using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QLKhoHang.Models;
using QLKhoHang.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace QLKhoHang.Controllers
{
    public class HangHoaController : Controller
    {
        private readonly IHangHoaRepository _hangRepo;
        private readonly IKhoRepository _khoRepo;
        private readonly ILoaiHangRepository _loaiRepo;

        public HangHoaController(IHangHoaRepository hangRepo, IKhoRepository khoRepo, ILoaiHangRepository loaiRepo)
        {
            _hangRepo = hangRepo;
            _khoRepo = khoRepo;
            _loaiRepo = loaiRepo;
        }

        // GET: HangHoa
        public async Task<IActionResult> Index()
        {
            var list = await _hangRepo.GetAllAsync();
            return View(list);
        }

        // GET: HangHoa/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var hang = await _hangRepo.GetByIdAsync(id);
            if (hang == null) return NotFound();

            return View(hang);
        }

        // GET: HangHoa/Create
        public async Task<IActionResult> Create()
        {
            var model = new HangHoaCreateViewModel
            {
                KhoList = (await _khoRepo.GetAllAsync()).Select(k => new SelectListItem
                {
                    Value = k.MaKho,
                    Text = k.TenKho
                }),
                LoaiHangList = (await _loaiRepo.GetAllAsync()).Select(l => new SelectListItem
                {
                    Value = l.MaLoai,
                    Text = l.TenLoai
                })
            };
            return View(model);
        }

        // POST: HangHoa/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HangHoaCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Reload dropdowns
                model.KhoList = (await _khoRepo.GetAllAsync()).Select(k => new SelectListItem
                {
                    Value = k.MaKho,
                    Text = k.TenKho
                });
                model.LoaiHangList = (await _loaiRepo.GetAllAsync()).Select(l => new SelectListItem
                {
                    Value = l.MaLoai,
                    Text = l.TenLoai
                });
                return View(model);
            }

            var hang = new HangHoa
            {
                MaHang = model.MaHang,
                TenHang = model.TenHang,
                DonViTinh = model.DonViTinh,
                SoLuongTon = model.SoLuongTon,
                GiaNhap = model.GiaNhap,
                GiaXuat = model.GiaXuat,
                MaKho = model.MaKho,
                MaLoai = model.MaLoai
            };

            await _hangRepo.AddAsync(hang);
            await _hangRepo.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: HangHoa/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var hang = await _hangRepo.GetByIdAsync(id);
            if (hang == null) return NotFound();

            var model = new HangHoaEditViewModel
            {
                MaHang = hang.MaHang,
                TenHang = hang.TenHang,
                DonViTinh = hang.DonViTinh,
                SoLuongTon = hang.SoLuongTon,
                GiaNhap = hang.GiaNhap,
                GiaXuat = hang.GiaXuat,
                MaKho = hang.MaKho,
                MaLoai = hang.MaLoai,
                KhoList = (await _khoRepo.GetAllAsync()).Select(k => new SelectListItem
                {
                    Value = k.MaKho,
                    Text = k.TenKho
                }),
                LoaiHangList = (await _loaiRepo.GetAllAsync()).Select(l => new SelectListItem
                {
                    Value = l.MaLoai,
                    Text = l.TenLoai
                })
            };

            return View(model);
        }

        // POST: HangHoa/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(HangHoaEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.KhoList = (await _khoRepo.GetAllAsync()).Select(k => new SelectListItem
                {
                    Value = k.MaKho,
                    Text = k.TenKho
                });
                model.LoaiHangList = (await _loaiRepo.GetAllAsync()).Select(l => new SelectListItem
                {
                    Value = l.MaLoai,
                    Text = l.TenLoai
                });
                return View(model);
            }

            var hang = await _hangRepo.GetByIdAsync(model.MaHang);
            if (hang == null) return NotFound();

            hang.TenHang = model.TenHang;
            hang.DonViTinh = model.DonViTinh;
            hang.SoLuongTon = model.SoLuongTon;
            hang.GiaNhap = model.GiaNhap;
            hang.GiaXuat = model.GiaXuat;
            hang.MaKho = model.MaKho;
            hang.MaLoai = model.MaLoai;

            _hangRepo.Update(hang);
            await _hangRepo.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: HangHoa/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var hang = await _hangRepo.GetByIdAsync(id);
            if (hang == null) return NotFound();

            return View(hang);
        }

        // POST: HangHoa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var hang = await _hangRepo.GetByIdAsync(id);
            if (hang == null) return NotFound();

            _hangRepo.Delete(hang);
            await _hangRepo.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
