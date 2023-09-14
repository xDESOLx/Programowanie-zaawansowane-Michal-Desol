using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Exercise2.Data;
using Exercise2.Models.Refrigerator;

namespace Exercise2.Controllers.Refrigerator
{
    public class MeatsController : Controller
    {
        private readonly Exercise2Context _context;

        public MeatsController(Exercise2Context context)
        {
            _context = context;
        }

        // GET: Meats
        public async Task<IActionResult> Index()
        {
              return _context.Meat != null ? 
                          View(await _context.Meat.ToListAsync()) :
                          Problem("Entity set 'Exercise2Context.Meat'  is null.");
        }

        // GET: Meats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Meat == null)
            {
                return NotFound();
            }

            var meat = await _context.Meat
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meat == null)
            {
                return NotFound();
            }

            return View(meat);
        }

        // GET: Meats/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Meats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ExpiryDate,Type,Weight,FatAmount,ListPrice")] Meat meat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(meat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(meat);
        }

        // GET: Meats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Meat == null)
            {
                return NotFound();
            }

            var meat = await _context.Meat.FindAsync(id);
            if (meat == null)
            {
                return NotFound();
            }
            return View(meat);
        }

        // POST: Meats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ExpiryDate,Type,Weight,FatAmount,ListPrice")] Meat meat)
        {
            if (id != meat.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(meat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeatExists(meat.Id))
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
            return View(meat);
        }

        // GET: Meats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Meat == null)
            {
                return NotFound();
            }

            var meat = await _context.Meat
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meat == null)
            {
                return NotFound();
            }

            return View(meat);
        }

        // POST: Meats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Meat == null)
            {
                return Problem("Entity set 'Exercise2Context.Meat'  is null.");
            }
            var meat = await _context.Meat.FindAsync(id);
            if (meat != null)
            {
                _context.Meat.Remove(meat);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MeatExists(int id)
        {
          return (_context.Meat?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
