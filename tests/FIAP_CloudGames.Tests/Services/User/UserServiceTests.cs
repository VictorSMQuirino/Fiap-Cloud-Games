using FIAP_CloudGames.Domain.Interfaces.Repositories;
using FIAP_CloudGames.Domain.Interfaces.Services;
using FIAP_CloudGames.Tests.Fixtures;
using Moq;

namespace FIAP_CloudGames.Tests.Services;

public class UserServiceTests
{
    protected readonly UserServiceFixture _fixture;
    protected readonly Mock<IUserRepository> _repositoryMock;
    protected readonly Mock<IApplicationUserService> _applicationUserServiceMock;
    protected readonly IUserService _service;

    public UserServiceTests()
    {
        _fixture = new UserServiceFixture();
        _repositoryMock = _fixture.GetRepositoryMock();
        _applicationUserServiceMock = _fixture.GetApplicationUserServiceMock();
        _service = _fixture.GetService(_repositoryMock, _applicationUserServiceMock);
    }
}