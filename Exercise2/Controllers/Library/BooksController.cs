using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Exercise2.Data;
using Exercise2.Models.Library;

namespace Exercise2.Controllers.Library
{
    public class BooksController : Controller
    {
        private readonly Exercise2Context _context;

        public BooksController(Exercise2Context context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var Exercise2Context = _context.Books.Include(b => b.Author).Include(b => b.Genre);
            return View(await Exercise2Context.ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public async Task<IActionResult> Create()
        {
            ViewData["AuthorId"] = await _context.Authors.Select(a => new SelectListItem($"{a.FirstName} {a.LastName}", a.Id.ToString())).ToListAsync();
            ViewData["GenreId"] = await _context.Genres.Select(g => new SelectListItem(g.Title, g.Id.ToString())).ToListAsync();
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,YearOfRelease,GenreId,AuthorId,PagesCount,ListPrice")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = await _context.Authors.Select(a => new SelectListItem($"{a.FirstName} {a.LastName}", a.Id.ToString())).ToListAsync();
            ViewData["GenreId"] = await _context.Genres.Select(g => new SelectListItem(g.Title, g.Id.ToString())).ToListAsync();
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
            ViewData["AuthorId"] = await _context.Authors.Select(a => new SelectListItem($"{a.FirstName} {a.LastName}", a.Id.ToString())).ToListAsync();
            ViewData["GenreId"] = await _context.Genres.Select(g => new SelectListItem(g.Title, g.Id.ToString())).ToListAsync();
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,YearOfRelease,GenreId,AuthorId,PagesCount,ListPrice")] Book book)
        {
            if (id != book.Id)
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
                    if (!BookExists(book.Id))
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
            ViewData["AuthorId"] = await _context.Authors.Select(a => new SelectListItem($"{a.FirstName} {a.LastName}", a.Id.ToString())).ToListAsync();
            ViewData["GenreId"] = await _context.Genres.Select(g => new SelectListItem(g.Title, g.Id.ToString())).ToListAsync();
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Books == null)
            {
                return Problem("Entity set 'Exercise2Context.Book'  is null.");
            }
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
          return (_context.Books?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
