using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Member
    {
        public int Id { get; set; }

        [Required, MaxLength(120)]
        public string Name { get; set; } = null!;

        [Required, EmailAddress, MaxLength(200)]
        public string Email { get; set; } = null!;
    }
}
