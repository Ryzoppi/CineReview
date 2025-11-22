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
    }
}
