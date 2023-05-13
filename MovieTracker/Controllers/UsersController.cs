using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieTracker.Models;
using MovieTracker.Models.DTO;

namespace MovieTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public UsersController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var user = _userManager.Users
            .Select(u => new UserDto
            {
                Id = u.Id,
                Username = u.UserName,
            })
            .ToList();
            
            return Ok(user);
        }

        [HttpGet("id")]
        public async Task<ActionResult<UserDto>> GetUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var userRoles = await _userManager.GetRolesAsync(user);
       
            var userDto = new UserDto
            {
                Id = user.Id,
                Username = user.UserName,
                Role = userRoles[0]

            };
            return Ok(userDto);
        }
    }
}
