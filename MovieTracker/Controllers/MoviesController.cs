using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieTracker.Data;
using MovieTracker.Models;
using MovieTracker.Models.DTO;

namespace MovieTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetMovies()
        {
            var moviesWithCategories = await _context.Movies.Include(m => m.Categories).ToListAsync();
            return Ok(moviesWithCategories);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovie([FromBody] MovieDto movieDto)
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

            // Crea la nueva película y agrega las categorías relacionadas
            var newMovie = new Movie
            {
                Title = movieDto.Title,
                Description = movieDto.Description,
                ReleaseDate = movieDto.ReleaseDate,
                Duration = TimeSpan.Parse(movieDto.Duration),
                Rate = 0,
                Categories = categories
            };

            _context.Movies.Add(newMovie);
            await _context.SaveChangesAsync();

            return Ok(newMovie);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            if (_context.Movies == null)
            {
                return NotFound();
            }
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Movies
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Movie>>> GetMovie()
        //{
        //  if (_context.Movie == null)
        //  {
        //      return NotFound();
        //  }
        //    return await _context.Movie.ToListAsync();
        //}

        // GET: api/Movies/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Movie>> GetMovie(int id)
        //{
        //  if (_context.Movie == null)
        //  {
        //      return NotFound();
        //  }
        //    var movie = await _context.Movie.FindAsync(id);

        //    if (movie == null)
        //    {
        //        return NotFound();
        //    }

        //    return movie;
        //}

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutMovie(int id, Movie movie)
        //{
        //    if (id != movie.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(movie).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!MovieExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //    [HttpPost]
        //    public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        //    {
        //      if (_context.Movie == null)
        //      {
        //          return Problem("Entity set 'ApplicationDbContext.Movie'  is null.");
        //      }
        //        _context.Movie.Add(movie);
        //        await _context.SaveChangesAsync();

        //        return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
        //    }

        //    // DELETE: api/Movies/5
        //    [HttpDelete("{id}")]
        //    public async Task<IActionResult> DeleteMovie(int id)
        //    {
        //        if (_context.Movie == null)
        //        {
        //            return NotFound();
        //        }
        //        var movie = await _context.Movie.FindAsync(id);
        //        if (movie == null)
        //        {
        //            return NotFound();
        //        }

        //        _context.Movie.Remove(movie);
        //        await _context.SaveChangesAsync();

        //        return NoContent();
        //    }

        //    private bool MovieExists(int id)
        //    {
        //        return (_context.Movie?.Any(e => e.Id == id)).GetValueOrDefault();
        //    }
    }
}
