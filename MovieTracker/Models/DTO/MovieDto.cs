using Microsoft.Build.Framework;
using Newtonsoft.Json;

namespace MovieTracker.Models.DTO
{
    public class MovieDto
    {
        [JsonIgnore]
        public string? Id { get; set; }

        [JsonIgnore]
        public float? Rate { get; set; }
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateOnly ReleaseDate { get; set; }

        [Required]
        public string Duration { get; set; }
        //public float Rate { get; set; }

        public string? MediaUrl { get; set; }

        [Required]
        public IFormFile Image { get; set; }

        public ICollection<Category> Categories { get; set; }
    }
}
