using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieTracker.Data;
using MovieTracker.Models;
using MovieTracker.Models.DTO;

namespace MovieTracker.Services
{
    public class CommentService : ICommentService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        public CommentService(ApplicationDbContext context, UserManager<User> userManager) { 
            _context = context;
            _userManager = userManager;
        }
        public async Task<IEnumerable<CommentDto>> GetCommentsByMovie(Guid movieId)
        {
            var comments = await _context.Comments
            .Include(c => c.User)
            .Where(c => c.MovieId == movieId)
            .ToListAsync();

            if(comments == null) { 
                return null; 
            };

            var commentDtos = new List<CommentDto>();

            foreach (var comment in comments)
            {
                var user = comment.User;
                var roles = await _userManager.GetRolesAsync(user);
                var roleName = roles.FirstOrDefault();

                var commentDto = new CommentDto
                {
                    Id = comment.Id,
                    Username = user?.UserName,
                    Content = comment.Content,
                    Created = comment.Created,
                    Rate = comment.Rate,
                    Role = roleName,
                    MovieId = comment.MovieId.ToString(),
                    UserId = comment.UserId,
                    //User = user
                };

                commentDtos.Add(commentDto);
            }

            if (comments.Count > 0)
            {
                return commentDtos;
            }
            else
            {
                return null;
            }

            
        }
    }
}
