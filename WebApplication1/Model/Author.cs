using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required, MaxLength(120)]
        public string Name { get; set; } = null!;

        // 1-to-many: Author → Books
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
