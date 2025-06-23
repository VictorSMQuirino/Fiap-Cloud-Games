using System.Linq.Expressions;
using FIAP_CloudGames.Application.Converters;
using FIAP_CloudGames.Domain.Entities;
using FIAP_CloudGames.Domain.Exceptions;
using FIAP_CloudGames.Tests.Fixtures;
using FluentAssertions;
using Moq;

namespace FIAP_CloudGames.Tests.Services;

[Collection(nameof(PromotionServiceCollection))]
public class PromotionServiceGetTests : PromotionServiceTests
{
    [Fact]
    public async Task ValidId_GetByIdAsync_MustReturnPromotion()
    {
        //Arrange
        var promotion = _fixture.GetValidPromotion();
        var gameDto = promotion.Game.ToGameDto();
        
        _repositoryMock
            .Setup(r => r.GetByIdAsync(promotion.Id, It.IsAny<Expression<Func<Promotion, object>>[]>()))
            .ReturnsAsync(promotion);

        //Act
        var result = await _service.GetByIdAsync(promotion.Id);

        //Assert
        result.Should().Be(promotion.ToDto(gameDto));
        _repositoryMock.Verify(r => r.GetByIdAsync(promotion.Id, It.IsAny<Expression<Func<Promotion, object>>[]>()), Times.Once);
    }

    [Fact]
    public async Task ValidId_GetByIdAsync_MustFailBecausePromotionNotFound()
    {
        //Arrange
        _repositoryMock
            .Setup(r => r.GetByIdAsync(
                It.IsAny<Guid>(), 
                It.IsAny<Expression<Func<Promotion, object>>[]>())
            )
            .ReturnsAsync(null as Promotion);
        
        //Act
        var act = async () => await _service.GetByIdAsync(Guid.NewGuid());
        
        //Assert
        await Assert.ThrowsAsync<NotFoundException>(act);
        _repositoryMock.Verify(r => r.GetByIdAsync(
            It.IsAny<Guid>(), 
            It.IsAny<Expression<Func<Promotion, object>>[]>()
            ), Times.Once);
    }

    [Fact]
    public async Task ValidList_GetAllAsync_MustReturnPromotionsList()
    {
        //Arrange
        var promotionList = _fixture.GetValidPromotionList();
        
        _repositoryMock
            .Setup(r => r.GetAllAsync(It.IsAny<Expression<Func<Promotion, object>>[]>()))
            .ReturnsAsync(promotionList);
        
        //Act
        var result = await _service.GetAllAsync();
        
        //Assert
        result.Should().BeEquivalentTo(promotionList.ToPromotionDtoList());
        _repositoryMock.Verify(r => r.GetAllAsync(It.IsAny<Expression<Func<Promotion, object>>[]>()), Times.Once);
    }
}