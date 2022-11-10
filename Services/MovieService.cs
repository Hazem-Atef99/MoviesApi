using Microsoft.EntityFrameworkCore;
using MoviesApi.Data;

namespace MoviesApi.Services
{
    public class MovieService : IMovieService
    {
        private readonly ApplicationDbContext _context;

        public MovieService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Movie> Add(Movie movie)
        {
            await _context.AddAsync(movie);
            _context.SaveChanges();
            return movie;
        }

        public Movie Delete(Movie movie)
        {
            _context.Remove(movie);
            _context.SaveChanges();
            return movie;
        }

        public async Task<IEnumerable<Movie>> GetAllAsync(byte genreId=0 )
        {
            return await _context.movies
                            .Where(m => m.GenreId == genreId || genreId == 0)
                            .OrderByDescending(m => m.Rate)
                            .Include(m => m.Genre)
                            .ToListAsync();
        }

        public Task<Movie> GetById(int id)
        {
           return _context.movies.Include(m => m.Genre).FirstOrDefaultAsync(m => m.Id == id);
        }

        public Movie Update(Movie movie)
        {
            _context.Update(movie);
            _context.SaveChanges();
            return movie;
        }
    }
}
