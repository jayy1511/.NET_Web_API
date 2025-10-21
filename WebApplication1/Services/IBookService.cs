using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IBookService
    {
        Task<List<Book>> GetAllAsync();
        Task<Book?> GetByIdAsync(int id);
        Task<Book> CreateAsync(Book book);
        Task<bool> UpdateAsync(Book book);
        Task<bool> DeleteAsync(int id);

        // ORM queries (examples your prof wants)
        Task<List<Book>> SearchByTitleAsync(string query);                // WHERE + projection
        Task<List<Book>> GetRecentAsync(int take = 5);                    // ORDER BY + TAKE
        Task<List<Book>> GetByAuthorNameAsync(string authorName);         // JOIN/Include + WHERE
        Task<Dictionary<string, int>> CountByAuthorAsync();                // GROUP BY
    }
}
