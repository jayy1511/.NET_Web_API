using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IPublisherService
    {
        Task<List<Publisher>> GetAllAsync();
        Task<Publisher?> GetByIdAsync(int id);
        Task<Publisher> CreateAsync(Publisher entity);
        Task<bool> UpdateAsync(Publisher entity);
        Task<bool> DeleteAsync(int id);
    }
}
