using System.ComponentModel.DataAnnotations;
using WebApplication1.Attributes;

namespace WebApplication1.DTOs
{
    // What you RETURN to clients
    public record BookDto(
        int Id,
        string Title,
        int Year,
        int? AuthorId,
        string? AuthorName
    );

    // What clients SEND when creating
    public class CreateBookDto
    {
        [Required, MaxLength(200)]
        public string Title { get; set; } = null!;

        [PublishedYear]
        public int Year { get; set; }

        public int? AuthorId { get; set; }
    }

    // What clients SEND when updating
    public class UpdateBookDto : CreateBookDto
    {
        [Required]
        public int Id { get; set; }
    }
}
