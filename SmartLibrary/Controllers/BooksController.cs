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
    public class BooksController : Controller
    {
        private readonly RepositoryContext _context;

        public BooksController(RepositoryContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var repositoryContext = _context.Books.Include(b => b.Autor).Include(b => b.Editorial).Include(b => b.Gender);
            return View(await repositoryContext.ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Autor)
                .Include(b => b.Editorial)
                .Include(b => b.Gender)
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewData["AutorId"] = new SelectList(_context.Autors, "AutorId", "AutorId");
            ViewData["EditorialId"] = new SelectList(_context.Editorials, "EditorialId", "EditorialId");
            ViewData["GenderId"] = new SelectList(_context.Genders, "GenderId", "GenderId");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GenderId,AutorId,EditorialId,IsActive")] Book book)
        {
            if (ModelState.IsValid)
            {
                book.BookId = Guid.NewGuid();
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AutorId"] = new SelectList(_context.Autors, "AutorId", "AutorId", book.AutorId);
            ViewData["EditorialId"] = new SelectList(_context.Editorials, "EditorialId", "EditorialId", book.EditorialId);
            ViewData["GenderId"] = new SelectList(_context.Genders, "GenderId", "GenderId", book.GenderId);
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["AutorId"] = new SelectList(_context.Autors, "AutorId", "AutorId", book.AutorId);
            ViewData["EditorialId"] = new SelectList(_context.Editorials, "EditorialId", "EditorialId", book.EditorialId);
            ViewData["GenderId"] = new SelectList(_context.Genders, "GenderId", "GenderId", book.GenderId);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("BookId,GenderId,AutorId,EditorialId,IsActive")] Book book)
        {
            if (id != book.BookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.BookId))
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
            ViewData["AutorId"] = new SelectList(_context.Autors, "AutorId", "AutorId", book.AutorId);
            ViewData["EditorialId"] = new SelectList(_context.Editorials, "EditorialId", "EditorialId", book.EditorialId);
            ViewData["GenderId"] = new SelectList(_context.Genders, "GenderId", "GenderId", book.GenderId);
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Autor)
                .Include(b => b.Editorial)
                .Include(b => b.Gender)
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Books == null)
            {
                return Problem("Entity set 'RepositoryContext.Books'  is null.");
            }
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(Guid id)
        {
          return _context.Books.Any(e => e.BookId == id);
        }
    }
}
