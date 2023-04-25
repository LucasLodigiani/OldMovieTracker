using Microsoft.EntityFrameworkCore;
using MovieTracker.Data;
using MovieTracker.Models;
using MovieTracker.Models.DTO;

namespace MovieTracker.Services
{
    public class MovieService : IMovieService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;
        public MovieService(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<Movie> CreateMovie(MovieDto movieDto)
        {
            var categories = new List<Category>();

            // Itera sobre cada categoría del cuerpo de la solicitud POST y verifica si ya existe en la base de datos
            // Si existe, agréguela a la lista de categorías para esta película
            foreach (var category in movieDto.Categories)
            {
                var existingCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Name == category.Name);
                if (existingCategory != null)
                {
                    categories.Add(existingCategory);
                }
                else
                {
                    categories.Add(new Category { Name = category.Name });
                }
            }

            //Guardar imagen
            string mediaUrl = await SaveImage(movieDto.Image);

            // Crea la nueva película y agrega las categorías relacionadas
            var newMovie = new Movie
            {
                Title = movieDto.Title,
                Description = movieDto.Description,
                ReleaseDate = movieDto.ReleaseDate,
                Duration = TimeSpan.Parse(movieDto.Duration),
                Rate = 0,
                Categories = categories,
                MediaUrl = mediaUrl
            };

            await _context.Movies.AddAsync(newMovie);
            await _context.SaveChangesAsync();
            return newMovie;
        }

        public async Task<Boolean> DeleteMovie(Guid id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return false;
            }

            _context.Movies.Remove(movie);
            await DeleteImage(movie.MediaUrl);
            await _context.SaveChangesAsync();

            return true;
        }

        public Task<Movie> GetAllMovies()
        {
            throw new NotImplementedException();
        }

        public Task<Movie> UpdateMovie(Guid id)
        {
            throw new NotImplementedException();
        }


        public async Task<string> SaveImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }

            var fileName = Path.GetFileName(file.FileName);
            var fileExtension = Path.GetExtension(fileName);
            //var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);

            // Genera un nombre de archivo único con un DateTime en el nombre para evitar conflictos y que sea unico
            var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";

            // La ruta donde se guardará el archivo es wwwroot/Media
            var filePath = Path.Combine(_env.WebRootPath, "Media", uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return uniqueFileName;
        }

        public async Task DeleteImage(string mediaUrl)
        {
            string filePath = Path.Combine(_env.WebRootPath, "Media", mediaUrl);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            
        }

    }
}
