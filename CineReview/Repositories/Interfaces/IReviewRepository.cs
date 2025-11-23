using System.Collections.Generic;
using System.Threading.Tasks;
using CineReview.Models;

namespace CineReview.Repositories.Interfaces
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetAllAsync();
        Task<Review> GetByIdAsync(int id);
        Task AddAsync(Review review);
        void Update(Review review);
        void Remove(Review review);
        Task<bool> SaveChangesAsync();
        Task<IEnumerable<Review>> FilterReviewsAsync(int? grade, int? userId, int? mediaId, string? orderBy);
    }
}