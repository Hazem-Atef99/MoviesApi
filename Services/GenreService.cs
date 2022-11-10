using Microsoft.EntityFrameworkCore;

namespace MoviesApi.Services
{
    public class GenreService : IGenreService
    {
        private readonly ApplicationDbContext _context;

        public GenreService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Genre> Add(Genre genre)
        {
            _context.SaveChanges();
            return genre;
        }

        public Genre Delete(Genre genre)
        {
            _context.Remove(genre);
            _context.SaveChanges();
            return genre;
        }

        public async Task<IEnumerable<Genre>> GetAll()
        {
          return await _context.genres.OrderBy(gen => gen.Name).ToListAsync();
        }

        public async Task<Genre> GetByID(byte id)
        {
           return await _context.genres.FirstOrDefaultAsync(g => g.Id == id);
        }

        public Task<bool> isValidGenre(byte id)
        {
          return  _context.genres.AnyAsync(x => x.Id == id);
        }

        public Genre Update(Genre genre)
        {
            _context.Update(genre);
            _context.SaveChanges();
            return genre;
        }
    }
}
