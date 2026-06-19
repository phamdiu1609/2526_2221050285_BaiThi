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
    public class ThietBiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ThietBiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ThietBi
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ThietBis.Include(t => t.LoaiThietBi).Include(t => t.NhaCungCap);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ThietBi/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thietBi = await _context.ThietBis
                .Include(t => t.LoaiThietBi)
                .Include(t => t.NhaCungCap)
                .FirstOrDefaultAsync(m => m.ThietBiId == id);
            if (thietBi == null)
            {
                return NotFound();
            }

            return View(thietBi);
        }

        // GET: ThietBi/Create
        public IActionResult Create()
        {
            ViewData["LoaiThietBiId"] = new SelectList(_context.LoaiThietBis, "LoaiThietBiId", "TenLoai");
            ViewData["NhaCungCapId"] = new SelectList(_context.NhaCungCaps, "NhaCungCapId", "TenNhaCungCap");
            return View();
        }

        // POST: ThietBi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ThietBiId,TenThietBi,LoaiThietBiId,NhaCungCapId,GiaBan,SoLuongTon")] ThietBi thietBi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(thietBi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LoaiThietBiId"] = new SelectList(_context.LoaiThietBis, "LoaiThietBiId", "TenLoai", thietBi.LoaiThietBiId);
            ViewData["NhaCungCapId"] = new SelectList(_context.NhaCungCaps, "NhaCungCapId", "TenNhaCungCap", thietBi.NhaCungCapId);
            return View(thietBi);
        }

        // GET: ThietBi/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thietBi = await _context.ThietBis.FindAsync(id);
            if (thietBi == null)
            {
                return NotFound();
            }
            ViewData["LoaiThietBiId"] = new SelectList(_context.LoaiThietBis, "LoaiThietBiId", "TenLoai", thietBi.LoaiThietBiId);
            ViewData["NhaCungCapId"] = new SelectList(_context.NhaCungCaps, "NhaCungCapId", "TenNhaCungCap", thietBi.NhaCungCapId);
            return View(thietBi);
        }

        // POST: ThietBi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ThietBiId,TenThietBi,LoaiThietBiId,NhaCungCapId,GiaBan,SoLuongTon")] ThietBi thietBi)
        {
            if (id != thietBi.ThietBiId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(thietBi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThietBiExists(thietBi.ThietBiId))
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
            ViewData["LoaiThietBiId"] = new SelectList(_context.LoaiThietBis, "LoaiThietBiId", "TenLoai", thietBi.LoaiThietBiId);
            ViewData["NhaCungCapId"] = new SelectList(_context.NhaCungCaps, "NhaCungCapId", "TenNhaCungCap", thietBi.NhaCungCapId);
            return View(thietBi);
        }

        // GET: ThietBi/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thietBi = await _context.ThietBis
                .Include(t => t.LoaiThietBi)
                .Include(t => t.NhaCungCap)
                .FirstOrDefaultAsync(m => m.ThietBiId == id);
            if (thietBi == null)
            {
                return NotFound();
            }

            return View(thietBi);
        }

        // POST: ThietBi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var thietBi = await _context.ThietBis.FindAsync(id);
            if (thietBi != null)
            {
                _context.ThietBis.Remove(thietBi);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThietBiExists(int id)
        {
            return _context.ThietBis.Any(e => e.ThietBiId == id);
        }
    }
}
