using FIAP_CloudGames.Domain.Interfaces.Repositories;
using FIAP_CloudGames.Domain.Interfaces.Services;
using FIAP_CloudGames.Tests.Fixtures;
using Moq;

namespace FIAP_CloudGames.Tests.Services;

public class GameServiceTests
{
    protected readonly GameServiceFixture _fixture;
    protected readonly Mock<IGameRepository> _repositoryMock;
    protected readonly IGameService _service;

    public GameServiceTests()
    {
        _fixture = new GameServiceFixture();
        _repositoryMock = _fixture.GetRepositoryMock();
        _service = _fixture.GetService(_repositoryMock);
    }
}