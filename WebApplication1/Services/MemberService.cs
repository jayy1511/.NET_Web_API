using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class MemberService : IMemberService
    {
        private readonly LibraryDbContext _db;
        public MemberService(LibraryDbContext db) => _db = db;

        public Task<List<Member>> GetAllAsync() => _db.Members.AsNoTracking().ToListAsync();
        public Task<Member?> GetByIdAsync(int id) => _db.Members.FindAsync(id).AsTask();

        public async Task<Member> CreateAsync(Member entity)
        {
            _db.Members.Add(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> UpdateAsync(Member entity)
        {
            if (!await _db.Members.AnyAsync(x => x.Id == entity.Id)) return false;
            _db.Entry(entity).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var e = await _db.Members.FindAsync(id);
            if (e is null) return false;
            _db.Members.Remove(e);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
