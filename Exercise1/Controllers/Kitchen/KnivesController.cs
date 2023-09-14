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
    public class KnivesController : Controller
    {
        private readonly Exercise1Context _context;

        public KnivesController(Exercise1Context context)
        {
            _context = context;
        }

        // GET: Knives
        public async Task<IActionResult> Index()
        {
              return _context.Knives != null ? 
                          View(await _context.Knives.ToListAsync()) :
                          Problem("Entity set 'Exercise1Context.Knife'  is null.");
        }

        // GET: Knives/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Knives == null)
            {
                return NotFound();
            }

            var knife = await _context.Knives
                .FirstOrDefaultAsync(m => m.Id == id);
            if (knife == null)
            {
                return NotFound();
            }

            return View(knife);
        }

        // GET: Knives/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Knives/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BladeLength,Weight,BladeType,HandleSubstance,HRC")] Knife knife)
        {
            if (ModelState.IsValid)
            {
                _context.Add(knife);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(knife);
        }

        // GET: Knives/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Knives == null)
            {
                return NotFound();
            }

            var knife = await _context.Knives.FindAsync(id);
            if (knife == null)
            {
                return NotFound();
            }
            return View(knife);
        }

        // POST: Knives/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BladeLength,Weight,BladeType,HandleSubstance,HRC")] Knife knife)
        {
            if (id != knife.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(knife);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KnifeExists(knife.Id))
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
            return View(knife);
        }

        // GET: Knives/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Knives == null)
            {
                return NotFound();
            }

            var knife = await _context.Knives
                .FirstOrDefaultAsync(m => m.Id == id);
            if (knife == null)
            {
                return NotFound();
            }

            return View(knife);
        }

        // POST: Knives/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Knives == null)
            {
                return Problem("Entity set 'Exercise1Context.Knife'  is null.");
            }
            var knife = await _context.Knives.FindAsync(id);
            if (knife != null)
            {
                _context.Knives.Remove(knife);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KnifeExists(int id)
        {
          return (_context.Knives?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
