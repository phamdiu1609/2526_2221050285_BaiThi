using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Models;
using OfficeOpenXml;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace MVC.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ================== CRUD ==================

        // GET: Student
        public async Task<IActionResult> Index()
        {
            return View(await _context.Students.ToListAsync());
        }

        // GET: Student/Details/SV01
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.StudentCode == id);

            if (student == null)
                return NotFound();

            return View(student);
        }

        // GET: Student/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student)
        {
            if (ModelState.IsValid)
            {
                if (_context.Students.Any(s => s.StudentCode == student.StudentCode))
                {
                    ModelState.AddModelError("", "Mã sinh viên đã tồn tại!");
                    return View(student);
                }

                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Student/Edit/SV01
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var student = await _context.Students.FindAsync(id);

            if (student == null)
                return NotFound();

            return View(student);
        }

        // POST: Student/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Student student)
        {
            if (id != student.StudentCode)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentCode))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Student/Delete/SV01
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.StudentCode == id);

            if (student == null)
                return NotFound();

            return View(student);
        }

        // POST: Student/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student != null)
            {
                _context.Students.Remove(student);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(string id)
        {
            return _context.Students.Any(e => e.StudentCode == id);
        }

        // ================== IMPORT EXCEL ==================

        // GET: Student/ImportExcel
        public IActionResult ImportExcel()
        {
            return View();
        }

        // POST: Student/ImportExcel
        [HttpPost]
        public async Task<IActionResult> ImportExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ViewBag.Message = "Vui lòng chọn file!";
                return View();
            }

            // EPPlus 5.x: dùng LicenseContext cũ
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);

                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    int rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        var student = new Student
                        {
                            StudentCode = worksheet.Cells[row, 1].Text,
                            FullName = worksheet.Cells[row, 2].Text,
                            Age = int.TryParse(worksheet.Cells[row, 3].Text, out int age) ? age : 0,
                            Email = worksheet.Cells[row, 4].Text
                        };

                        if (!_context.Students.Any(s => s.StudentCode == student.StudentCode))
                        {
                            _context.Students.Add(student);
                        }
                    }

                    await _context.SaveChangesAsync();
                }
            }

            ViewBag.Message = "Import thành công!";
            return View();
        }
    }
}
