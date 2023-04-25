using System.ComponentModel.DataAnnotations;

namespace MovieTracker.Models
{
    public class Movie
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public TimeSpan Duration { get; set; }
        public float Rate { get; set; }

        public ICollection<Category> Categories { get; set; }
    }
}
