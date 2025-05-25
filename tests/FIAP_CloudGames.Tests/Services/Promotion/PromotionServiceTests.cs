using FIAP_CloudGames.Domain.Interfaces.Repositories;
using FIAP_CloudGames.Domain.Interfaces.Services;
using FIAP_CloudGames.Tests.Fixtures;
using Moq;

namespace FIAP_CloudGames.Tests.Services;

public class PromotionServiceTests
{
    protected readonly PromotionServiceFixture _fixture;
    protected readonly Mock<IPromotionRepository> _repositoryMock;
    protected readonly Mock<IGameRepository> _gameRepositoryMock;
    protected readonly IPromotionService _service;

    public PromotionServiceTests()
    {
        _fixture = new PromotionServiceFixture();
        _repositoryMock = _fixture.GetRepositoryMock();
        _gameRepositoryMock = _fixture.GetGameRepositoryMock();
        _service = _fixture.GetService(_repositoryMock, _gameRepositoryMock);
    }
}