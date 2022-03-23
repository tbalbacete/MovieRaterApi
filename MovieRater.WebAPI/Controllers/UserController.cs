using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieRater.Models.User;
using MovieRater.Services.User;
using MovieRater.Data.Entities;

namespace MovieRater.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        //Add new user
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromForm] UserRegister model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var registerResult = await _userService.RegisterUserAsync(model);
            if (registerResult)
            {
                return Ok("User was registered.");
            }

            return BadRequest("User could not be registered");
        }

        [HttpGet("{userId:int}")]
        public async Task<IActionResult> GetById([FromRoute] int userId)
        {
            //Get the UserDetail that matches  the UserID and set it to a var
            var userDetail = await _userService.GetUserByIdAsync(userId);
            
            //if there is no matching user(it's null) return NotFound
            if(userDetail is null)
            {
                return NotFound("The user does not exist");
            }

            return Ok(userDetail);
        }        
    }
}