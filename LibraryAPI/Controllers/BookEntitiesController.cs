using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryDAL;

namespace LibraryAPI.Controllers
{
    [Route("library/books")]
    [ApiController]
    public class BookEntitiesController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        public BookEntitiesController(LibraryDbContext context)
        {
            _context = context;
        }

        // GET: api/BookEntities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookEntity>>> GetBooks()
        {
            return await _context.Books.ToListAsync();
        }

        // GET: api/BookEntities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookEntity>> GetBookEntity(long id)
        {
            var bookEntity = await _context.Books.FindAsync(id);

            if (bookEntity == null)
            {
                return NotFound();
            }

            return bookEntity;
        }

        // PUT: api/BookEntities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookEntity(long id, BookEntity bookEntity)
        {
            if (id != bookEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(bookEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookEntityExists(id))
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

        // POST: api/BookEntities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BookEntity>> PostBookEntity(Book book)
        {
            BookEntity bookEntity = book.ToBookEntity(_context);
            _context.Books.Add(bookEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBookEntity", new { id = bookEntity.Id }, bookEntity);
        }

        // DELETE: api/BookEntities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookEntity(long id)
        {
            var bookEntity = await _context.Books.FindAsync(id);
            if (bookEntity == null)
            {
                return NotFound();
            }

            _context.Books.Remove(bookEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookEntityExists(long id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
