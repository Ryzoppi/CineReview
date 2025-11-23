using CineReview.Data;
using CineReview.Models;
using CineReview.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CineReview.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataBaseContext _ctx;
        public ReviewRepository(DataBaseContext ctx) => _ctx = ctx;

        public async Task<IEnumerable<Review>> GetAllByUserAsync(int userId)
        => await _ctx.Reviews.Where(r => r.UserId == userId).Include(r => r.User).Include(r => r.Media).ToListAsync();
        

        public async Task<Review> GetByIdAsync(int id)
            => await _ctx.Reviews.FindAsync(id);

        public async Task AddAsync(Review reviews) => await _ctx.Reviews.AddAsync(reviews);

        public void Update(Review reviews) => _ctx.Reviews.Update(reviews);

        public void Remove(Review reviews) => _ctx.Reviews.Remove(reviews);

        public async Task<bool> SaveChangesAsync() => (await _ctx.SaveChangesAsync()) > 0;
    }
}


