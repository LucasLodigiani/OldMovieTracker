using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MovieTracker.Models
{
    public class Comment
    {
        [Key]
        public string Id { get; set; }

        public string Content { get; set; }

        public DateTime Created { get; set; }
        
        public float Rate { get; set; }

        public Guid MovieId { get; set; }

        public  Movie Movie { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        
    }
}
