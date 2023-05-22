using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
    public class CommentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ICommentService _commentService;

        public CommentsController(ApplicationDbContext context, ICommentService commentService)
        {
            _context = context;
            _commentService = commentService;
        }

        // GET: api/Comments
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Comment>>> GetAllComments()
        {
          if (_context.Comments == null)
          {
              return NotFound();
          }
            return await _context.Comments.ToListAsync();
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CommentDto>>> GetCommentDto(Guid id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var commentDtos = await _commentService.GetCommentsByMovie(id);
            if(commentDtos == null)
            {
                return NotFound();
            }
            

            return Ok(commentDtos);
        }

        // PUT: api/Comments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutCommentDto(string id, CommentDto commentDto)
        //{
        //    if (id != commentDto.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(commentDto).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CommentDtoExists(id))
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

        // POST: api/Comments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CommentDto>> PostCommentDto(CommentDto commentDto)
        {
          if (_context.Comments == null)
          {
              return Problem("Entity set 'ApplicationDbContext.CommentDto'  is null.");
          }
            //Obtener el id del usuario desde el jwt
            var userId = User.Claims.FirstOrDefault(c => c.Type == "unique_id").Value;

            var comment = new Comment
            {
                Id = Guid.NewGuid().ToString(),
                Content = commentDto.Content,
                Created = DateTime.UtcNow,
                Rate = commentDto.Rate,
                MovieId = Guid.Parse(commentDto.MovieId),
                UserId = userId,
            };
            _context.Comments.Add(comment);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CommentDtoExists(commentDto.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCommentDto", new { id = commentDto.Id }, commentDto);
        }

        // DELETE: api/Comments/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteCommentDto(string id)
        //{
        //    if (_context.CommentDto == null)
        //    {
        //        return NotFound();
        //    }
        //    var commentDto = await _context.CommentDto.FindAsync(id);
        //    if (commentDto == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.CommentDto.Remove(commentDto);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool CommentDtoExists(string id)
        {
            return (_context.Comments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
