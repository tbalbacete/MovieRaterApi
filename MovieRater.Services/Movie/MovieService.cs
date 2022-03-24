using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MovieRater.Data;

namespace MovieRater.Services.Movie
{
    public class MovieService
    {
        private readonly int _movieId;
        private readonly ApplicationDbContext _dbContext;
        public MovieService(IHttpContextAccessor httpContextAccessor, ApplicationDbContext dbContext)
        {
            var movieClaims = httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var value = movieClaims.FindFirst("Id")?.Value;
            var validId = int.TryParse(value, out _movieId);
            if (!validId)
            throw new Exception("Attempted to build MovieService without Movie Id claim.");

            _dbContext = dbContext;
        }
    }
}