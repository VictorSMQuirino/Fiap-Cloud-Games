using FIAP_CloudGames.Domain.Entities;
using FIAP_CloudGames.Domain.Exceptions;
using FIAP_CloudGames.Tests.Fixtures;
using Moq;

namespace FIAP_CloudGames.Tests.Services;

[Collection(nameof(PromotionServiceCollection))]
public class PromotionServiceDeleteTests : PromotionServiceTests
{
    [Fact]
    public async Task ValidId_DeleteAsync_MustDeletePromotion()
    {
        //Arrange
        var promotion = _fixture.GetValidPromotion();
        
        _repositoryMock
            .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(promotion);
        
        //Act
        await _service.DeleteAsync(Guid.NewGuid());

        //Assert
        _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        _repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<Promotion>()), Times.Once);
    }

    [Fact]
    public async Task ValidId_DeleteAsync_MustFailBecausePromotionNotFound()
    {
        //Arrange
        _repositoryMock
            .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(null as Promotion);

        //Act
        var act = async () => await _service.DeleteAsync(Guid.NewGuid());

        //Assert
        await Assert.ThrowsAsync<NotFoundException>(act);
        _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
    }

    [Fact]
    public async Task ValidId_DeleteAsync_MustFailBecausePromotionIsActive()
    {
        //Arrange
        var promotion = _fixture.GetValidPromotion(true);
        
        _repositoryMock
            .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(promotion);
        
        //Act
        var act = async () => await _service.DeleteAsync(Guid.NewGuid());
        
        //Assert
        await Assert.ThrowsAsync<DomainException>(act);
        _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        _repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<Promotion>()), Times.Never);
    }
}