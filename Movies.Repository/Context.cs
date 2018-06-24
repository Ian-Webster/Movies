using Microsoft.EntityFrameworkCore;
using Movies.Repository.Entities;
using Movies.Repository.EntityTypeConfigurations;

namespace Movies.Repository
{
    public class Context : DbContext
    {
        public DbSet<Movie> MovieDbSet { get; set; }
        public DbSet<User> UserDbSet { get; set; }
        public DbSet<MovieRating> MovieRatingDbSet { get; set; }

        public Context(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MovieConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new MovieRatingConfiguration());
        }
    }
}
