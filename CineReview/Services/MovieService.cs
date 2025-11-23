using AutoMapper;
using CineReview.DTOs;
using CineReview.Models;
using CineReview.Repositories.Interfaces;
using CineReview.Services.Interfaces;

namespace CineReview.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _repo;
        private readonly IMapper _mapper;

        public MovieService(IMovieRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MovieReadDto>> GetAllAsync()
        {
            var movies = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<MovieReadDto>>(movies);
        }

        public async Task<MovieReadDto> GetByIdAsync(int id)
        {
            var movie = await _repo.GetByIdAsync(id);
            if (movie == null) return null;
            return _mapper.Map<MovieReadDto>(movie);
        }

        public async Task<MovieReadDto> CreateAsync(MovieCreateDto dto)
        {
            var movie = _mapper.Map<Movie>(dto);
            await _repo.AddAsync(movie);
            if (!await _repo.SaveChangesAsync()) return null;
            return _mapper.Map<MovieReadDto>(movie);
        }

        public async Task<MovieReadDto> UpdateAsync(int id, MovieCreateDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return null;

            _mapper.Map(dto, existing);
            _repo.Update(existing);
            if (!await _repo.SaveChangesAsync()) return null;
            return _mapper.Map<MovieReadDto>(existing);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;
            _repo.Remove(existing);
            return await _repo.SaveChangesAsync();
        }

        public async Task<IEnumerable<MovieReadDto>> FilterMoviesAsync(string? name, string? synopsis, string? director, int? releaseYear, int? duration, string? orderBy)
        {
            var movies = await _repo.FilterMoviesAsync(name, synopsis, director, releaseYear, duration, orderBy);
            if (movies == null) return null;

            return _mapper.Map<IEnumerable<MovieReadDto>>(movies);
        }
    }
}
