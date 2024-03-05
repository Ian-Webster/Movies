using DataAccess.Repository;
using Microsoft.Extensions.DependencyInjection;
using Movies.Repository;
using Movies.Repository.Interfaces;
using Movies.Repository.Repositories;

namespace Movies.Infrastructure.IoC
{
    public static class Repositories
    {
        public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<UnitOfWork<Context>>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IRatingRepository, RatingRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
