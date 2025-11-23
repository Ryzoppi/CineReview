using System.Collections.Generic;
using System.Threading.Tasks;
using CineReview.DTOs;

namespace CineReview.Services.Interfaces
{
    public interface ISerieService
    {
        Task<IEnumerable<SerieReadDto>> GetAllAsync();
        Task<SerieReadDto> GetByIdAsync(int id);
        Task<SerieReadDto> CreateAsync(SerieCreateDto dto);
        Task<SerieReadDto> UpdateAsync(int id, SerieCreateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<SerieReadDto>> FilterSeriesAsync(string? name, string? synopsis, string? director, int? releaseYear, int? seasons, int? episodes, string? orderBy);
    }
}
