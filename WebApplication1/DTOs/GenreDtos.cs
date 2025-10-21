using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs
{
    public record GenreDto(int Id, string Name);

    public class CreateGenreDto
    {
        [Required, MaxLength(80)]
        public string Name { get; set; } = null!;
    }

    public class UpdateGenreDto : CreateGenreDto
    {
        [Required] public int Id { get; set; }
    }
}
