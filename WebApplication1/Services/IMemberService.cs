using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IMemberService
    {
        Task<List<Member>> GetAllAsync();
        Task<Member?> GetByIdAsync(int id);
        Task<Member> CreateAsync(Member entity);
        Task<bool> UpdateAsync(Member entity);
        Task<bool> DeleteAsync(int id);
    }
}
