using FIAP_CloudGames.Application.Converters;
using FIAP_CloudGames.Domain.Entities;
using FIAP_CloudGames.Domain.Exceptions;
using FIAP_CloudGames.Tests.Fixtures;
using FluentAssertions;
using Moq;

namespace FIAP_CloudGames.Tests.Services;

[Collection(nameof(GameServiceCollection))]
public class GameServiceGetTests : GameServiceTests
{
    [Fact]
    public async Task ValidId_GetByIdAsync_MustReturnGame()
    {
        //Arrange
        var game = _fixture.GetValidGame();
        var dto = game.ToGameDto();
        
        _repositoryMock
            .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(game);

        //Act
        var result = await _service.GetByIdAsync(game.Id);

        //Assert
        _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        result.Should().Be(dto);
    }

    [Fact]
    public async Task ValidId_GetByIdAsync_MustFailBecauseGameNotFound()
    {
        //Arrange
        _repositoryMock
            .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(null as Game);
        
        //Act
        Func<Task> act = async () => await _service.GetByIdAsync(Guid.NewGuid());
        
        //Assert
        await Assert.ThrowsAsync<NotFoundException>(act);
        _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
    }

    [Fact]
    public async Task ValidList_GetAllAsync()
    {
        //Arrange
        var gameList = _fixture.GetValidGameList();
        
        _repositoryMock
            .Setup(r => r.GetAllAsync())
            .ReturnsAsync(gameList);

        //Act
        var result = await _service.GetAllAsync();

        //Assert
        result.Should().BeEquivalentTo(gameList.ToGameDtoList());
    }
}