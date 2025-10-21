using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DTOs;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MembersController : ControllerBase
    {
        private readonly IMemberService _svc;
        public MembersController(IMemberService svc) => _svc = svc;

        private static MemberDto ToDto(Member m) => new(m.Id, m.Name, m.Email);

        // GET: /api/members
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetAll()
        {
            var items = await _svc.GetAllAsync();
            return Ok(items.Select(ToDto));
        }

        // GET: /api/members/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<MemberDto>> Get(int id)
        {
            var e = await _svc.GetByIdAsync(id);
            return e is null ? NotFound() : Ok(ToDto(e));
        }

        // POST: /api/members
        [HttpPost]
        public async Task<ActionResult<MemberDto>> Create([FromBody] CreateMemberDto input)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var entity = new Member { Name = input.Name, Email = input.Email };
            var created = await _svc.CreateAsync(entity);

            return CreatedAtAction(nameof(Get), new { id = created.Id }, ToDto(created));
        }

        // PUT: /api/members/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMemberDto input)
        {
            if (id != input.Id) return BadRequest("Id mismatch.");
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var ok = await _svc.UpdateAsync(new Member { Id = input.Id, Name = input.Name, Email = input.Email });
            return ok ? NoContent() : NotFound();
        }

        // DELETE: /api/members/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _svc.DeleteAsync(id);
            return ok ? NoContent() : NotFound();
        }
    }
}
