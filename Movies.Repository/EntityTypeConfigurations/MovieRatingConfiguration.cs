using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movies.Repository.Entities;

namespace Movies.Repository.EntityTypeConfigurations
{
    public class MovieRatingConfiguration : IEntityTypeConfiguration<MovieRating>
    {
        public void Configure(EntityTypeBuilder<MovieRating> builder)
        {
            builder.ToTable("MovieRating");

            builder.HasKey(pk => new { pk.MovieId, pk.UserId });

            builder.HasOne(fk => fk.Movie)
                .WithMany(fk => fk.MovieRatings)
                .HasForeignKey(fk => fk.MovieId);

            builder.HasOne(fk => fk.User)
                .WithMany(fk => fk.MovieRatings)
                .HasForeignKey(fk => fk.UserId);
        }
    }
}
