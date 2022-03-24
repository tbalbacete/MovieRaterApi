using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieRater.Models;
using MovieRater.Models.Movie;
using MovieRater.Services.Movie;

namespace MovieRater.WebAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

    //Get api/Movie
    [HttpGet]
    public async Task<IActionResult> GetAllMovies()
    {
        var movies = await _movieService.GetAllMoviesAsync();
            return Ok(movies);
    }

    // Get api/Movie/5
    [HttpGet("{movieId:int}")]
    public async Task<IActionResult> GetMovieById([FromRoute] int movieId)
    {
        var detail = await _movieService.GetMovieByIdAsync(movieId);

        return detail is not null
        ? Ok(detail)
        : NotFound();
    }

    // [HttpGet("{rating:int}")]
    // public async Task<IActionResult> GetMovieByRating([FromRoute] int rating)
    // {
    //     var detail = await _movieService.GetMovieByRatingAsync(rating);

    //     return detail is not null
    //     ? Ok(detail)
    //     : NotFound();
    // }


    //Post api/Movie
    [HttpPost("Add")]
    public async Task<IActionResult> CreateMovie([FromBody] MovieCreate request)
    {
        if (!ModelState.IsValid)
        return BadRequest(ModelState);

        if (await _movieService.CreateMovieAsync(request))
        return Ok("Movie created successfully.");

        return BadRequest("Movie could not be created.");
    }

    // Put api/Movie
    [HttpPut]
    public async Task<IActionResult> UpdateMovieById([FromBody] MovieUpdate request)
    {
        if (!ModelState.IsValid)
        return BadRequest(ModelState);

        return await _movieService.UpdateMovieAsync(request)
        ? Ok("Movie updated successfully.")
        : BadRequest("Movie could not be updated.");
    }

    // DELETE api/Movie/5
    [HttpDelete("{movieId:int}")]
    public async Task<IActionResult> DeleteMovie([FromRoute] int movieId)
    {
        return await _movieService.DeleteMovieAsync(movieId)
        ? Ok($"Movie {movieId} was deleted successfully.")
        : BadRequest($"Movie {movieId} could not be deleted.");
    }

    }
}