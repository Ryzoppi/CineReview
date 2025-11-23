using System.Collections.Generic;
using System.Threading.Tasks;
using CineReview.DTOs;

namespace CineReview.Services.Interfaces
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieReadDto>> GetAllAsync();
        Task<MovieReadDto> GetByIdAsync(int id);
        Task<MovieReadDto> CreateAsync(MovieCreateDto dto);
        Task<MovieReadDto> UpdateAsync(int id, MovieCreateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<MovieReadDto>> FilterMoviesAsync(string? name, string? synopsis, string? director, int? releaseYear, int? duration, string? orderBy);
    }
}
