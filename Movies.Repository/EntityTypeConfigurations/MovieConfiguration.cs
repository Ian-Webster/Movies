using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movies.Repository.Entities;

namespace Movies.Repository.EntityTypeConfigurations
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.ToTable("Movie");

            builder.HasKey(pk => pk.Id);

            builder.HasMany(fk => fk.MovieRatings)
                .WithOne(fk => fk.Movie)
                .HasForeignKey(fk => fk.MovieId);
        }
    }
}
