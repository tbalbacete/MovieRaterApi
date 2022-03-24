using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieRater.Models.Rating;
using MovieRater.Services.Rating;

namespace MovieRater.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        } 

        [HttpPost("Add")]
        public async Task<IActionResult> CreateRating([FromForm] RatingCreate request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(await _ratingService.CreateRatingAsync(request))
            {
                return Ok("Rating added successfully.");
            }

            return BadRequest("Rating could not be added");
        }

        
    }
}