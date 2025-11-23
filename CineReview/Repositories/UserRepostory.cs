using Microsoft.EntityFrameworkCore;
using CineReview.Data;
using CineReview.Models;
using CineReview.Repositories.Interfaces;

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

            var media = user.FavoriteList.FirstOrDefault(m => m.Id == mediaId);

            if (media != null)
            {
                user.FavoriteList.Remove(media);
                await _ctx.SaveChangesAsync();
            }

            return media;
        }
    }
}

