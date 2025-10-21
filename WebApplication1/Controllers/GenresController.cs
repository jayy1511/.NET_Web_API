using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _svc;
        public GenresController(IGenreService svc) => _svc = svc;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenreDto>>> GetAll() =>
            Ok((await _svc.GetAllAsync()).Select(g => new GenreDto(g.Id, g.Name)));

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GenreDto>> Get(int id)
        {
            var e = await _svc.GetByIdAsync(id);
            return e is null ? NotFound() : Ok(new GenreDto(e.Id, e.Name));
        }

        [HttpPost]
        public async Task<ActionResult<GenreDto>> Create(CreateGenreDto dto)
        {
            var e = await _svc.CreateAsync(new Genre { Name = dto.Name });
            return CreatedAtAction(nameof(Get), new { id = e.Id }, new GenreDto(e.Id, e.Name));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateGenreDto dto)
        {
            if (id != dto.Id) return BadRequest("Id mismatch.");
            var ok = await _svc.UpdateAsync(new Genre { Id = dto.Id, Name = dto.Name });
            return ok ? NoContent() : NotFound();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id) =>
            await _svc.DeleteAsync(id) ? NoContent() : NotFound();
    }
}
