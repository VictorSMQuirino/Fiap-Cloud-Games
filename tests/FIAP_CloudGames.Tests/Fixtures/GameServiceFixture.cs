using AutoFixture;
using FIAP_CloudGames.Domain.DTO;
using FIAP_CloudGames.Domain.Entities;
using FIAP_CloudGames.Domain.Interfaces.Repositories;
using FIAP_CloudGames.Domain.Interfaces.Services;
using FIAP_CloudGames.Application.Services;
using Moq;

namespace FIAP_CloudGames.Tests.Fixtures;

[CollectionDefinition(nameof(GameServiceCollection))]
public class GameServiceCollection : ICollectionFixture<GameServiceFixture>;

public class GameServiceFixture : ServiceFixture
{
    public IGameService GetService(Mock<IGameRepository> _repositoryMock)
    {
        return new GameService(_repositoryMock.Object);
    }
    
    internal Mock<IGameRepository> GetRepositoryMock() => new();

    internal CreateGameDto GetValidCreateDto()
    {
        return _fixture.Build<CreateGameDto>().Create();
    }

    internal CreateGameDto GetInvalidCreateDto(string title = "", decimal price = 0, DateOnly? releaseDate = null)
    {
        return _fixture.Build<CreateGameDto>()
            .With(dto => dto.Title, title)
            .With(dto => dto.Price, price)
            .With(dto => dto.ReleaseDate, releaseDate)
            .Create();
    }

    internal UpdateGameDto GetValidUpdateDto()
    {
        return _fixture.Build<UpdateGameDto>().Create();
    }

    internal UpdateGameDto GetInvalidUpdateDto(string title = "", decimal price = 0, DateOnly? releaseDate = null)
    {
        return _fixture.Build<UpdateGameDto>()
            .With(dto => dto.Title, title)
            .With(dto => dto.Price, price)
            .With(dto => dto.ReleaseDate, releaseDate)
            .Create();
    }

    internal Game GetValidGame()
    {
        return _fixture.Build<Game>()
            .With(g => g.UserGames, new List<UserGame>())
            .Create();
    }

    internal ICollection<Game> GetValidGameList()
    {
        return _fixture.Build<Game>()
            .With(g => g.UserGames, new List<UserGame>())
            .CreateMany()
            .ToList();
    }
}