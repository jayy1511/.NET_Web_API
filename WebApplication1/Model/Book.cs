using System.ComponentModel.DataAnnotations;
using WebApplication1.Attributes;

namespace WebApplication1.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; } = null!;

        [PublishedYear]
        public int Year { get; set; }

        public int? AuthorId { get; set; }
        public Author? AuthorRef { get; set; }

        public int? GenreId { get; set; }
        public Genre? Genre { get; set; }

        public int? PublisherId { get; set; }
        public Publisher? Publisher { get; set; }
    }
}
