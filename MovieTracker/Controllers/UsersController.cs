using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieTracker.Models;
using MovieTracker.Models.DTO;
using System.Data;

namespace MovieTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public UsersController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        [Route("GetAllUsers")]
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

        [HttpPost]
        [Route("ChangeUserRole")]
        public async Task<IActionResult> ChangeUserRole(string userId, string Role)
        {
            if (!await _roleManager.RoleExistsAsync(Role))
            {
                await _roleManager.CreateAsync(new IdentityRole(Role));
            }
                

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var roles = await _userManager.GetRolesAsync(user);
            //remover roles
            var removeRolesResult = await _userManager.RemoveFromRolesAsync(user, roles);
            if (!removeRolesResult.Succeeded)
            {
                return BadRequest(removeRolesResult.Errors);
            }

            var addRolesResult = await _userManager.AddToRoleAsync(user, Role);
            if (!addRolesResult.Succeeded)
            {
                return BadRequest(addRolesResult.Errors);
            }

            // El cambio de rol fue exitoso
            return Ok();

        }

    }
}
