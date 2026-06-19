using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Models;

namespace MVC.Controllers
{
    public class PhieuNhapController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PhieuNhapController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ================== DANH SÁCH ==================
        public async Task<IActionResult> Index()
        {
            var data = _context.PhieuNhaps.Include(p => p.NhaCungCap);
            return View(await data.ToListAsync());
        }

        // ================== CHI TIẾT ==================
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var phieuNhap = await _context.PhieuNhaps
                .Include(p => p.NhaCungCap)
                .Include(p => p.ChiTietNhaps)
                .ThenInclude(ct => ct.ThietBi)
                .FirstOrDefaultAsync(m => m.PhieuNhapId == id);

            if (phieuNhap == null) return NotFound();

            return View(phieuNhap);
        }

        // ================== TẠO ==================
        public IActionResult Create()
        {
            ViewData["NhaCungCapId"] = new SelectList(_context.NhaCungCaps, "NhaCungCapId", "TenNhaCungCap");
            ViewData["ThietBiId"] = new SelectList(_context.ThietBis, "ThietBiId", "TenThietBi");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PhieuNhap phieuNhap, List<ChiTietNhap> chiTietNhaps)
        {
            // ❗ kiểm tra rỗng
            if (chiTietNhaps == null || !chiTietNhaps.Any())
            {
                ModelState.AddModelError("", "Phải nhập ít nhất 1 thiết bị");
            }

            if (ModelState.IsValid)
            {
                decimal tongTien = 0;

                // 🔥 xử lý chi tiết
                foreach (var item in chiTietNhaps)
                {
                    if (item.ThietBiId == 0 || item.SoLuong <= 0)
                        continue;

                    item.ThanhTien = item.SoLuong * item.DonGiaNhap;
                    tongTien += item.ThanhTien;

                    var tb = await _context.ThietBis.FindAsync(item.ThietBiId);
                    if (tb != null)
                    {
                        tb.SoLuongTon += item.SoLuong;
                    }
                }

                phieuNhap.TongTien = tongTien;

                // lưu phiếu trước
                _context.Add(phieuNhap);
                await _context.SaveChangesAsync();

                // lưu chi tiết
                foreach (var item in chiTietNhaps)
                {
                    if (item.ThietBiId == 0 || item.SoLuong <= 0)
                        continue;

                    item.PhieuNhapId = phieuNhap.PhieuNhapId;
                    _context.Add(item);
                }

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["NhaCungCapId"] = new SelectList(_context.NhaCungCaps, "NhaCungCapId", "TenNhaCungCap");
            ViewData["ThietBiId"] = new SelectList(_context.ThietBis, "ThietBiId", "TenThietBi");

            return View(phieuNhap);
        }

        // ================== SỬA ==================
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var phieuNhap = await _context.PhieuNhaps.FindAsync(id);
            if (phieuNhap == null) return NotFound();

            ViewData["NhaCungCapId"] = new SelectList(_context.NhaCungCaps, "NhaCungCapId", "TenNhaCungCap", phieuNhap.NhaCungCapId);

            return View(phieuNhap);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PhieuNhap phieuNhap)
        {
            if (id != phieuNhap.PhieuNhapId) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(phieuNhap);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(phieuNhap);
        }

        // ================== XOÁ ==================
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var phieuNhap = await _context.PhieuNhaps
                .Include(p => p.NhaCungCap)
                .FirstOrDefaultAsync(m => m.PhieuNhapId == id);

            if (phieuNhap == null) return NotFound();

            return View(phieuNhap);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var phieuNhap = await _context.PhieuNhaps.FindAsync(id);

            if (phieuNhap != null)
            {
                // 🔥 xóa chi tiết trước
                var chiTiet = _context.ChiTietNhaps.Where(x => x.PhieuNhapId == id);
                _context.ChiTietNhaps.RemoveRange(chiTiet);

                _context.PhieuNhaps.Remove(phieuNhap);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}