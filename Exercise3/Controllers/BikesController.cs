using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Exercise3.Data;
using Exercise3.Models;

namespace Exercise3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BikesController : ControllerBase
    {
        private readonly Exercise3Context _context;

        public BikesController(Exercise3Context context)
        {
            _context = context;
        }

        // GET: api/Bikes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bike>>> GetBike()
        {
          if (_context.Bike == null)
          {
              return NotFound();
          }
            return await _context.Bike.ToListAsync();
        }

        // GET: api/Bikes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bike>> GetBike(int id)
        {
          if (_context.Bike == null)
          {
              return NotFound();
          }
            var bike = await _context.Bike.FindAsync(id);

            if (bike == null)
            {
                return NotFound();
            }

            return bike;
        }

        // PUT: api/Bikes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBike(int id, Bike bike)
        {
            if (id != bike.Id)
            {
                return BadRequest();
            }

            _context.Entry(bike).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BikeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Bikes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Bike>> PostBike(Bike bike)
        {
          if (_context.Bike == null)
          {
              return Problem("Entity set 'Exercise3Context.Bike'  is null.");
          }
            _context.Bike.Add(bike);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBike", new { id = bike.Id }, bike);
        }

        // DELETE: api/Bikes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBike(int id)
        {
            if (_context.Bike == null)
            {
                return NotFound();
            }
            var bike = await _context.Bike.FindAsync(id);
            if (bike == null)
            {
                return NotFound();
            }

            _context.Bike.Remove(bike);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BikeExists(int id)
        {
            return (_context.Bike?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
