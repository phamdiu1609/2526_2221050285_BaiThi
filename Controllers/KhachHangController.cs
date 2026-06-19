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
    public class KhachHangController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KhachHangController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: KhachHang
        public async Task<IActionResult> Index()
        {
            return View(await _context.KhachHangs.ToListAsync());
        }

        // GET: KhachHang/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachHang = await _context.KhachHangs
                .FirstOrDefaultAsync(m => m.KhachHangId == id);
            if (khachHang == null)
            {
                return NotFound();
            }

            return View(khachHang);
        }

        // GET: KhachHang/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: KhachHang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KhachHangId,Ten,Email")] KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(khachHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(khachHang);
        }

        // GET: KhachHang/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachHang = await _context.KhachHangs.FindAsync(id);
            if (khachHang == null)
            {
                return NotFound();
            }
            return View(khachHang);
        }

        // POST: KhachHang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KhachHangId,Ten,Email")] KhachHang khachHang)
        {
            if (id != khachHang.KhachHangId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(khachHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KhachHangExists(khachHang.KhachHangId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(khachHang);
        }

        // GET: KhachHang/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachHang = await _context.KhachHangs
                .FirstOrDefaultAsync(m => m.KhachHangId == id);
            if (khachHang == null)
            {
                return NotFound();
            }

            return View(khachHang);
        }

        // POST: KhachHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var khachHang = await _context.KhachHangs.FindAsync(id);
            if (khachHang != null)
            {
                _context.KhachHangs.Remove(khachHang);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KhachHangExists(int id)
        {
            return _context.KhachHangs.Any(e => e.KhachHangId == id);
        }
        // Xem đơn hàng đã mua
        public async Task<IActionResult> DonHangDaMua(int id)
        {
            var khachHang = await _context.KhachHangs
            .Include(kh => kh.DonHangs)
            .ThenInclude(dh => dh.ChiTietDonHangs)
                .ThenInclude(ct => ct.SanPham)
            .FirstOrDefaultAsync(kh => kh.KhachHangId == id);

            if (khachHang == null)
            {
                return NotFound();
            }

            return View(khachHang);
        }
    }
}
