using Microsoft.Extensions.DependencyInjection;
using Movies.Business;
using Movies.Business.Interfaces;

namespace Movies.Infrastructure.IoC
{
    public static class BusinessServices
    {

        public static IServiceCollection Configure(this IServiceCollection services)
        {
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IRatingService, RatingService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }

    }
}
