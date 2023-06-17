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
    public class AuthorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Authors
        public async Task<IActionResult> Index()
        {
              return _context.AuthorModels != null ? 
                          View(await _context.AuthorModels.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.AuthorModels'  is null.");
        }

        // GET: Authors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AuthorModels == null)
            {
                return NotFound();
            }

            var authorModel = await _context.AuthorModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (authorModel == null)
            {
                return NotFound();
            }

            return View(authorModel);
        }

        // GET: Authors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] AuthorModel authorModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(authorModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(authorModel);
        }

        // GET: Authors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AuthorModels == null)
            {
                return NotFound();
            }

            var authorModel = await _context.AuthorModels.FindAsync(id);
            if (authorModel == null)
            {
                return NotFound();
            }
            return View(authorModel);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] AuthorModel authorModel)
        {
            if (id != authorModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(authorModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorModelExists(authorModel.Id))
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
            return View(authorModel);
        }

        // GET: Authors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AuthorModels == null)
            {
                return NotFound();
            }

            var authorModel = await _context.AuthorModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (authorModel == null)
            {
                return NotFound();
            }

            return View(authorModel);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AuthorModels == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AuthorModels'  is null.");
            }
            var authorModel = await _context.AuthorModels.FindAsync(id);
            if (authorModel != null)
            {
                _context.AuthorModels.Remove(authorModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorModelExists(int id)
        {
          return (_context.AuthorModels?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
