using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Genre
    {
        public int Id { get; set; }

        [Required, MaxLength(80)]
        public string Name { get; set; } = null!;

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
