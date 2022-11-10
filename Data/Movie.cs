﻿namespace MoviesApi.Data
{
    public class Movie
    {
        public int Id { get; set; }
        [MaxLength(250)]
        public string Title { get; set; }
        public int Year { get; set; }
        public double Rate { get; set; }
        [MaxLength(2500)]
        public string StoryLine { get; set; }
        public byte[] poster { get; set; }
        public Byte GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
