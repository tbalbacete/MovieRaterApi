using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieRater.Models;
using MovieRater.Models.Movie;

namespace MovieRater.Services.Movie
{
    public interface IMovieService
    {
        Task<bool> CreateMovieAsync(MovieCreate request);
        Task<IEnumerable<MovieListItem>> GetAllMoviesAsync();
        Task<MovieDetail> GetMovieByIdAsync(int movieId);
        // Task<MovieDetail> GetMovieByRatingAsync(int rating);
        Task<bool> UpdateMovieAsync(MovieUpdate request);
        Task<bool> DeleteMovieAsync(int movieId);
    }
}