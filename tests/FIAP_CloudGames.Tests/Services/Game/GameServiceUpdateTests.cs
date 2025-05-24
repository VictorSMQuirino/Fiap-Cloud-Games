using System.Linq.Expressions;
using FIAP_CloudGames.Domain.Entities;
using FIAP_CloudGames.Domain.Exceptions;
using FIAP_CloudGames.Tests.Fixtures;
using Moq;

namespace FIAP_CloudGames.Tests.Services;

[Collection(nameof(GameServiceCollection))]
public class GameServiceUpdateTests : GameServiceTests
{
    [Fact]
    public async Task ValidGame_UpdateAsync_MustUpdateGame()
    {
        //Arrange
        var dto = _fixture.GetValidUpdateDto();
        var game = _fixture.GetValidGame();
        
        _repositoryMock
            .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(game);

        _repositoryMock
            .Setup(r => r.ExistsBy(It.IsAny<Expression<Func<Game, bool>>>()))
            .ReturnsAsync(false);

        //Act
        await _service.UpdateAsync(It.IsAny<Guid>(), dto);

        //Assert
        _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        _repositoryMock.Verify(r => r.ExistsBy(It.IsAny<Expression<Func<Game, bool>>>()), Times.Once);
        _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Game>()), Times.Once);
    }

    [Theory]
    [InlineData("", 50, "2025-06-25")]
    [InlineData("Game teste", -1, "2025-06-25")]
    [InlineData("Game teste", 50, "1899-06-25")]
    public async Task InvalidGame__UpdateAsync_MustFailbBecauseDtoIsInvalid(string title, decimal price, string releaseDate)
    {
        //Arrange
        var date = DateOnly.Parse(releaseDate);
        var dto = _fixture.GetInvalidUpdateDto(title, price, date);
        
        //Act
        var act = async () => await _service.UpdateAsync(It.IsAny<Guid>(), dto);
        
        //Assert
        await Assert.ThrowsAsync<ValidationException>(act);
        _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()), Times.Never);
        _repositoryMock.Verify(r => r.ExistsBy(It.IsAny<Expression<Func<Game, bool>>>()), Times.Never);
        _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Game>()), Times.Never);
    }

    [Fact]
    public async Task ValidGame_UpdateAsync_MustFailBecauseGameNotFound()
    {
        //Arrange
        var dto = _fixture.GetValidUpdateDto();
        
        _repositoryMock
            .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(null as Game);
        
        //Act
        var act = async () => await _service.UpdateAsync(It.IsAny<Guid>(), dto);
        
        //Assert
        await Assert.ThrowsAsync<NotFoundException>(act);
        _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        _repositoryMock.Verify(r => r.ExistsBy(It.IsAny<Expression<Func<Game, bool>>>()), Times.Never);
        _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Game>()), Times.Never);
    }

    [Fact]
    public async Task ValidGame_UpdateAsync_MustFailBecauseGameTittleIsDuplicated()
    {
        //Arrange
        var dto = _fixture.GetValidUpdateDto();
        var game = _fixture.GetValidGame();
        
        _repositoryMock
            .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(game);

        _repositoryMock
            .Setup(r => r.ExistsBy(It.IsAny<Expression<Func<Game, bool>>>()))
            .ReturnsAsync(true);
        
        //Act
        var act = async () => await _service.UpdateAsync(It.IsAny<Guid>(), dto);
        
        //Assert
        await Assert.ThrowsAsync<DuplicatedEntityException>(act);
        _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        _repositoryMock.Verify(r => r.ExistsBy(It.IsAny<Expression<Func<Game, bool>>>()), Times.Once);
        _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Game>()), Times.Never);
    }
}