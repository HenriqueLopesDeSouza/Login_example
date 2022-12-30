using Application.Business.Interfaces;
using Application.Business.Services;
using Application.Data.Repository;
using Application.DataAccess;
using Application.DataAccess.Interfaces;

namespace Application.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<ITokenRepository, TokenRepository>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IDataAccessFactory, DataAccessFactory>();


            return services;
        }
    }
}
