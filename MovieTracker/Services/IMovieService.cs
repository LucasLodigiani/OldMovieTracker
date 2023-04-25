using MovieTracker.Models;
using MovieTracker.Models.DTO;

namespace MovieTracker.Services
{
    public interface IMovieService
    {
        Task<Movie> GetAllMovies();

        Task<Movie> CreateMovie(MovieDto movieDto);

        Task<Movie> UpdateMovie(Guid id);

        Task<Boolean> DeleteMovie(Guid id);
    }
}
