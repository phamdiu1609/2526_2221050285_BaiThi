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
    public class LoaiThietBiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoaiThietBiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LoaiThietBi
        public async Task<IActionResult> Index()
        {
            return View(await _context.LoaiThietBis.ToListAsync());
        }

        // GET: LoaiThietBi/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiThietBi = await _context.LoaiThietBis
                .FirstOrDefaultAsync(m => m.LoaiThietBiId == id);
            if (loaiThietBi == null)
            {
                return NotFound();
            }

            return View(loaiThietBi);
        }

        // GET: LoaiThietBi/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LoaiThietBi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LoaiThietBiId,TenLoai,MoTa")] LoaiThietBi loaiThietBi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loaiThietBi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loaiThietBi);
        }

        // GET: LoaiThietBi/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiThietBi = await _context.LoaiThietBis.FindAsync(id);
            if (loaiThietBi == null)
            {
                return NotFound();
            }
            return View(loaiThietBi);
        }

        // POST: LoaiThietBi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LoaiThietBiId,TenLoai,MoTa")] LoaiThietBi loaiThietBi)
        {
            if (id != loaiThietBi.LoaiThietBiId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loaiThietBi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoaiThietBiExists(loaiThietBi.LoaiThietBiId))
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
            return View(loaiThietBi);
        }

        // GET: LoaiThietBi/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiThietBi = await _context.LoaiThietBis
                .FirstOrDefaultAsync(m => m.LoaiThietBiId == id);
            if (loaiThietBi == null)
            {
                return NotFound();
            }

            return View(loaiThietBi);
        }

        // POST: LoaiThietBi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loaiThietBi = await _context.LoaiThietBis.FindAsync(id);
            if (loaiThietBi != null)
            {
                _context.LoaiThietBis.Remove(loaiThietBi);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoaiThietBiExists(int id)
        {
            return _context.LoaiThietBis.Any(e => e.LoaiThietBiId == id);
        }
    }
}
