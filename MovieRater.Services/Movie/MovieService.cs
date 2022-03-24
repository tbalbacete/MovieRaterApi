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
        private readonly int _userId;
        private readonly ApplicationDbContext _dbContext;
        public MovieService(IHttpContextAccessor httpContextAccessor, ApplicationDbContext dbContext)
        {
            var movieClaims = httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var value = movieClaims.FindFirst("Id")?.Value;
            var validId = int.TryParse(value, out _userId);
            if (!validId)
            throw new Exception("Attempted to build MovieService without Movie Id claim.");

            _dbContext = dbContext;
        }

                public async Task<bool> CreateMovieAsync(MovieCreate request)
        {
            var movieEntity = new MovieEntity
            {
                Title = request.Title,
                Description = request.Description,
                CreatedUtc = DateTimeOffset.Now,
                OwnerId = _userId
            };

            _dbContext.Movies.Add(movieEntity);

            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<IEnumerable<MovieListItem>> GetAllMoviesAsync()
        {
            var movies = await _dbContext.Movies
                .Where(entity => entity.OwnerId == _userId)
                .Select(entity => new MovieListItem
                {
                    Id = entity.Id,
                    Title = entity.Title,
                    CreatedUtc = entity.CreatedUtc
                })
                .ToListAsync();
            
            return movies;
        }

        public async Task <MovieDetail> GetMovieByIdAsync(int MovieId)
        {
            var MovieEntity = await _dbContext.Movies
                .FirstOrDefaultAsync(e =>
                    e.Id == MovieId && e.OwnerId == _userId
                );
            return MovieEntity is null ? null : new MovieDetail
            {
                Id = MovieEntity.Id,
                Title = MovieEntity.Title,
                Description = MovieEntity.Description,
                CreatedUtc = MovieEntity.CreatedUtc,
                ModifiedUtc = MovieEntity.ModifiedUtc
            };
        }

        public async Task<bool> UpdateMovieAsync(MovieUpdate request)
        {
            var MovieEntity = await _dbContext.Movies.FindAsync(request.Id);

            if (MovieEntity?.OwnerId != _userId)
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

            if (MovieEntity?.OwnerId != _userId)
                return false;

            _dbContext.Movies.Remove(MovieEntity);
            return await _dbContext.SaveChangesAsync() == 1;
        }
    }
}