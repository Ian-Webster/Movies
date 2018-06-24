using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movies.Repository.Entities;

namespace Movies.Repository.EntityTypeConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(pk => pk.Id);

            builder.HasMany(fk => fk.MovieRatings)
                .WithOne(fk => fk.User)
                .HasForeignKey(fk => fk.UserId);
        }
    }
}
