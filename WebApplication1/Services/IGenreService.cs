using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IGenreService
    {
        Task<List<Genre>> GetAllAsync();
        Task<Genre?> GetByIdAsync(int id);
        Task<Genre> CreateAsync(Genre entity);
        Task<bool> UpdateAsync(Genre entity);
        Task<bool> DeleteAsync(int id);
    }
}
