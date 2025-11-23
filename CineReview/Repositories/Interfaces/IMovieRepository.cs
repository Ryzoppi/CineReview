using System.Collections.Generic;
using System.Threading.Tasks;
using CineReview.Models;

namespace CineReview.Repositories.Interfaces
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetAllAsync();
        Task<Movie> GetByIdAsync(int id);
        Task AddAsync(Movie movie);
        void Update(Movie movie);
        void Remove(Movie movie);
        Task<bool> SaveChangesAsync();
        Task<IEnumerable<Movie>> FilterMoviesAsync(string? name, string? synopsis, string? director, int? releaseYear, int? duration, string? orderBy);
    }
}
