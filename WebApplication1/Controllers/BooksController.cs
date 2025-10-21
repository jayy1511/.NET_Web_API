using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly LibraryDbContext _db;
        public BooksController(LibraryDbContext db) => _db = db;

        // mapper helpers
        private static BookDto ToDto(Book b) =>
            new(b.Id, b.Title, b.Year, b.AuthorId, b.AuthorRef?.Name);

        // GET /api/books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetAll()
        {
            var items = await _db.Books.Include(b => b.AuthorRef)
                                       .AsNoTracking()
                                       .ToListAsync();
            return Ok(items.Select(ToDto));
        }

        // GET /api/books/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<BookDto>> Get(int id)
        {
            var item = await _db.Books.Include(b => b.AuthorRef)
                                      .FirstOrDefaultAsync(b => b.Id == id);
            return item is null ? NotFound() : Ok(ToDto(item));
        }

        // POST /api/books
        [HttpPost]
        public async Task<ActionResult<BookDto>> Create([FromBody] CreateBookDto input)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var entity = new Book
            {
                Title = input.Title,
                Year = input.Year,
                AuthorId = input.AuthorId
            };

            _db.Books.Add(entity);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = entity.Id }, ToDto(entity));
        }

        // PUT /api/books/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateBookDto input)
        {
            if (id != input.Id) return BadRequest("Id mismatch.");
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var entity = await _db.Books.FindAsync(id);
            if (entity is null) return NotFound();

            entity.Title = input.Title;
            entity.Year = input.Year;
            entity.AuthorId = input.AuthorId;

            await _db.SaveChangesAsync();
            return NoContent();
        }

        // DELETE /api/books/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _db.Books.FindAsync(id);
            if (entity is null) return NotFound();

            _db.Books.Remove(entity);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        // ===== sample ORM queries with DTO output =====

        // GET /api/books/search?q=code
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<BookDto>>> Search([FromQuery] string q)
        {
            var items = await _db.Books.Include(b => b.AuthorRef)
                                       .Where(b => b.Title.Contains(q))
                                       .OrderBy(b => b.Title)
                                       .AsNoTracking()
                                       .ToListAsync();
            return Ok(items.Select(ToDto));
        }

        // GET /api/books/recent?take=5
        [HttpGet("recent")]
        public async Task<ActionResult<IEnumerable<BookDto>>> Recent([FromQuery] int take = 5)
        {
            var items = await _db.Books.Include(b => b.AuthorRef)
                                       .OrderByDescending(b => b.Id)
                                       .Take(take)
                                       .AsNoTracking()
                                       .ToListAsync();
            return Ok(items.Select(ToDto));
        }
    }
}
