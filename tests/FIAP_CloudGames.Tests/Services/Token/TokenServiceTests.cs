using FIAP_CloudGames.Application.Auth;
using FIAP_CloudGames.Domain.Interfaces.Services;
using FIAP_CloudGames.Tests.Fixtures;

namespace FIAP_CloudGames.Tests.Services.Token;

public class TokenServiceTests
{
    protected readonly TokenServiceFixture _fixture;
    protected readonly JwtSettings _jwtSettings;
    protected readonly ITokenService _service;

    public TokenServiceTests()
    {
        _fixture = new TokenServiceFixture();
        _jwtSettings = _fixture.GetJwtSettings();
        _service = _fixture.GetService(_jwtSettings);
    }
}