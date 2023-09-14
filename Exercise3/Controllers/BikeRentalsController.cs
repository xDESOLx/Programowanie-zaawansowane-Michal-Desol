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
    public class BikeRentalsController : ControllerBase
    {
        private readonly Exercise3Context _context;

        public BikeRentalsController(Exercise3Context context)
        {
            _context = context;
        }

        // GET: api/BikeRentals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BikeRental>>> GetBikeRental()
        {
          if (_context.BikeRental == null)
          {
              return NotFound();
          }
            return await _context.BikeRental.ToListAsync();
        }

        // GET: api/BikeRentals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BikeRental>> GetBikeRental(int id)
        {
          if (_context.BikeRental == null)
          {
              return NotFound();
          }
            var bikeRental = await _context.BikeRental.FindAsync(id);

            if (bikeRental == null)
            {
                return NotFound();
            }

            return bikeRental;
        }

        // PUT: api/BikeRentals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBikeRental(int id, BikeRental bikeRental)
        {
            if (id != bikeRental.Id)
            {
                return BadRequest();
            }

            _context.Entry(bikeRental).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BikeRentalExists(id))
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

        // POST: api/BikeRentals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BikeRental>> PostBikeRental(BikeRental bikeRental)
        {
          if (_context.BikeRental == null)
          {
              return Problem("Entity set 'Exercise3Context.BikeRental'  is null.");
          }
            _context.BikeRental.Add(bikeRental);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBikeRental", new { id = bikeRental.Id }, bikeRental);
        }

        // DELETE: api/BikeRentals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBikeRental(int id)
        {
            if (_context.BikeRental == null)
            {
                return NotFound();
            }
            var bikeRental = await _context.BikeRental.FindAsync(id);
            if (bikeRental == null)
            {
                return NotFound();
            }

            _context.BikeRental.Remove(bikeRental);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BikeRentalExists(int id)
        {
            return (_context.BikeRental?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
