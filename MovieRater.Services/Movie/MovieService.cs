using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MovieRater.Data;
using MovieRater.Data.Entities;
using MovieRater.Models;
using MovieRater.Models.Movie;

namespace MovieRater.Services.Movie
{
    public class MovieService : IMovieService
    {
        private readonly ApplicationDbContext _dbContext;
        public MovieService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

                public async Task<bool> CreateMovieAsync(MovieCreate request)
        {
            var movieEntity = new MovieEntity
            {
                Title = request.Title,
                Description = request.Description,
                Genre = request.Genre,
                CreatedUtc = DateTimeOffset.Now,
                OwnerId = 1
            };

            _dbContext.Movies.Add(movieEntity);

            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<IEnumerable<MovieListItem>> GetAllMoviesAsync()
        {
            var movies = await _dbContext.Movies
                .Where(entity => entity.OwnerId == 1)
                .Select(entity => new MovieListItem
                {
                    Id = entity.Id,
                    Title = entity.Title,
                    Genre = entity.Genre,
                    CreatedUtc = entity.CreatedUtc
                })
                .ToListAsync();
            
            return movies;
        }

        public async Task <MovieDetail> GetMovieByIdAsync(int MovieId)
        {
            var MovieEntity = await _dbContext.Movies
                .FirstOrDefaultAsync(e =>
                    e.Id == MovieId && e.OwnerId == 1
                );
            return MovieEntity is null ? null : new MovieDetail
            {
                Id = MovieEntity.Id,
                Title = MovieEntity.Title,
                Description = MovieEntity.Description,
                Genre = MovieEntity.Genre,
                CreatedUtc = MovieEntity.CreatedUtc,
                ModifiedUtc = MovieEntity.ModifiedUtc
            };
        }

        public async Task<bool> UpdateMovieAsync(MovieUpdate request)
        {
            var MovieEntity = await _dbContext.Movies.FindAsync(request.Id);

            if (MovieEntity?.OwnerId != 1)
                return false;
            
            MovieEntity.Title = request.Title;
            MovieEntity.Description = request.Description;
            MovieEntity.ModifiedUtc = DateTimeOffset.Now;

            var numberOfChanges = await _dbContext.SaveChangesAsync();

            return numberOfChanges == 1;
        }

        public async Task<bool> DeleteMovieAsync(int MovieId)
        {
            var MovieEntity = await _dbContext.Movies.FindAsync(MovieId);

            if (MovieEntity?.OwnerId != 1)
                return false;

            _dbContext.Movies.Remove(MovieEntity);
            return await _dbContext.SaveChangesAsync() == 1;
        }
    }
}