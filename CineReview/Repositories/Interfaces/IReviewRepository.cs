using System.Collections.Generic;
using System.Threading.Tasks;
using CineReview.Models;

namespace CineReview.Repositories.Interfaces
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetAllByUserAsync(int userId);
        Task<Review> GetByIdAsync(int id);
        Task AddAsync(Review review);
        void Update(Review review);
        void Remove(Review review);
        Task<bool> SaveChangesAsync();
    }
}