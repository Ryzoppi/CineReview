using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CineReview.Data;
using CineReview.Models;
using CineReview.Repositories.Interfaces;

namespace CineReview.Repositories
{
    public class SerieRepository : ISerieRepository
    {
        private readonly DataBaseContext _ctx;
        public SerieRepository(DataBaseContext ctx) => _ctx = ctx;

        public async Task<IEnumerable<Serie>> GetAllAsync()
            => await _ctx.Series.ToListAsync();

        public async Task<Serie> GetByIdAsync(int id)
            => await _ctx.Series.FindAsync(id);

        public async Task AddAsync(Serie serie) => await _ctx.Series.AddAsync(serie);

        public void Update(Serie serie) => _ctx.Series.Update(serie);

        public void Remove(Serie serie) => _ctx.Series.Remove(serie);

        public async Task<bool> SaveChangesAsync() => (await _ctx.SaveChangesAsync()) > 0;

        public async Task<IEnumerable<Serie>> FilterSeriesAsync(string? name, string? synopsis, string? director, int? releaseYear, int? seasons, int? episodes, string? orderBy)
        {
            IQueryable<Serie> query = _ctx.Series;

            if (!string.IsNullOrEmpty(name))
                query = query.Where(s => s.Name.Contains(name));

            if (!string.IsNullOrEmpty(synopsis))
                query = query.Where(s => s.Synopsis.Contains(synopsis));

            if (!string.IsNullOrEmpty(director))
                query = query.Where(s => s.Director.Contains(director));

            if (releaseYear.HasValue)
                query = query.Where(s => s.ReleaseYear == releaseYear);

            if (seasons.HasValue)
                query = query.Where(s => s.Seasons == seasons);

            if (episodes.HasValue)
                query = query.Where(s => s.Episodes == episodes);

            query = orderBy?.ToLower() switch
            {
                "name" => query.OrderBy(s => s.Name),
                "name_desc" => query.OrderByDescending(s => s.Name),

                "synopsis" => query.OrderBy(s => s.Synopsis),
                "synopsis_desc" => query.OrderByDescending(s => s.Synopsis),

                "director" => query.OrderBy(s => s.Director),
                "director_desc" => query.OrderByDescending(s => s.Director),

                "release_year" => query.OrderBy(s => s.ReleaseYear),
                "release_year_desc" => query.OrderByDescending(s => s.ReleaseYear),

                "seasons" => query.OrderBy(s => s.Seasons),
                "seasons_desc" => query.OrderByDescending(s => s.Seasons),

                "episodes" => query.OrderBy(s => s.Episodes),
                "episodes_desc" => query.OrderByDescending(s => s.Episodes),

                _ => query.OrderBy(s => s.Id)
            };

            return await query.ToListAsync();
        }
    }
}

