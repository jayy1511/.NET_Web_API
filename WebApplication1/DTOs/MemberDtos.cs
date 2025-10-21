using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs
{
    public record MemberDto(int Id, string Name, string Email);

    public class CreateMemberDto
    {
        [Required, MaxLength(120)] public string Name { get; set; } = null!;
        [Required, EmailAddress, MaxLength(200)] public string Email { get; set; } = null!;
    }

    public class UpdateMemberDto : CreateMemberDto
    {
        [Required] public int Id { get; set; }
    }
}
