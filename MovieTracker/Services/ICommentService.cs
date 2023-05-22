using MovieTracker.Models.DTO;

namespace MovieTracker.Services
{
    public interface ICommentService
    {

        Task<IEnumerable<CommentDto>> GetCommentsByMovie(Guid movieId);

    }
}
