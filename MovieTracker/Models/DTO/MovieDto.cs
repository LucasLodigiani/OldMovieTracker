namespace MovieTracker.Models.DTO
{
    public class MovieDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public string Duration { get; set; }
        //public float Rate { get; set; }

        public ICollection<Category> Categories { get; set; }
    }
}
