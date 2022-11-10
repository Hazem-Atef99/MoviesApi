namespace MoviesApi.Dtos
{
    public class MovieDto
    {
        [MaxLength(250)]
        public string Title { get; set; }
        public int Year { get; set; }
        public double Rate { get; set; }
        [MaxLength(2500)]
        public string StoryLine { get; set; }
        public IFormFile? poster { get; set; }
        public Byte GenreId { get; set; }
    }
}
