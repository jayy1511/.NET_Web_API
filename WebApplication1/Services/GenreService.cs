using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class GenreService : IGenreService
    {
        private readonly LibraryDbContext _db;
        public GenreService(LibraryDbContext db) => _db = db;

        public Task<List<Genre>> GetAllAsync() => _db.Genres.AsNoTracking().ToListAsync();
        public Task<Genre?> GetByIdAsync(int id) => _db.Genres.FindAsync(id).AsTask();

        public async Task<Genre> CreateAsync(Genre entity)
        {
            _db.Genres.Add(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> UpdateAsync(Genre entity)
        {
            if (!await _db.Genres.AnyAsync(x => x.Id == entity.Id)) return false;
            _db.Entry(entity).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var e = await _db.Genres.FindAsync(id);
            if (e is null) return false;
            _db.Genres.Remove(e);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
