using AutoFixture;
using FIAP_CloudGames.Domain.DTO.Promotion;
using FIAP_CloudGames.Domain.Entities;
using FIAP_CloudGames.Domain.Interfaces.Repositories;
using FIAP_CloudGames.Domain.Interfaces.Services;
using FIAP_CloudGames.Domain.Services;
using Moq;

namespace FIAP_CloudGames.Tests.Fixtures;

[CollectionDefinition(nameof(PromotionServiceCollection))]
public class PromotionServiceCollection : ICollectionFixture<PromotionServiceFixture>;

public class PromotionServiceFixture : ServiceFixture
{
    public IPromotionService GetService(Mock<IPromotionRepository> repository, Mock<IGameRepository> gameRepository) 
        => new PromotionService(repository.Object, gameRepository.Object);

    internal Mock<IPromotionRepository> GetRepositoryMock() => new();
    
    internal Mock<IGameRepository> GetGameRepositoryMock() => new();

    internal CreatePromotionDto GetValidCreateDto()
    {
        return _fixture.Build<CreatePromotionDto>()
            .With(dto => dto.DiscountPercentage, 50)
            .Create();
    }
    
    internal CreatePromotionDto GetInvalidCreateDto(Guid gameId, int? discountPercentage, DateOnly date)
    {
        return _fixture.Build<CreatePromotionDto>()
            .With(dto => dto.DiscountPercentage, discountPercentage)
            .With(dto => dto.GameId, gameId)
            .With(dto => dto.Deadline, date)
            .Create();
    }
    
    internal UpdatePromotionDto GetInvalidUpdateDto(Guid gameId, int? discountPercentage, DateOnly date)
    {
        return _fixture.Build<UpdatePromotionDto>()
            .With(dto => dto.DiscountPercentage, discountPercentage)
            .With(dto => dto.GameId, gameId)
            .With(dto => dto.Deadline, date)
            .Create();
    }

    internal Game GetValidGame()
    {
        return _fixture.Build<Game>()
            .With(g => g.UserGames, new List<UserGame>())
            .Create();
    }


    internal UpdatePromotionDto GetValidUpdateDto()
    {
        return _fixture.Build<UpdatePromotionDto>()
            .With(dto => dto.DiscountPercentage, 50)
            .Create();
    }

    internal Promotion GetValidPromotion(bool active = false, Game? providedGame = null)
    {
        var game = providedGame ?? GetValidGame();
        
        return _fixture.Build<Promotion>()
            .With(p => p.Game, game)
            .With(p => p.Active, active)
            .Create();
    }

    internal ICollection<Promotion> GetValidPromotionList()
    {
        return _fixture.Build<Promotion>()
            .With(p => p.Game, GetValidGame())
            .CreateMany()
            .ToList();
    }
}