using FIAP_CloudGames.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FIAP_CloudGames.Infrastructure.Context;

public class FiapCloudGamesDbContext(DbContextOptions<FiapCloudGamesDbContext> options) : DbContext(options)
{
    public DbSet<Game> Games { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserGame> UserGames { get; set; }
    public DbSet<Promotion> Promotions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FiapCloudGamesDbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }
}