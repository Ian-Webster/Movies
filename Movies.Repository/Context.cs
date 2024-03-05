using Microsoft.EntityFrameworkCore;
using Movies.Repository.Entities;
using System.Reflection;
using System;

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
            base.OnModelCreating(modelBuilder);
            // get the the assembly your EntityTypeConfigurations are in
            // in this case they are in the same assembly as this context class
            var assembly = Assembly.GetAssembly(typeof(Context));
            if (assembly == null)
            {
                throw new Exception($"Failed to get assembly for {nameof(Context)}");
            }
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        }
    }
}
