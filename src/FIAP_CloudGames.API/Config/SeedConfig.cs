using FIAP_CloudGames.Domain.Interfaces.Services;
using FIAP_CloudGames.Infrastructure.Context;
using FIAP_CloudGames.Infrastructure.Data.Seed;

namespace FIAP_CloudGames.API.Config;

public static class SeedConfig
{
    public static async Task SeedAdminUser(IServiceProvider serviceProvider, IConfiguration configuration)
    {
        using var scope = serviceProvider.CreateScope();
        var services = scope.ServiceProvider;
        
        var context = services.GetRequiredService<FiapCloudGamesDbContext>();
        var passwordService = services.GetRequiredService<IPasswordService>();
        var seeder = new DataBaseSeeder(context, passwordService);
        await seeder.SeedAsync(configuration);
    }
}