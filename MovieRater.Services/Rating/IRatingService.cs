using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieRater.Models.Rating;
using MovieRater.Data.Entities;

namespace MovieRater.Services.Rating
{
    public interface IRatingService
    {
        Task<bool> CreateRatingAsync(RatingCreate request);
        Task<IEnumerable<RatingListItem>> GetAllRatingsAsync();
        Task<RatingDetail> GetRatingByMovieAsync(int movieId);
        Task<bool> DeleteRatingAsync(int ratingId);
    }
}