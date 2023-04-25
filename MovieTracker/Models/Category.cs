using System.Text.Json.Serialization;

namespace MovieTracker.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Movie>? Movies { get; set; }
    }
}
