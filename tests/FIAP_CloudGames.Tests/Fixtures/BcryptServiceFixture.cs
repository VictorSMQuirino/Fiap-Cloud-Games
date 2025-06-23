using FIAP_CloudGames.Domain.Interfaces.Services;
using FIAP_CloudGames.Application.Services;

namespace FIAP_CloudGames.Tests.Fixtures;

[CollectionDefinition(nameof(BcryptServiceCollection))]
public class BcryptServiceCollection : ICollectionFixture<BcryptServiceFixture>;

public class BcryptServiceFixture : ServiceFixture
{
    public IPasswordService GetService() 
        => new BcryptService();

    public string GetValidPassword()
        => "Password@785";
}