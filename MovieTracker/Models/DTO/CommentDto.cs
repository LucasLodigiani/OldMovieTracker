using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MovieTracker.Models.DTO
{
    public class CommentDto
    {
        [Key]
        public string Id { get; set; }

        public string? Username { get; set; }

        [Required]
        public string? Content { get; set; }

        public DateTime? Created { get; set; }

        [Required]
        public float Rate { get; set; }

        public string? Role { get; set; }

        [Required]
        public string? MovieId { get; set; }

        public string? UserId { get; set; }

        [JsonIgnore]
        public virtual User? User { get; set; }
    }
}
