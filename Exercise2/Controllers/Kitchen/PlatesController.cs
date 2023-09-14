using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Exercise2.Data;
using Exercise2.Models.Kitchen;

namespace Exercise2.Controllers.Kitchen
{
    public class PlatesController : Controller
    {
        private readonly Exercise2Context _context;

        public PlatesController(Exercise2Context context)
        {
            _context = context;
        }

        // GET: Plates
        public async Task<IActionResult> Index()
        {
              return _context.Plates != null ? 
                          View(await _context.Plates.ToListAsync()) :
                          Problem("Entity set 'Exercise2Context.Plate'  is null.");
        }

        // GET: Plates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Plates == null)
            {
                return NotFound();
            }

            var plate = await _context.Plates
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plate == null)
            {
                return NotFound();
            }

            return View(plate);
        }

        // GET: Plates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Plates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Diameter,Weight,Color,Substance,Height")] Plate plate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(plate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(plate);
        }

        // GET: Plates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Plates == null)
            {
                return NotFound();
            }

            var plate = await _context.Plates.FindAsync(id);
            if (plate == null)
            {
                return NotFound();
            }
            return View(plate);
        }

        // POST: Plates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Diameter,Weight,Color,Substance,Height")] Plate plate)
        {
            if (id != plate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlateExists(plate.Id))
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
            return View(plate);
        }

        // GET: Plates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Plates == null)
            {
                return NotFound();
            }

            var plate = await _context.Plates
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plate == null)
            {
                return NotFound();
            }

            return View(plate);
        }

        // POST: Plates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Plates == null)
            {
                return Problem("Entity set 'Exercise2Context.Plate'  is null.");
            }
            var plate = await _context.Plates.FindAsync(id);
            if (plate != null)
            {
                _context.Plates.Remove(plate);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlateExists(int id)
        {
          return (_context.Plates?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
