using FIAP_CloudGames.Domain.Interfaces.Repositories;
using FIAP_CloudGames.Domain.Interfaces.Services;
using FIAP_CloudGames.Tests.Fixtures;
using Moq;

namespace FIAP_CloudGames.Tests.Services.Auth;

public class AuthServiceTests
{
    protected readonly AuthServiceFixture _fixture;
    protected readonly Mock<IUserRepository> _userRepositoryMock;
    protected readonly Mock<ITokenService> _tokenServiceMock;
    protected readonly Mock<IPasswordService> _passwordServiceMock;
    protected readonly IAuthService _service;

    public AuthServiceTests()
    {
        _fixture = new AuthServiceFixture();
        _userRepositoryMock = AuthServiceFixture.GetUserRepositoryMock();
        _tokenServiceMock = AuthServiceFixture.GetTokenServiceMock();
        _passwordServiceMock = AuthServiceFixture.GetPasswordServiceMock();
        _service = _fixture.GetService(_userRepositoryMock, _tokenServiceMock, _passwordServiceMock);
    }
}