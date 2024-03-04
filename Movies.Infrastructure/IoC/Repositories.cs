using Microsoft.Extensions.DependencyInjection;
using Movies.Repository;
using Movies.Repository.Interfaces;

namespace Movies.Infrastructure.IoC
{
    public static class Repositories
    {
        public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IRatingRepository, RatingRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
