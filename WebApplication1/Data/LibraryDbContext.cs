using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }

        public DbSet<Book> Books => Set<Book>();
        public DbSet<Author> Authors => Set<Author>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // PK identity for Book
            modelBuilder.Entity<Book>()
                        .HasKey(b => b.Id);
            modelBuilder.Entity<Book>()
                        .Property(b => b.Id)
                        .ValueGeneratedOnAdd();

            // Book → Author (many-to-one, optional)
            modelBuilder.Entity<Book>()
                        .HasOne(b => b.AuthorRef)
                        .WithMany(a => a.Books)
                        .HasForeignKey(b => b.AuthorId)
                        .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
