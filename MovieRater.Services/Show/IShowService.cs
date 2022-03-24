using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieRater.Models;
using MovieRater.Models.Show;

namespace MovieRater.Services.Show
{
    public interface IShowService
    {
        Task<bool> CreateShowAsync(ShowCreate request);
        Task<IEnumerable<ShowListItem>> GetAllShowsAsync();
        Task<ShowDetail> GetShowByIdAsync(int noteId);
        Task<bool> UpdateShowAsync(ShowUpdate request);
        Task<bool> DeleteShowAsync(int noteId);
    }
}