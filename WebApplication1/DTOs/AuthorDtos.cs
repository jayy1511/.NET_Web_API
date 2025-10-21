using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs
{
    public record AuthorDto(int Id, string Name);

    public class CreateAuthorDto
    {
        [Required, MaxLength(120)]
        public string Name { get; set; } = null!;
    }
}
