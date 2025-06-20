using FIAP_CloudGames.Domain.Interfaces.Services;
using FIAP_CloudGames.Tests.Fixtures;

namespace FIAP_CloudGames.Tests.Services.Bcrypt;

public class BcryptServiceTests
{
    protected readonly BcryptServiceFixture _fixture;
    protected readonly IPasswordService _service;

    public BcryptServiceTests()
    {
        _fixture = new BcryptServiceFixture();
        _service = _fixture.GetService();
    }
}