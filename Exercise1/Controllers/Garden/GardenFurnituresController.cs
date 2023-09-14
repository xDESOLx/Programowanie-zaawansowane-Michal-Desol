using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Exercise1.Data;
using Exercise1.Models.Garden;

namespace Exercise1.Controllers.Garden
{
    public class GardenFurnituresController : Controller
    {
        private readonly Exercise1Context _context;

        public GardenFurnituresController(Exercise1Context context)
        {
            _context = context;
        }

        // GET: GardenFurnitures
        public async Task<IActionResult> Index()
        {
              return _context.GardenFurniture != null ? 
                          View(await _context.GardenFurniture.ToListAsync()) :
                          Problem("Entity set 'Exercise1Context.GardenFurniture'  is null.");
        }

        // GET: GardenFurnitures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GardenFurniture == null)
            {
                return NotFound();
            }

            var gardenFurniture = await _context.GardenFurniture
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gardenFurniture == null)
            {
                return NotFound();
            }

            return View(gardenFurniture);
        }

        // GET: GardenFurnitures/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GardenFurnitures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Substance,Weight,Color,Type,ListPrice")] GardenFurniture gardenFurniture)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gardenFurniture);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gardenFurniture);
        }

        // GET: GardenFurnitures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GardenFurniture == null)
            {
                return NotFound();
            }

            var gardenFurniture = await _context.GardenFurniture.FindAsync(id);
            if (gardenFurniture == null)
            {
                return NotFound();
            }
            return View(gardenFurniture);
        }

        // POST: GardenFurnitures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Substance,Weight,Color,Type,ListPrice")] GardenFurniture gardenFurniture)
        {
            if (id != gardenFurniture.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gardenFurniture);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GardenFurnitureExists(gardenFurniture.Id))
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
            return View(gardenFurniture);
        }

        // GET: GardenFurnitures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GardenFurniture == null)
            {
                return NotFound();
            }

            var gardenFurniture = await _context.GardenFurniture
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gardenFurniture == null)
            {
                return NotFound();
            }

            return View(gardenFurniture);
        }

        // POST: GardenFurnitures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GardenFurniture == null)
            {
                return Problem("Entity set 'Exercise1Context.GardenFurniture'  is null.");
            }
            var gardenFurniture = await _context.GardenFurniture.FindAsync(id);
            if (gardenFurniture != null)
            {
                _context.GardenFurniture.Remove(gardenFurniture);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GardenFurnitureExists(int id)
        {
          return (_context.GardenFurniture?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
