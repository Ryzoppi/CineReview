using CineReview.Models;

namespace CineReview.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task AddAsync(User serie);
        void Update(User serie);
        void Remove(User serie);
        Task<bool> SaveChangesAsync();
    }
}
