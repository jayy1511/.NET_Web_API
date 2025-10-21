using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Publisher
    {
        public int Id { get; set; }

        [Required, MaxLength(120)]
        public string Name { get; set; } = null!;

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
