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
using MovieRater.Models.Show;

namespace MovieRater.Services.Show
{
    public class ShowService : IShowService
    {
        private readonly ApplicationDbContext _dbContext;
        public ShowService(ApplicationDbContext dbContext)
        {
                _dbContext = dbContext;
        }

        public async Task<bool> CreateShowAsync(ShowCreate request)
        {
            var showEntity = new ShowEntity
            {
                Title = request.Title,
                Description = request.Description,
                Created = DateTimeOffset.Now,
                OwnerId = 1
            };

            _dbContext.Shows.Add(showEntity);

            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<IEnumerable<ShowListItem>> GetAllShowsAsync()
        {
            var shows = await _dbContext.Shows
                .Where(entity => entity.OwnerId == 1)
                .Select(entity => new ShowListItem
                {
                    Id = entity.Id,
                    Title = entity.Title,
                    Created = entity.Created
                })
                .ToListAsync();
            
            return shows;
        }

        public async Task <ShowDetail> GetShowByIdAsync(int showId)
        {
            var showEntity = await _dbContext.Shows
                .FirstOrDefaultAsync(e =>
                    e.Id == showId && e.OwnerId == 1
                );
            return showEntity is null ? null : new ShowDetail
            {
                Id = showEntity.Id,
                Title = showEntity.Title,
                Description = showEntity.Description,
                Created = showEntity.Created,
                Updated = showEntity.Updated
            };
        }

        public async Task<bool> UpdateShowAsync(ShowUpdate request)
        {
            var showEntity = await _dbContext.Shows.FindAsync(request.Id);

            if (showEntity?.OwnerId != 1)
                return false;
            
            showEntity.Title = request.Title;
            showEntity.Description = request.Description;
            showEntity.Updated = DateTimeOffset.Now;

            var numberOfChanges = await _dbContext.SaveChangesAsync();

            return numberOfChanges == 1;
        }

        public async Task<bool> DeleteShowAsync(int showId)
        {
            var showEntity = await _dbContext.Shows.FindAsync(showId);

            if (showEntity?.OwnerId != 1)
                return false;

            _dbContext.Shows.Remove(showEntity);
            return await _dbContext.SaveChangesAsync() == 1;
        }
    }
}