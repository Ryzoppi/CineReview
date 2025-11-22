using System.Collections.Generic;
using System.Threading.Tasks;
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
    }
}

