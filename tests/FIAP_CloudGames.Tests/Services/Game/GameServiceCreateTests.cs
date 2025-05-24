using System.Linq.Expressions;
using FIAP_CloudGames.Domain.Entities;
using FIAP_CloudGames.Domain.Exceptions;
using FIAP_CloudGames.Tests.Fixtures;
using Moq;

namespace FIAP_CloudGames.Tests.Services;

[Collection(nameof(GameServiceCollection))]
public class GameServiceCreateTests : GameServiceTests
{
    [Fact]
    public async Task ValidGame_CreateAsync_MustCreategame()
    {
        //Arrange
        var dto = _fixture.GetValidCreateDto();
        
        _repositoryMock
            .Setup(r => r.ExistsBy(It.IsAny<Expression<Func<Game, bool>>>()))
            .ReturnsAsync(false);

        //Act
        await _service.CreateAsync(dto);

        //Assert
        _repositoryMock.Verify(r => r.ExistsBy(It.IsAny<Expression<Func<Game, bool>>>()), Times.Once);
        _repositoryMock.Verify(r => r.CreateAsync(It.IsAny<Game>()), Times.Once);
    }

    [Theory]
    [InlineData("", 50, "2026-05-18")]
    [InlineData("Game teste", -3, "2026-05-18")]
    [InlineData("Game teste", 50, "1899-05-18")]
    public async Task InvalidGame_CreateAsync_MustFailBecauseDtoIsInvalid(string title, decimal price, string releaseDate)
    {
        //Arrange
        var date = DateOnly.Parse(releaseDate);
        var dto = _fixture.GetInvalidCreateDto(title, price, date);
        
        //Act
        Func<Task> act = async () => await _service.CreateAsync(dto);
        
        //Assert
        await Assert.ThrowsAsync<ValidationException>(act);
        _repositoryMock.Verify(r => r.ExistsBy(It.IsAny<Expression<Func<Game, bool>>>()), Times.Never);
        _repositoryMock.Verify(r => r.CreateAsync(It.IsAny<Game>()), Times.Never);
    }

    [Fact]
    public async Task ValidGame_CreateAsync_MustfailBecauseTitleIsDuplicated()
    {
        //Arrange
        var dto = _fixture.GetValidCreateDto();
        
        _repositoryMock
            .Setup(r => r.ExistsBy(It.IsAny<Expression<Func<Game, bool>>>()))
            .ReturnsAsync(true);
        
        //Act
        Func<Task> act = async () => await _service.CreateAsync(dto);
        
        //Assert
        await Assert.ThrowsAsync<DuplicatedEntityException>(act);
        _repositoryMock.Verify(r => r.ExistsBy(It.IsAny<Expression<Func<Game, bool>>>()), Times.Once);
        _repositoryMock.Verify(r => r.CreateAsync(It.IsAny<Game>()), Times.Never);
    }
}