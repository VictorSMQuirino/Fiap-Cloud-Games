using FIAP_CloudGames.Domain.Interfaces.Repositories;
using FIAP_CloudGames.Infrastructure.Context;
using FIAP_CloudGames.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FIAP_CloudGames.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<FiapCloudGamesDbContext>(options => options.UseNpgsql(
            configuration.GetConnectionString("DefaultConnection")
            ));

        services.AddScoped<IGameRepository, GameRepository>();

        return services;
    }
}