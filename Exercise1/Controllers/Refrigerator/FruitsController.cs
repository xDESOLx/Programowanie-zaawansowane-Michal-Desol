using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Exercise1.Data;
using Exercise1.Models.Refrigerator;

namespace Exercise1.Controllers.Refrigerator
{
    public class FruitsController : Controller
    {
        private readonly Exercise1Context _context;

        public FruitsController(Exercise1Context context)
        {
            _context = context;
        }

        // GET: Fruits
        public async Task<IActionResult> Index()
        {
              return _context.Fruits != null ? 
                          View(await _context.Fruits.ToListAsync()) :
                          Problem("Entity set 'Exercise1Context.Fruit'  is null.");
        }

        // GET: Fruits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Fruits == null)
            {
                return NotFound();
            }

            var fruit = await _context.Fruits
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fruit == null)
            {
                return NotFound();
            }

            return View(fruit);
        }

        // GET: Fruits/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fruits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ExpiryDate,Type,Weight,VitaminCContent,ListPrice")] Fruit fruit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fruit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fruit);
        }

        // GET: Fruits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Fruits == null)
            {
                return NotFound();
            }

            var fruit = await _context.Fruits.FindAsync(id);
            if (fruit == null)
            {
                return NotFound();
            }
            return View(fruit);
        }

        // POST: Fruits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ExpiryDate,Type,Weight,VitaminCContent,ListPrice")] Fruit fruit)
        {
            if (id != fruit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fruit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FruitExists(fruit.Id))
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
            return View(fruit);
        }

        // GET: Fruits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Fruits == null)
            {
                return NotFound();
            }

            var fruit = await _context.Fruits
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fruit == null)
            {
                return NotFound();
            }

            return View(fruit);
        }

        // POST: Fruits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Fruits == null)
            {
                return Problem("Entity set 'Exercise1Context.Fruit'  is null.");
            }
            var fruit = await _context.Fruits.FindAsync(id);
            if (fruit != null)
            {
                _context.Fruits.Remove(fruit);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FruitExists(int id)
        {
          return (_context.Fruits?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
