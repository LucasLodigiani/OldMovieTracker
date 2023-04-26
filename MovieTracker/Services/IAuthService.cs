using MovieTracker.Models;

namespace MovieTracker.Services
{
    public interface IAuthService
    {
        Task<(int, string)> Register(RegistrationModel model, string role);
        Task<(int, string)> Login(LoginModel model);
    }
}
