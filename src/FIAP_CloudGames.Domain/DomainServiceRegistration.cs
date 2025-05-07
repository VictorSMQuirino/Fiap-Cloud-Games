using System.Reflection;
using FIAP_CloudGames.Domain.Interfaces.Services;
using FIAP_CloudGames.Domain.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace FIAP_CloudGames.Domain;

public static class DomainServiceRegistration
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddScoped<IGameService, GameService>();
        
        return services;
    }
}