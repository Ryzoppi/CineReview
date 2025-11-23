using CineReview.Data;
using CineReview.Models;
using CineReview.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CineReview.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly DataBaseContext _ctx;
        public MovieRepository(DataBaseContext ctx) => _ctx = ctx;

        public async Task<IEnumerable<Movie>> GetAllAsync()
            => await _ctx.Movies.ToListAsync();

        public async Task<Movie> GetByIdAsync(int id)
            => await _ctx.Movies.FindAsync(id);

        public async Task AddAsync(Movie movie) => await _ctx.Movies.AddAsync(movie);

        public void Update(Movie movie) => _ctx.Movies.Update(movie);

        public void Remove(Movie movie) => _ctx.Movies.Remove(movie);

        public async Task<bool> SaveChangesAsync() => (await _ctx.SaveChangesAsync()) > 0;

        public async Task<IEnumerable<Movie>> FilterMoviesAsync(string? name, string? synopsis, string? director, int? releaseYear, int? duration, string? orderBy)
        {
            IQueryable<Movie> query = _ctx.Movies;

            if (!string.IsNullOrEmpty(name))
                query = query.Where(m => m.Name.Contains(name));

            if (!string.IsNullOrEmpty(synopsis))
                query = query.Where(m => m.Synopsis.Contains(synopsis));

            if (!string.IsNullOrEmpty(director))
                query = query.Where(m => m.Director.Contains(director));

            if (releaseYear.HasValue)
                query = query.Where(m => m.ReleaseYear == releaseYear);

            if (duration.HasValue)
                query = query.Where(m => m.Duration == duration);

            query = orderBy?.ToLower() switch
            {
                "name" => query.OrderBy(m => m.Name),
                "name_desc" => query.OrderByDescending(m => m.Name),

                "synopsis" => query.OrderBy(m => m.Synopsis),
                "synopsis_desc" => query.OrderByDescending(m => m.Synopsis),

                "director" => query.OrderBy(m => m.Director),
                "director_desc" => query.OrderByDescending(m => m.Director),

                "release_year" => query.OrderBy(m => m.ReleaseYear),
                "release_year_desc" => query.OrderByDescending(m => m.ReleaseYear),

                "duration" => query.OrderBy(m => m.Duration),
                "duration_desc" => query.OrderByDescending(m => m.Duration),

                _ => query.OrderBy(m => m.Id)
            };

            return await query.ToListAsync();
        }
    }
}

