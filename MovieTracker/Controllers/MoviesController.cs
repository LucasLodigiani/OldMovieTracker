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
using MovieTracker.Services;

namespace MovieTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMovieService _movieService;

        public MoviesController(ApplicationDbContext context, IMovieService movieService)
        {
            _context = context;
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMovies()
        {
            var moviesWithCategories = await _context.Movies.Include(m => m.Categories).ToListAsync();
            return Ok(moviesWithCategories);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovie([FromForm] MovieDto movieDto)
        {
            ModelState.Remove("MediaUrl");
            if (ModelState.IsValid)
            {
                var newMovie = await _movieService.CreateMovie(movieDto);
                return Ok(newMovie);
            }
            else
            {
                return StatusCode(422);
            }
           
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(Guid id)
        {
            if (_context.Movies == null)
            {
                return NotFound();
            }
            
            
            var result = await _movieService.DeleteMovie(id);
            if(result)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
            
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
