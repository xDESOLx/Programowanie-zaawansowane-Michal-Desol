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
    public class ToolsController : Controller
    {
        private readonly Exercise1Context _context;

        public ToolsController(Exercise1Context context)
        {
            _context = context;
        }

        // GET: Tools
        public async Task<IActionResult> Index()
        {
              return _context.Tools != null ? 
                          View(await _context.Tools.ToListAsync()) :
                          Problem("Entity set 'Exercise1Context.Tool'  is null.");
        }

        // GET: Tools/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tools == null)
            {
                return NotFound();
            }

            var tool = await _context.Tools
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tool == null)
            {
                return NotFound();
            }

            return View(tool);
        }

        // GET: Tools/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tools/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,Substance,Weight,Length,ListPrice")] Tool tool)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tool);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tool);
        }

        // GET: Tools/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tools == null)
            {
                return NotFound();
            }

            var tool = await _context.Tools.FindAsync(id);
            if (tool == null)
            {
                return NotFound();
            }
            return View(tool);
        }

        // POST: Tools/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,Substance,Weight,Length,ListPrice")] Tool tool)
        {
            if (id != tool.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tool);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToolExists(tool.Id))
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
            return View(tool);
        }

        // GET: Tools/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tools == null)
            {
                return NotFound();
            }

            var tool = await _context.Tools
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tool == null)
            {
                return NotFound();
            }

            return View(tool);
        }

        // POST: Tools/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tools == null)
            {
                return Problem("Entity set 'Exercise1Context.Tool'  is null.");
            }
            var tool = await _context.Tools.FindAsync(id);
            if (tool != null)
            {
                _context.Tools.Remove(tool);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToolExists(int id)
        {
          return (_context.Tools?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
