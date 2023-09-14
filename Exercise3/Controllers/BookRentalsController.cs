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
    public class BookRentalsController : ControllerBase
    {
        private readonly Exercise3Context _context;

        public BookRentalsController(Exercise3Context context)
        {
            _context = context;
        }

        // GET: api/BookRentals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookRental>>> GetBookRental()
        {
          if (_context.BookRental == null)
          {
              return NotFound();
          }
            return await _context.BookRental.ToListAsync();
        }

        // GET: api/BookRentals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookRental>> GetBookRental(int id)
        {
          if (_context.BookRental == null)
          {
              return NotFound();
          }
            var bookRental = await _context.BookRental.FindAsync(id);

            if (bookRental == null)
            {
                return NotFound();
            }

            return bookRental;
        }

        // PUT: api/BookRentals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookRental(int id, BookRental bookRental)
        {
            if (id != bookRental.Id)
            {
                return BadRequest();
            }

            _context.Entry(bookRental).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookRentalExists(id))
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

        // POST: api/BookRentals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BookRental>> PostBookRental(BookRental bookRental)
        {
          if (_context.BookRental == null)
          {
              return Problem("Entity set 'Exercise3Context.BookRental'  is null.");
          }
            _context.BookRental.Add(bookRental);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBookRental", new { id = bookRental.Id }, bookRental);
        }

        // DELETE: api/BookRentals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookRental(int id)
        {
            if (_context.BookRental == null)
            {
                return NotFound();
            }
            var bookRental = await _context.BookRental.FindAsync(id);
            if (bookRental == null)
            {
                return NotFound();
            }

            _context.BookRental.Remove(bookRental);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookRentalExists(int id)
        {
            return (_context.BookRental?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
