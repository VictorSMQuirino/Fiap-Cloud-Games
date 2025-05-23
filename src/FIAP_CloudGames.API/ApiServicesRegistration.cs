using FIAP_CloudGames.API.Extensions.Auth;
using FIAP_CloudGames.Domain.Interfaces.Services;

namespace FIAP_CloudGames.API;

public static class ApiServicesRegistration
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddScoped<IApplicationUserService, ApplicationUser>();

        return services;
    }
}