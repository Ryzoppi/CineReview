using System.Collections.Generic;
using System.Threading.Tasks;
using CineReview.DTOs;

namespace CineReview.Services.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewReadDto>> GetAllAsync();
        Task<ReviewReadDto> GetByIdAsync(int id);
        Task<ReviewReadDto> CreateAsync(ReviewCreateDto dto);
        Task<ReviewReadDto> UpdateAsync(int id, ReviewCreateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
