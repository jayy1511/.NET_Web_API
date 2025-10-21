using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryDbContext _db;

        public BookService(LibraryDbContext db) => _db = db;

        public async Task<List<Book>> GetAllAsync() =>
            await _db.Books.Include(b => b.AuthorRef)
                           .AsNoTracking()
                           .ToListAsync();

        public async Task<Book?> GetByIdAsync(int id) =>
            await _db.Books.Include(b => b.AuthorRef)
                           .FirstOrDefaultAsync(b => b.Id == id);

        public async Task<Book> CreateAsync(Book book)
        {
            _db.Books.Add(book);
            await _db.SaveChangesAsync();
            return book;
        }

        public async Task<bool> UpdateAsync(Book book)
        {
            var exists = await _db.Books.AnyAsync(b => b.Id == book.Id);
            if (!exists) return false;

            _db.Entry(book).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var book = await _db.Books.FindAsync(id);
            if (book is null) return false;
            _db.Books.Remove(book);
            await _db.SaveChangesAsync();
            return true;
        }

        // ===== ORM Query examples =====

        public async Task<List<Book>> SearchByTitleAsync(string query) =>
            await _db.Books
                     .Where(b => b.Title.Contains(query))
                     .OrderBy(b => b.Title)
                     .AsNoTracking()
                     .ToListAsync();

        public async Task<List<Book>> GetRecentAsync(int take = 5) =>
            await _db.Books
                     .OrderByDescending(b => b.Id)
                     .Take(take)
                     .AsNoTracking()
                     .ToListAsync();

        public async Task<List<Book>> GetByAuthorNameAsync(string authorName) =>
            await _db.Books
                     .Include(b => b.AuthorRef)
                     .Where(b => b.AuthorRef != null && b.AuthorRef.Name.Contains(authorName))
                     .AsNoTracking()
                     .ToListAsync();

        public async Task<Dictionary<string, int>> CountByAuthorAsync() =>
            await _db.Books
                     .Include(b => b.AuthorRef)
                     .GroupBy(b => b.AuthorRef!.Name)
                     .Select(g => new { Author = g.Key, Count = g.Count() })
                     .ToDictionaryAsync(x => x.Author, x => x.Count);
    }
}
