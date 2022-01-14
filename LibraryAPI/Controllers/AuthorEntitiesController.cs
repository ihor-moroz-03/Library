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
    [Route("library/authors")]
    [ApiController]
    public class AuthorEntitiesController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        public AuthorEntitiesController(LibraryDbContext context)
        {
            _context = context;
        }

        // GET: api/AuthorEntities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorEntity>>> GetAuthors()
        {
            return await _context.Authors.ToListAsync();
        }

        // GET: api/AuthorEntities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorEntity>> GetAuthorEntity(long id)
        {
            var authorEntity = await _context.Authors.FindAsync(id);

            if (authorEntity == null)
            {
                return NotFound();
            }

            return authorEntity;
        }

        // PUT: api/AuthorEntities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthorEntity(long id, AuthorEntity authorEntity)
        {
            if (id != authorEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(authorEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorEntityExists(id))
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

        // POST: api/AuthorEntities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AuthorEntity>> PostAuthorEntity(Author author)
        {
            AuthorEntity authorEntity = author.ToAuthorEntity();
            _context.Authors.Add(authorEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuthorEntity", new { id = authorEntity.Id }, authorEntity);
        }

        // DELETE: api/AuthorEntities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthorEntity(long id)
        {
            var authorEntity = await _context.Authors.FindAsync(id);
            if (authorEntity == null)
            {
                return NotFound();
            }

            _context.Authors.Remove(authorEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AuthorEntityExists(long id)
        {
            return _context.Authors.Any(e => e.Id == id);
        }
    }
}
