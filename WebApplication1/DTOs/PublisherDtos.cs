using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs
{
    public record PublisherDto(int Id, string Name);

    public class CreatePublisherDto
    {
        [Required, MaxLength(120)]
        public string Name { get; set; } = null!;
    }

    public class UpdatePublisherDto : CreatePublisherDto
    {
        [Required] public int Id { get; set; }
    }
}
