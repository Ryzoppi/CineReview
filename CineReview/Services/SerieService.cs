using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CineReview.DTOs;
using CineReview.Models;
using CineReview.Repositories.Interfaces;
using CineReview.Services.Interfaces;

namespace CineReview.Services
{
    public class SerieService : ISerieService
    {
        private readonly ISerieRepository _repo;
        private readonly IMapper _mapper;

        public SerieService(ISerieRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SerieReadDto>> GetAllAsync()
        {
            var movies = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<SerieReadDto>>(movies);
        }

        public async Task<SerieReadDto> GetByIdAsync(int id)
        {
            var movie = await _repo.GetByIdAsync(id);
            if (movie == null) return null;
            return _mapper.Map<SerieReadDto>(movie);
        }

        public async Task<SerieReadDto> CreateAsync(SerieCreateDto dto)
        {
            var serie = _mapper.Map<Serie>(dto);
            await _repo.AddAsync(serie);
            if (!await _repo.SaveChangesAsync()) return null;
            return _mapper.Map<SerieReadDto>(serie);
        }

        public async Task<SerieReadDto> UpdateAsync(int id, SerieCreateDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return null;

            _mapper.Map(dto, existing);
            _repo.Update(existing);
            if (!await _repo.SaveChangesAsync()) return null;
            return _mapper.Map<SerieReadDto>(existing);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;
            _repo.Remove(existing);
            return await _repo.SaveChangesAsync();
        }

        public async Task<IEnumerable<SerieReadDto>> FilterSeriesAsync(string? name, string? synopsis, string? director, int? releaseYear, int? seasons, int? episodes, string? orderBy)
        {
            var series = await _repo.FilterSeriesAsync(name, synopsis, director, releaseYear, seasons, episodes, orderBy);
            if (series == null) return null;

            return _mapper.Map<IEnumerable<SerieReadDto>>(series);
        }
    }
}
