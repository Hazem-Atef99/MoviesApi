using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace MoviesApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Genre> genres { get; set; }
        public DbSet<Movie> movies { get; set; }
    }
}
