using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BooksCatalog.Data;
using BooksCatalog.Models;

namespace BooksCatalog.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
              return _context.BookModels != null ? 
                          View(await _context.BookModels.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.BookModels'  is null.");
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BookModels == null)
            {
                return NotFound();
            }

            var bookModel = await _context.BookModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookModel == null)
            {
                return NotFound();
            }

            return View(bookModel);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Genre")] BookModel bookModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bookModel);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BookModels == null)
            {
                return NotFound();
            }

            var bookModel = await _context.BookModels.FindAsync(id);
            if (bookModel == null)
            {
                return NotFound();
            }
            return View(bookModel);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Genre")] BookModel bookModel)
        {
            if (id != bookModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookModelExists(bookModel.Id))
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
            return View(bookModel);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BookModels == null)
            {
                return NotFound();
            }

            var bookModel = await _context.BookModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookModel == null)
            {
                return NotFound();
            }

            return View(bookModel);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BookModels == null)
            {
                return Problem("Entity set 'ApplicationDbContext.BookModels'  is null.");
            }
            var bookModel = await _context.BookModels.FindAsync(id);
            if (bookModel != null)
            {
                _context.BookModels.Remove(bookModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookModelExists(int id)
        {
          return (_context.BookModels?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
