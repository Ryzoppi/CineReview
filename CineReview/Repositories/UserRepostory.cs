using CineReview.Data;
using CineReview.Models;
using CineReview.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CineReview.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataBaseContext _ctx;
        public UserRepository(DataBaseContext ctx) => _ctx = ctx;

        public async Task<IEnumerable<User>> GetAllAsync()
            => await _ctx.Users.ToListAsync();

        public async Task<User> GetByIdAsync(int id)
            => await _ctx.Users.FindAsync(id);

        public async Task AddAsync(User user) => await _ctx.Users.AddAsync(user);

        public void Update(User user) => _ctx.Users.Update(user);

        public void Remove(User user) => _ctx.Users.Remove(user);

        public async Task<bool> SaveChangesAsync() => (await _ctx.SaveChangesAsync()) > 0;

        public async Task<IEnumerable<Media>> GetAllFavoritesAsync(int userId)
        {
            var user = await _ctx.Users
                .Include(u => u.FavoriteList)
                .FirstOrDefaultAsync(u => u.Id == userId);

            return user?.FavoriteList;
        }

        public async Task<Media> AddToFavoritesAsync(int id, int mediaId)
        {
            var user = await _ctx.Users
                .Include(u => u.FavoriteList)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null) return null;

            var media = await _ctx.Media.FindAsync(mediaId);

            user.FavoriteList.Add(media);
            await _ctx.SaveChangesAsync();

            return media;
        }

        public async Task<Media> RemoveFromFavoritesAsync(int id, int mediaId)
        {
            var user = await _ctx.Users
                .Include(u => u.FavoriteList)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null) return null;

            var media = user.FavoriteList.FirstOrDefault(u => u.Id == mediaId);

            if (media != null)
            {
                user.FavoriteList.Remove(media);
                await _ctx.SaveChangesAsync();
            }

            return media;
        }

        public async Task<IEnumerable<User>> FilterUsersAsync(string? name, string? email, string? orderBy)
        {
            IQueryable<User> query = _ctx.Users;

            if (!string.IsNullOrEmpty(name))
                query = query.Where(u => u.Name.Contains(name));

            if (!string.IsNullOrEmpty(email))
                query = query.Where(u => u.Email.Contains(email));

            query = orderBy?.ToLower() switch
            {
                "name" => query.OrderBy(u => u.Name),
                "name_desc" => query.OrderByDescending(u => u.Name),

                _ => query.OrderBy(u => u.Id)
            };

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Media>> FilterFavoritesAsync(int id, string? name, string? synopsis, string? director, int? releaseYear, string? orderBy)
        {
            IQueryable<Media> query = _ctx.Users.Where(u => u.Id == id).SelectMany(u => u.FavoriteList).AsQueryable();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(m => m.Name.Contains(name));

            if (!string.IsNullOrEmpty(synopsis))
                query = query.Where(m => m.Synopsis.Contains(synopsis));

            if (!string.IsNullOrEmpty(director))
                query = query.Where(m => m.Director.Contains(director));

            if (releaseYear.HasValue)
                query = query.Where(m => m.ReleaseYear == releaseYear);

            query = orderBy?.ToLower() switch
            {
                "name" => query.OrderBy(m => m.Name),
                "name_desc" => query.OrderByDescending(m => m.Name),

                "synopsis" => query.OrderBy(m => m.Synopsis),
                "synopsis_desc" => query.OrderByDescending(m => m.Synopsis),

                "director" => query.OrderBy(m => m.Director),
                "director_desc" => query.OrderByDescending(m => m.Director),

                "release_year" => query.OrderBy(m => m.ReleaseYear),
                "release_year_desc" => query.OrderByDescending(m => m.ReleaseYear),

                _ => query.OrderBy(u => u.Id)
            };

            return await query.ToListAsync();
        }
    }
}
