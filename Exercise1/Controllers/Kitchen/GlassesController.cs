using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Exercise1.Data;
using Exercise1.Models.Kitchen;

namespace Exercise1.Controllers.Kitchen
{
    public class GlassesController : Controller
    {
        private readonly Exercise1Context _context;

        public GlassesController(Exercise1Context context)
        {
            _context = context;
        }

        // GET: Glasses
        public async Task<IActionResult> Index()
        {
              return _context.Glasses != null ? 
                          View(await _context.Glasses.ToListAsync()) :
                          Problem("Entity set 'Exercise1Context.Glass'  is null.");
        }

        // GET: Glasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Glasses == null)
            {
                return NotFound();
            }

            var glass = await _context.Glasses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (glass == null)
            {
                return NotFound();
            }

            return View(glass);
        }

        // GET: Glasses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Glasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Diameter,Weight,Type,Color,Substance")] Glass glass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(glass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(glass);
        }

        // GET: Glasses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Glasses == null)
            {
                return NotFound();
            }

            var glass = await _context.Glasses.FindAsync(id);
            if (glass == null)
            {
                return NotFound();
            }
            return View(glass);
        }

        // POST: Glasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Diameter,Weight,Type,Color,Substance")] Glass glass)
        {
            if (id != glass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(glass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GlassExists(glass.Id))
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
            return View(glass);
        }

        // GET: Glasses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Glasses == null)
            {
                return NotFound();
            }

            var glass = await _context.Glasses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (glass == null)
            {
                return NotFound();
            }

            return View(glass);
        }

        // POST: Glasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Glasses == null)
            {
                return Problem("Entity set 'Exercise1Context.Glass'  is null.");
            }
            var glass = await _context.Glasses.FindAsync(id);
            if (glass != null)
            {
                _context.Glasses.Remove(glass);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GlassExists(int id)
        {
          return (_context.Glasses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
