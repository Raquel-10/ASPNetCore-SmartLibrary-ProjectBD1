using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartLibrary.Entities;
using SmartLibrary.SqlDbContext;

namespace SmartLibrary.Controllers
{
    public class EditorialsController : Controller
    {
        private readonly RepositoryContext _context;

        public EditorialsController(RepositoryContext context)
        {
            _context = context;
        }

        // GET: Editorials
        public async Task<IActionResult> Index()
        {
            var repositoryContext = _context.Editorials.Include(e => e.Country);
            return View(await repositoryContext.ToListAsync());
        }

        // GET: Editorials/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Editorials == null)
            {
                return NotFound();
            }

            var editorial = await _context.Editorials
                .Include(e => e.Country)
                .FirstOrDefaultAsync(m => m.EditorialId == id);
            if (editorial == null)
            {
                return NotFound();
            }

            return View(editorial);
        }

        // GET: Editorials/Create
        public IActionResult Create()
        {
            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "CountryId");
            return View();
        }

        // POST: Editorials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EditorialName,CountryId,IsActive")] Editorial editorial)
        {
            if (ModelState.IsValid)
            {
                editorial.EditorialId = Guid.NewGuid();
                _context.Add(editorial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "CountryId", editorial.CountryId);
            return View(editorial);
        }

        // GET: Editorials/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Editorials == null)
            {
                return NotFound();
            }

            var editorial = await _context.Editorials.FindAsync(id);
            if (editorial == null)
            {
                return NotFound();
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "CountryId", editorial.CountryId);
            return View(editorial);
        }

        // POST: Editorials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("EditorialId,EditorialName,CountryId,IsActive")] Editorial editorial)
        {
            if (id != editorial.EditorialId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(editorial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EditorialExists(editorial.EditorialId))
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
            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "CountryId", editorial.CountryId);
            return View(editorial);
        }

        // GET: Editorials/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Editorials == null)
            {
                return NotFound();
            }

            var editorial = await _context.Editorials
                .Include(e => e.Country)
                .FirstOrDefaultAsync(m => m.EditorialId == id);
            if (editorial == null)
            {
                return NotFound();
            }

            return View(editorial);
        }

        // POST: Editorials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Editorials == null)
            {
                return Problem("Entity set 'RepositoryContext.Editorials'  is null.");
            }
            var editorial = await _context.Editorials.FindAsync(id);
            if (editorial != null)
            {
                _context.Editorials.Remove(editorial);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EditorialExists(Guid id)
        {
          return _context.Editorials.Any(e => e.EditorialId == id);
        }
    }
}
