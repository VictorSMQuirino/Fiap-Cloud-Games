using FIAP_CloudGames.Domain.Entities;
using FIAP_CloudGames.Domain.Exceptions;
using FIAP_CloudGames.Tests.Fixtures;
using Moq;

namespace FIAP_CloudGames.Tests.Services;

[Collection(nameof(PromotionServiceCollection))]
public class PromotionServiceTestsDeactivePromotionTests : PromotionServiceTests
{
    [Fact]
    public async Task ValidId_DeactivePromotionAsync_MustDeactivePromotion()
    {
        //Arrange
        var promotion = _fixture.GetValidPromotion(true);
        
        _repositoryMock
            .Setup(r => r.GetByIdAsync(promotion.Id))
            .ReturnsAsync(promotion);
        
        //Act
        await _service.DeactivePromotionAsync(promotion.Id);

        //Assert
        _repositoryMock.Verify(r => r.GetByIdAsync(promotion.Id), Times.Once);
        _repositoryMock.Verify(r => r.UpdateAsync(promotion), Times.Once);
    }

    [Fact]
    public async Task ValidId_DeeactiPromotionAsync_MustFailBecausePromotionNotFound()
    {
        //Arrange
        _repositoryMock
            .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(null as Promotion);
        
        //Act
        var act = () => _service.DeactivePromotionAsync(Guid.NewGuid());
        
        //Assert
        await Assert.ThrowsAsync<NotFoundException>(act);
        _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Promotion>()), Times.Never);
    }

    [Fact]
    public async Task ValidId_DeactivePromotionAsync_MustFailBecausePromotionAlreadyIsInactive()
    {
        //Arrange
        var promotion = _fixture.GetValidPromotion();
        
        _repositoryMock
            .Setup(r => r.GetByIdAsync(promotion.Id))
            .ReturnsAsync(promotion);
        
        //Act
        var act = () => _service.DeactivePromotionAsync(promotion.Id);
        
        //Assert
        await Assert.ThrowsAsync<DomainException>(act);
        _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Promotion>()), Times.Never);
    }
}