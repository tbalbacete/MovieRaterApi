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
            };

            if (request.MovieId > 0)
            {
                ratingEntity.MovieId = request.MovieId;
            }

            if (request.ShowId > 0)
            {
                ratingEntity.ShowId = request.ShowId;
            }

            _dbContext.Ratings.Add(ratingEntity);

            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;

        }
    }
}