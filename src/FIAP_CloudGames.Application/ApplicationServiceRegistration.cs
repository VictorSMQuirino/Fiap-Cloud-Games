using System.Reflection;
using FIAP_CloudGames.Application.Services;
using FIAP_CloudGames.Domain.Interfaces.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace FIAP_CloudGames.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddScoped<IGameService, GameService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IPasswordService, BcryptService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IPromotionService, PromotionService>();
        services.AddScoped<IUserService, UserService>();
        
        return services;
    }
}