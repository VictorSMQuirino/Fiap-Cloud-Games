using FIAP_CloudGames.Domain.Entities;
using FIAP_CloudGames.Domain.Exceptions;
using FIAP_CloudGames.Tests.Fixtures;
using Moq;

namespace FIAP_CloudGames.Tests.Services;

[Collection(nameof(GameServiceCollection))]
public class GameServiceDeleteTests : GameServiceTests
{
    [Fact]
    public async Task ValidId_DeleteAsync_MustDeleteGame()
    {
        //Arrange
        var game = _fixture.GetValidGame();
        
        _repositoryMock
            .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(game);
        
        //Act
        await _service.DeleteAsync(Guid.NewGuid());

        //Assert
        _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        _repositoryMock.Verify(r => r.DeleteAsync(game), Times.Once);
    }

    [Fact]
    public async Task ValidId_DelteAsync_MustFailBecauseGameNotFound()
    {
        //Arrange
        var game = _fixture.GetValidGame();
        
        _repositoryMock
            .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(null as Game);
        
        //Act
        Func<Task> act = async () => await _service.DeleteAsync(Guid.NewGuid());
        
        //Assert
        await Assert.ThrowsAsync<NotFoundException>(act);
        _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        _repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<Game>()), Times.Never);
    }
}