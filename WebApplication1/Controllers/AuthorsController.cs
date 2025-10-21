using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly LibraryDbContext _db;
        public AuthorsController(LibraryDbContext db) => _db = db;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAll()
        {
            var items = await _db.Authors.AsNoTracking().ToListAsync();
            return Ok(items.Select(a => new AuthorDto(a.Id, a.Name)));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AuthorDto>> GetById(int id)
        {
            var a = await _db.Authors.FindAsync(id);
            return a is null ? NotFound() : Ok(new AuthorDto(a.Id, a.Name));
        }

        [HttpPost]
        public async Task<ActionResult<AuthorDto>> Create(CreateAuthorDto input)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var a = new Author { Name = input.Name };
            _db.Authors.Add(a);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = a.Id }, new AuthorDto(a.Id, a.Name));
        }
    }
}
