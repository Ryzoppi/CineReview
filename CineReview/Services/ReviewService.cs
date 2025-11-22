using AutoMapper;
using CineReview.DTOs;
using CineReview.Models;
using CineReview.Repositories.Interfaces;
using CineReview.Services.Interfaces;

namespace CineReview.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _repo;
        private readonly IMapper _mapper;

        public ReviewService(IReviewRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReviewReadDto>> GetAllAsync()
        {
            var reviews = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<ReviewReadDto>>(reviews);
        }

        public async Task<ReviewReadDto> GetByIdAsync(int id)
        {
            var review = await _repo.GetByIdAsync(id);
            if (review == null) return null;
            return _mapper.Map<ReviewReadDto>(review);
        }

        public async Task<ReviewReadDto> CreateAsync(ReviewCreateDto dto)
        {
            var review = _mapper.Map<Review>(dto);
            await _repo.AddAsync(review);
            if (!await _repo.SaveChangesAsync()) return null;
            return _mapper.Map<ReviewReadDto>(review);
        }

        public async Task<ReviewReadDto> UpdateAsync(int id, ReviewCreateDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return null;

            _mapper.Map(dto, existing);
            _repo.Update(existing);
            if (!await _repo.SaveChangesAsync()) return null;
            return _mapper.Map<ReviewReadDto>(existing);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;
            _repo.Remove(existing);
            return await _repo.SaveChangesAsync();
        }
    }
}
