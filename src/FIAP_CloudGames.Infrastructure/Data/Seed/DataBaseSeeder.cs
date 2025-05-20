using FIAP_CloudGames.Domain.Entities;
using FIAP_CloudGames.Domain.Enums;
using FIAP_CloudGames.Domain.Interfaces.Services;
using FIAP_CloudGames.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FIAP_CloudGames.Infrastructure.Data.Seed;

public class DataBaseSeeder
{
    private readonly FiapCloudGamesDbContext _context;
    private readonly IPasswordService _passwordService;

    public DataBaseSeeder(FiapCloudGamesDbContext context, IPasswordService passwordService)
    {
        _context = context;
        _passwordService = passwordService;
    }

    public async Task SeedAsync(IConfiguration configuration)
    {
        var adminId = Guid.Parse(configuration["AdminUser:Id"]);
        if (!_context.Users.AsNoTracking().Any(u => u.Id == adminId && u.Role == UserRole.Admin))
        {
            var adminUser = new User
            {
                Id = adminId,
                Name = "AdminUser",
                Email = "admin@admin.com",
                Password = _passwordService.HashPassword(configuration["AdminUser:Password"]),
                Role = UserRole.Admin
            };
            
            await _context.Users.AddAsync(adminUser);
            await _context.SaveChangesAsync();
        }
    }
}