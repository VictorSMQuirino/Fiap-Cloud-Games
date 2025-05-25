using FIAP_CloudGames.Domain.Entities;
using FIAP_CloudGames.Domain.Exceptions;
using FIAP_CloudGames.Tests.Fixtures;
using Moq;

namespace FIAP_CloudGames.Tests.Services;

[Collection(nameof(PromotionServiceCollection))]
public class PromotionServiceUpdateTests : PromotionServiceTests
{
    [Fact]
    public async Task ValidPromotion_UpdateAsync_MustUpdatePromotion()
    {
        //Arrange
        var dto = _fixture.GetValidUpdateDto();
        var promotion = _fixture.GetValidPromotion();
        var game = _fixture.GetValidGame();
        
        _repositoryMock
            .Setup(r => r.GetByIdAsync(promotion.Id))
            .ReturnsAsync(promotion);
        
        _gameRepositoryMock
            .Setup(r => r.GetByIdAsync(dto.GameId))
            .ReturnsAsync(game);

        //Act
        await _service.UpdateAsync(promotion.Id, dto);

        //Assert
        _repositoryMock.Verify(r => r.GetByIdAsync(promotion.Id), Times.Once);
        _gameRepositoryMock.Verify(r => r.GetByIdAsync(dto.GameId), Times.Once);
        _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Promotion>()), Times.Once);
    }

    [Theory]
    [InlineData("B48A21A2-C977-458C-A707-7E9EF0CC7D47", 150, "2026-06-10")]
    [InlineData("B48A21A2-C977-458C-A707-7E9EF0CC7D47", 0, "2026-06-10")]
    [InlineData("B48A21A2-C977-458C-A707-7E9EF0CC7D47", null, "2026-06-10")]
    [InlineData("B48A21A2-C977-458C-A707-7E9EF0CC7D47", 50, "1899-06-10")]
    public async Task ValidPromotion_UpdateAsync_MustFailBecausePromotionIsInvalid(Guid gameId, int? discountPercentage, string deadline)
    {
        //Arrange
        var date = DateOnly.Parse(deadline);
        var dto = _fixture.GetInvalidUpdateDto(gameId, discountPercentage, date);

        //Act
        var act = async () => await _service.UpdateAsync(gameId, dto);

        //Assert
        await Assert.ThrowsAsync<ValidationException>(act);
        _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()), Times.Never);
        _gameRepositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()), Times.Never);
        _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Promotion>()), Times.Never);
    }

    [Fact]
    public async Task ValidPromotion_UpdateAsync_MustFailBecausePromotionNotFound()
    {
        //Arrange
        var dto = _fixture.GetValidUpdateDto();
        
        _repositoryMock
            .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(null as Promotion);
        
        //Act
        var act = async () => await _service.UpdateAsync(Guid.NewGuid(), dto);
        
        //Assert
        await Assert.ThrowsAsync<NotFoundException>(act);
        _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        _gameRepositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()), Times.Never);
        _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Promotion>()), Times.Never);
    }

    [Fact]
    public async Task ValidPromotion_UpdateAsync_MustFailBecauseGameNotFound()
    {
        //Arrange
        var dto = _fixture.GetValidUpdateDto();
        var promotion = _fixture.GetValidPromotion();
        
        _repositoryMock
            .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(promotion);
        
        _gameRepositoryMock
            .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(null as Game);
        
        //Act
        var act = async () => await _service.UpdateAsync(Guid.NewGuid(), dto);
        
        //Assert
        await Assert.ThrowsAsync<NotFoundException>(act);
        _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        _gameRepositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Promotion>()), Times.Never);
    }
}