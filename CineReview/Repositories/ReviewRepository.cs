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

        public async Task<IEnumerable<Review>> GetAllAsync()
        => await _ctx.Reviews.Include(r => r.User).Include(r => r.Media).ToListAsync();
        

        public async Task<Review> GetByIdAsync(int id)
            => await _ctx.Reviews.FindAsync(id);

        public async Task AddAsync(Review reviews) => await _ctx.Reviews.AddAsync(reviews);

        public void Update(Review reviews) => _ctx.Reviews.Update(reviews);

        public void Remove(Review reviews) => _ctx.Reviews.Remove(reviews);

        public async Task<bool> SaveChangesAsync() => (await _ctx.SaveChangesAsync()) > 0;

        public async Task<IEnumerable<Review>> FilterReviewsAsync(int? grade, int? userId, int? mediaId, string? orderBy)
        {
            IQueryable<Review> query = _ctx.Reviews;

            if (grade.HasValue)
                query = query.Where(r => r.Grade == grade);

            if (userId.HasValue)
                query = query.Where(r => r.UserId == userId);

            if (mediaId.HasValue)
                query = query.Where(r => r.MediaId == mediaId);

            query = orderBy?.ToLower() switch
            {
                "grade" => query.OrderBy(r => r.Grade),
                "grade_desc" => query.OrderByDescending(r => r.Grade),

                _ => query.OrderBy(r => r.UserId)
            };

            return await query.Include(r => r.User).Include(r => r.Media).ToListAsync();
        }
    }
}


