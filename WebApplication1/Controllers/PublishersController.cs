using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DTOs;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublishersController : ControllerBase
    {
        private readonly IPublisherService _svc;
        public PublishersController(IPublisherService svc) => _svc = svc;

        private static PublisherDto ToDto(Publisher p) => new(p.Id, p.Name);

        // GET: /api/publishers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublisherDto>>> GetAll()
        {
            var items = await _svc.GetAllAsync();
            return Ok(items.Select(ToDto));
        }

        // GET: /api/publishers/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PublisherDto>> Get(int id)
        {
            var e = await _svc.GetByIdAsync(id);
            return e is null ? NotFound() : Ok(ToDto(e));
        }

        // POST: /api/publishers
        [HttpPost]
        public async Task<ActionResult<PublisherDto>> Create([FromBody] CreatePublisherDto input)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var entity = new Publisher { Name = input.Name };
            var created = await _svc.CreateAsync(entity);

            return CreatedAtAction(nameof(Get), new { id = created.Id }, ToDto(created));
        }

        // PUT: /api/publishers/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePublisherDto input)
        {
            if (id != input.Id) return BadRequest("Id mismatch.");
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var ok = await _svc.UpdateAsync(new Publisher { Id = input.Id, Name = input.Name });
            return ok ? NoContent() : NotFound();
        }

        // DELETE: /api/publishers/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _svc.DeleteAsync(id);
            return ok ? NoContent() : NotFound();
        }
    }
}
