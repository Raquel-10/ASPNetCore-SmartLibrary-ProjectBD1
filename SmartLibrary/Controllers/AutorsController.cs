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
    public class AutorsController : Controller
    {
        private readonly RepositoryContext _context;

        public AutorsController(RepositoryContext context)
        {
            _context = context;
        }

        // GET: Autors
        public async Task<IActionResult> Index()
        {
            var repositoryContext = _context.Autors.Include(a => a.Country).Include(a => a.Person);
            return View(await repositoryContext.ToListAsync());
        }

        // GET: Autors/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Autors == null)
            {
                return NotFound();
            }

            var autor = await _context.Autors
                .Include(a => a.Country)
                .Include(a => a.Person)
                .FirstOrDefaultAsync(m => m.AutorId == id);
            if (autor == null)
            {
                return NotFound();
            }

            return View(autor);
        }

        // GET: Autors/Create
        public IActionResult Create()
        {
            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "CountryId");
            ViewData["PersonId"] = new SelectList(_context.People, "PersonId", "PersonId");
            return View();
        }

        // POST: Autors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CountryId,PersonId,CreationDate,IsActive")] Autor autor)
        {
            if (ModelState.IsValid)
            {
                autor.AutorId = Guid.NewGuid();
                _context.Add(autor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "CountryId", autor.CountryId);
            ViewData["PersonId"] = new SelectList(_context.People, "PersonId", "PersonId", autor.PersonId);
            return View(autor);
        }

        // GET: Autors/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Autors == null)
            {
                return NotFound();
            }

            var autor = await _context.Autors.FindAsync(id);
            if (autor == null)
            {
                return NotFound();
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "CountryId", autor.CountryId);
            ViewData["PersonId"] = new SelectList(_context.People, "PersonId", "PersonId", autor.PersonId);
            return View(autor);
        }

        // POST: Autors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AutorId,CountryId,PersonId,CreationDate,IsActive")] Autor autor)
        {
            if (id != autor.AutorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(autor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutorExists(autor.AutorId))
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
            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "CountryId", autor.CountryId);
            ViewData["PersonId"] = new SelectList(_context.People, "PersonId", "PersonId", autor.PersonId);
            return View(autor);
        }

        // GET: Autors/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Autors == null)
            {
                return NotFound();
            }

            var autor = await _context.Autors
                .Include(a => a.Country)
                .Include(a => a.Person)
                .FirstOrDefaultAsync(m => m.AutorId == id);
            if (autor == null)
            {
                return NotFound();
            }

            return View(autor);
        }

        // POST: Autors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Autors == null)
            {
                return Problem("Entity set 'RepositoryContext.Autors'  is null.");
            }
            var autor = await _context.Autors.FindAsync(id);
            if (autor != null)
            {
                _context.Autors.Remove(autor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AutorExists(Guid id)
        {
          return _context.Autors.Any(e => e.AutorId == id);
        }
    }
}
