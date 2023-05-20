using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            //var user = _userManager.Users
            //.Select(u => new UserDto
            //{
            //    Id = u.Id,
            //    Username = u.UserName,
            //    Role = null,
            //})
            //.ToList();


            //TO DO: Mejorar este codigo, es ineficiente en terminos de rendimiento porque realiza muchas consultas a la base de datos.
            var users = await _userManager.Users.ToListAsync();

            var userDtos = new List<UserDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var roleName = roles.FirstOrDefault();

                var userDto = new UserDto
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Role = roleName
                };

                userDtos.Add(userDto);
            }


            return Ok(userDtos);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if(id == null)
            {
                return BadRequest();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return BadRequest();
            }

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }


        }


    }
}
