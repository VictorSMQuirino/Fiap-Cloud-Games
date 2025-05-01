using FIAP_CloudGames.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FIAP_CloudGames.Infrastructure.EntityTypeConfiguration;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(user => user.Id);
        builder.Property(user => user.Name).IsRequired();
        builder.Property(user => user.Email).IsRequired();
        builder.Property(user => user.Password).IsRequired();
    }
}
