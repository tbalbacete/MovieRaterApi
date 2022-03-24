using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieRater.Models;
using MovieRater.Models.Show;
using MovieRater.Services.Show;

namespace MovieRater.WebAPI
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShowController : ControllerBase
    {
        private readonly IShowService _showService;

        public ShowController(IShowService showservice)
        {
            _showService = showservice;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllShows()
        {
            var shows = await _showService.GetAllShowsAsync();
            return Ok(shows);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> CreateShow([FromBody] ShowCreate request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            if (await _showService.CreateShowAsync(request))
                return Ok("Show created successfully");
            
            return BadRequest("Show could not be created.");
        }

        [HttpGet("{showId:int}")]
        public async Task<IActionResult> GetShowById([FromRoute] int showId)
        {
            var detail = await _showService.GetShowByIdAsync(showId);

            return detail is not null
                ? Ok(detail)
                : NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateShowById([FromBody] ShowUpdate request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _showService.UpdateShowAsync(request)
                ? Ok("Show was updated successfully.")
                : BadRequest("Show could not be updated.");
        }

        [HttpDelete("{showId:int}")]
        public async Task<IActionResult> DeleteShow([FromRoute] int showId)
        {
            return await _showService.DeleteShowAsync(showId)
                ? Ok($"Show {showId} was deleted successfully.")
                : BadRequest($"Show {showId} could not be deleted.");
        }
    }
}