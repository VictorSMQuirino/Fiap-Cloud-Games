using System.Linq.Expressions;
using FIAP_CloudGames.Domain.Entities;
using FIAP_CloudGames.Domain.Exceptions;
using Moq;

namespace FIAP_CloudGames.Tests.Services;

[Collection(nameof(PromotionServiceTests))]
public class PromotionServiceActivePromotionTests : PromotionServiceTests
{
    [Fact]
    public async Task ValidId_ActivePromotionAsync_MustActivePromotion()
    {
        //Arrange
        var promotion = _fixture.GetValidPromotion();
        
        _repositoryMock
            .Setup(r => r.GetByIdAsync(promotion.Id))
            .ReturnsAsync(promotion);
        
        _repositoryMock
            .Setup(r => r.ExistsBy(It.IsAny<Expression<Func<Promotion, bool>>>()))
            .ReturnsAsync(false);

        //Act
        await _service.ActivePromotionAsync(promotion.Id);

        //Assert
        _repositoryMock.Verify(r => r.GetByIdAsync(promotion.Id), Times.Once);
        _repositoryMock.Verify(r => r.ExistsBy(It.IsAny<Expression<Func<Promotion, bool>>>()), Times.Once);
        _repositoryMock.Verify(r => r.UpdateAsync(promotion), Times.Once);
    }

    [Fact]
    public async Task ValidId_ActivePromotionAsync_MustFailBecausePromotionNotFound()
    {
        //Arrange
        _repositoryMock
            .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(null as Promotion);
        
        //Act
        var act = async () => await _service.ActivePromotionAsync(Guid.NewGuid());
        
        //Assert
        await Assert.ThrowsAsync<NotFoundException>(act);
        _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        _repositoryMock.Verify(r => r.ExistsBy(It.IsAny<Expression<Func<Promotion, bool>>>()), Times.Never);
        _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Promotion>()), Times.Never);
    }

    [Fact]
    public async Task ValidId_ActivePromotionAsync_MustFailBecausePromotionAlreadyIsActive()
    {
        //Arrange
        var promotion = _fixture.GetValidPromotion(true);
        
        _repositoryMock
            .Setup(r => r.GetByIdAsync(promotion.Id))
            .ReturnsAsync(promotion);
        
        //Act
        var act = async () => await _service.ActivePromotionAsync(promotion.Id);
        
        //Assert
        await Assert.ThrowsAsync<DomainException>(act);
        _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        _repositoryMock.Verify(r => r.ExistsBy(It.IsAny<Expression<Func<Promotion, bool>>>()), Times.Never);
        _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Promotion>()), Times.Never);
    }

    [Fact]
    public async Task ValidId_ActivePromotionAsync_MustFailBecauseAlreadyExistsAActivePromotionForGame()
    {
        //Arrange
        var promotion = _fixture.GetValidPromotion();
        
        _repositoryMock
            .Setup(r => r.GetByIdAsync(promotion.Id))
            .ReturnsAsync(promotion);
        
        _repositoryMock
            .Setup(r => r.ExistsBy(It.IsAny<Expression<Func<Promotion, bool>>>()))
            .ReturnsAsync(true);
        
        //Act
        var act = async () => await _service.ActivePromotionAsync(promotion.Id);
        
        //Assert
        await Assert.ThrowsAsync<DomainException>(act);
        _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        _repositoryMock.Verify(r => r.ExistsBy(It.IsAny<Expression<Func<Promotion, bool>>>()), Times.Once);
        _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Promotion>()), Times.Never);
    }
}