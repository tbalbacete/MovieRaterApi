using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieRater.Data;
using MovieRater.Data.Entities;
using MovieRater.Models.Rating;

namespace MovieRater.Services.Rating
{
    public class RatingService: IRatingService
    {
        private readonly ApplicationDbContext _dbContext;

        public RatingService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateRatingAsync(RatingCreate request)
        {
            var ratingEntity = new RatingEntity
            {
                Rating = request.Rating,
                CreatedUtc = DateTimeOffset.Now,
                MovieId = request.MovieId,
                ShowId = request.ShowId
            };

            _dbContext.Ratings.Add(ratingEntity);

            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }
    }
}