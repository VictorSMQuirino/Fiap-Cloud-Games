using FIAP_CloudGames.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FIAP_CloudGames.Infrastructure.EntityTypeConfiguration;

public class GameEntityTypeConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.ToTable("Games");
        builder.HasKey(game => game.Id);
        builder.Property(game => game.Title).IsRequired();
        builder.Property(game => game.Price).IsRequired();
        
        builder.HasIndex(game => game.Title).IsUnique();
    }
}