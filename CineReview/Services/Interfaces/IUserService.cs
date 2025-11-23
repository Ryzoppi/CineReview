using CineReview.DTOs;

namespace CineReview.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserReadDto>> GetAllAsync();
        Task<UserReadDto> GetByIdAsync(int id);
        Task<UserReadDto> CreateAsync(UserCreateDto dto);
        Task<UserReadDto> UpdateAsync(int id, UserCreateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<MediaReadDto>> GetAllFavoritesAsync(int id);
        Task<MediaReadDto> AddToFavoritesAsync(int id, int mediaId);
        Task<MediaReadDto> RemoveFromFavoritesAsync(int id, int mediaId);
        Task<IEnumerable<UserReadDto>> FilterUsersAsync(string? name, string? email, string? orderBy);
        Task<IEnumerable<MediaReadDto>> FilterFavoritesAsync(int id, string? name, string? synopsis, string? director, int? releaseYear, string? orderBy);
    }
}
