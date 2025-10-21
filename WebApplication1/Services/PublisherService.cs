using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly LibraryDbContext _db;
        public PublisherService(LibraryDbContext db) => _db = db;

        public Task<List<Publisher>> GetAllAsync() => _db.Publishers.AsNoTracking().ToListAsync();
        public Task<Publisher?> GetByIdAsync(int id) => _db.Publishers.FindAsync(id).AsTask();

        public async Task<Publisher> CreateAsync(Publisher entity)
        {
            _db.Publishers.Add(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> UpdateAsync(Publisher entity)
        {
            if (!await _db.Publishers.AnyAsync(x => x.Id == entity.Id)) return false;
            _db.Entry(entity).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var e = await _db.Publishers.FindAsync(id);
            if (e is null) return false;
            _db.Publishers.Remove(e);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
