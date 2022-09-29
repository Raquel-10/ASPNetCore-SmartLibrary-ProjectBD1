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
    public class InvoiceDetailsController : Controller
    {
        private readonly RepositoryContext _context;

        public InvoiceDetailsController(RepositoryContext context)
        {
            _context = context;
        }

        // GET: InvoiceDetails
        public async Task<IActionResult> Index()
        {
            var repositoryContext = _context.InvoiceDetails.Include(i => i.Book).Include(i => i.Factura);
            return View(await repositoryContext.ToListAsync());
        }

        // GET: InvoiceDetails/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.InvoiceDetails == null)
            {
                return NotFound();
            }

            var invoiceDetail = await _context.InvoiceDetails
                .Include(i => i.Book)
                .Include(i => i.Factura)
                .FirstOrDefaultAsync(m => m.InvoiceDetailsId == id);
            if (invoiceDetail == null)
            {
                return NotFound();
            }

            return View(invoiceDetail);
        }

        // GET: InvoiceDetails/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookId");
            ViewData["FacturaId"] = new SelectList(_context.Facturas, "FacturaId", "FacturaId");
            return View();
        }

        // POST: InvoiceDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookId,FacturaId,Price,Cuantity,Iva,Total")] InvoiceDetail invoiceDetail)
        {
            if (ModelState.IsValid)
            {
                invoiceDetail.InvoiceDetailsId = Guid.NewGuid();
                _context.Add(invoiceDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookId", invoiceDetail.BookId);
            ViewData["FacturaId"] = new SelectList(_context.Facturas, "FacturaId", "FacturaId", invoiceDetail.FacturaId);
            return View(invoiceDetail);
        }

        // GET: InvoiceDetails/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.InvoiceDetails == null)
            {
                return NotFound();
            }

            var invoiceDetail = await _context.InvoiceDetails.FindAsync(id);
            if (invoiceDetail == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookId", invoiceDetail.BookId);
            ViewData["FacturaId"] = new SelectList(_context.Facturas, "FacturaId", "FacturaId", invoiceDetail.FacturaId);
            return View(invoiceDetail);
        }

        // POST: InvoiceDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("InvoiceDetailsId,BookId,FacturaId,Price,Cuantity,Iva,Total")] InvoiceDetail invoiceDetail)
        {
            if (id != invoiceDetail.InvoiceDetailsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoiceDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceDetailExists(invoiceDetail.InvoiceDetailsId))
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
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookId", invoiceDetail.BookId);
            ViewData["FacturaId"] = new SelectList(_context.Facturas, "FacturaId", "FacturaId", invoiceDetail.FacturaId);
            return View(invoiceDetail);
        }

        // GET: InvoiceDetails/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.InvoiceDetails == null)
            {
                return NotFound();
            }

            var invoiceDetail = await _context.InvoiceDetails
                .Include(i => i.Book)
                .Include(i => i.Factura)
                .FirstOrDefaultAsync(m => m.InvoiceDetailsId == id);
            if (invoiceDetail == null)
            {
                return NotFound();
            }

            return View(invoiceDetail);
        }

        // POST: InvoiceDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.InvoiceDetails == null)
            {
                return Problem("Entity set 'RepositoryContext.InvoiceDetails'  is null.");
            }
            var invoiceDetail = await _context.InvoiceDetails.FindAsync(id);
            if (invoiceDetail != null)
            {
                _context.InvoiceDetails.Remove(invoiceDetail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceDetailExists(Guid id)
        {
          return _context.InvoiceDetails.Any(e => e.InvoiceDetailsId == id);
        }
    }
}
