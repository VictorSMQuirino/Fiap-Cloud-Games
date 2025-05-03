using FIAP_CloudGames.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FIAP_CloudGames.Infrastructure.EntityTypeConfiguration;

public class UserGameEntityTypeConfiguration : IEntityTypeConfiguration<UserGame>
{
    public void Configure(EntityTypeBuilder<UserGame> builder)
    {
        builder.ToTable("UserGames");
        builder.HasKey(userGame => userGame.Id);
        
        builder.HasOne(userGame => userGame.User)
            .WithMany(user => user.UserGames)
            .HasForeignKey(userGame => userGame.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(userGame => userGame.Game)
            .WithMany(game => game.UserGames)
            .HasForeignKey(userGame => userGame.GameId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasIndex(userGame => new { userGame.UserId, userGame.GameId }).IsUnique();
    }
}