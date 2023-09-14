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
    public class TreesController : Controller
    {
        private readonly Exercise1Context _context;

        public TreesController(Exercise1Context context)
        {
            _context = context;
        }

        // GET: Trees
        public async Task<IActionResult> Index()
        {
              return _context.Trees != null ? 
                          View(await _context.Trees.ToListAsync()) :
                          Problem("Entity set 'Exercise1Context.Tree'  is null.");
        }

        // GET: Trees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Trees == null)
            {
                return NotFound();
            }

            var tree = await _context.Trees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tree == null)
            {
                return NotFound();
            }

            return View(tree);
        }

        // GET: Trees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Trees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Height,Type,PlantingDate,TrunkDiameter,LeafColor")] Tree tree)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tree);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tree);
        }

        // GET: Trees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Trees == null)
            {
                return NotFound();
            }

            var tree = await _context.Trees.FindAsync(id);
            if (tree == null)
            {
                return NotFound();
            }
            return View(tree);
        }

        // POST: Trees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Height,Type,PlantingDate,TrunkDiameter,LeafColor")] Tree tree)
        {
            if (id != tree.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tree);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TreeExists(tree.Id))
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
            return View(tree);
        }

        // GET: Trees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Trees == null)
            {
                return NotFound();
            }

            var tree = await _context.Trees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tree == null)
            {
                return NotFound();
            }

            return View(tree);
        }

        // POST: Trees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Trees == null)
            {
                return Problem("Entity set 'Exercise1Context.Tree'  is null.");
            }
            var tree = await _context.Trees.FindAsync(id);
            if (tree != null)
            {
                _context.Trees.Remove(tree);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TreeExists(int id)
        {
          return (_context.Trees?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
