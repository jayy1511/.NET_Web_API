using System.ComponentModel.DataAnnotations;
using WebApplication1.Attributes;

namespace WebApplication1.Models
{
    public class Book
    {
        [Key]                           // Attribute: primary key
        public int Id { get; set; }

        [Required, MaxLength(200)]      // Attributes: validation
        public string Title { get; set; } = null!;

        [PublishedYear]                 // Custom attribute
        public int Year { get; set; }

        // RELATIONSHIP: many Books → one Author (optional)
        public int? AuthorId { get; set; }
        public Author? AuthorRef { get; set; }
    }
}
