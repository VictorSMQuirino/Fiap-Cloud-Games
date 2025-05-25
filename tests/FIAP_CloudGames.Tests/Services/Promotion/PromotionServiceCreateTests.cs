using FIAP_CloudGames.Domain.Entities;
using FIAP_CloudGames.Domain.Exceptions;
using FIAP_CloudGames.Domain.Extensions;
using FIAP_CloudGames.Tests.Fixtures;
using Moq;

namespace FIAP_CloudGames.Tests.Services;

[Collection(nameof(PromotionServiceCollection))]
public class PromotionServiceCreateTests : PromotionServiceTests
{
    [Fact]
    public async Task ValidPromotion_CreateAsync_MustCreatePromotion()
    {
        //Arrange
        var dto = _fixture.GetValidCreateDto();
        var game = _fixture.GetValidGame();

        _gameRepositoryMock
            .Setup(r => r.GetByIdAsync(dto.GameId))
            .ReturnsAsync(game);
        
        //Act
        await _service.CreateAsync(dto);

        //Assert
        _gameRepositoryMock.Verify(r => r.GetByIdAsync(dto.GameId), Times.Once);
        _repositoryMock.Verify(r => r.CreateAsync(It.IsAny<Promotion>()), Times.Once);
    }

    [Theory]
    [InlineData("B48A21A2-C977-458C-A707-7E9EF0CC7D47", 150, "2026-06-10")]
    [InlineData("B48A21A2-C977-458C-A707-7E9EF0CC7D47", 0, "2026-06-10")]
    [InlineData("B48A21A2-C977-458C-A707-7E9EF0CC7D47", null, "2026-06-10")]
    [InlineData("B48A21A2-C977-458C-A707-7E9EF0CC7D47", 50, "1899-06-10")]
    public async Task InvalidPromotion_CreateAsync_MustFailBecauseDtoIsInvalid(Guid gameId, int? discountPercentage, string deadline)
    {
        //Arrange
        var date = DateOnly.Parse(deadline);
        var dto = _fixture.GetInvalidCreateDto(gameId, discountPercentage, date);
        
        //Act
        var act = async () => await _service.CreateAsync(dto);
        
        //Assert
        await Assert.ThrowsAsync<ValidationException>(act);
        _gameRepositoryMock.Verify(r => r.GetByIdAsync(gameId), Times.Never);
        _repositoryMock.Verify(r => r.CreateAsync(It.IsAny<Promotion>()), Times.Never);
    }

    [Fact]
    public async Task ValidPromotion_CreateAsync_MustFailBecauseGameNotfound()
    {
        //Arrange
        var dto = _fixture.GetValidCreateDto();
        
        _gameRepositoryMock
            .Setup(r =>r.GetByIdAsync(dto.GameId))
            .ReturnsAsync(null as Game);
        
        //Act
        var act = async () => await _service.CreateAsync(dto);
        
        //Assert
        await Assert.ThrowsAsync<NotFoundException>(act);
        _gameRepositoryMock.Verify(r => r.GetByIdAsync(dto.GameId), Times.Once);
        _repositoryMock.Verify(r => r.CreateAsync(It.IsAny<Promotion>()), Times.Never);
    }
}