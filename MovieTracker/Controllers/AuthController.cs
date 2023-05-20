using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieTracker.Models;
using MovieTracker.Services;
using System.IdentityModel.Tokens.Jwt;

namespace MovieTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;
        //private readonly HttpContextAccessor _contextAccessor;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
            //_contextAccessor = contextAccessor;

        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid payload");
                var (status, message) = await _authService.Login(model);
                if (status == 0)
                    return BadRequest(message);
                return Ok(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegistrationModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid payload");
                var (status, message) = await _authService.Register(model, UserRoles.Admin);
                if (status == 0)
                {
                    return BadRequest(message);
                }
                return CreatedAtAction(nameof(Register), model);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("TokenCheck")]
        public async Task<IActionResult> TokenCheck()
        {
            // Obtener el valor del header "Authorization"
            if (Request.Headers.TryGetValue("Authorization", out var authorizationHeaderValue))
            {
                // Verificar si el valor del header comienza con "Bearer "
                var bearerToken = authorizationHeaderValue.FirstOrDefault();
                if (!string.IsNullOrEmpty(bearerToken) && bearerToken.StartsWith("Bearer "))
                {
                    // Extraer el token JWT sin el prefijo "Bearer "
                    var jwt = bearerToken.Substring(7);

                    var (status, message) = await _authService.TokenCheck(jwt);
                    if (status == 0)
                    {
                        return Ok(message);
                    }
                    else if(status == 2)
                    {
                        return Ok(message);
                    }
                    

                    return NoContent();
                }
            }
            return BadRequest("Token JWT no encontrado en el header de autorización.");
        }


    }
}

