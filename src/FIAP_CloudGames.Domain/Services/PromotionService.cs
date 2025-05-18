using FIAP_CloudGames.Domain.DTO.Promotion;
using FIAP_CloudGames.Domain.Entities;
using FIAP_CloudGames.Domain.Exceptions;
using FIAP_CloudGames.Domain.Extensions;
using FIAP_CloudGames.Domain.Interfaces.Repositories;
using FIAP_CloudGames.Domain.Interfaces.Services;
using FIAP_CloudGames.Domain.Validators.Promotion;

namespace FIAP_CloudGames.Domain.Services;

public class PromotionService : IPromotionService
{
    private readonly IPromotionRepository _promotionRepository;
    private readonly IGameRepository _gameRepository;

    public PromotionService(IPromotionRepository promotionRepository, IGameRepository gameRepository)
    {
        _promotionRepository = promotionRepository;
        _gameRepository = gameRepository;
    }

    public async Task<Guid> CreateAsync(CreatePromotionDto dto)
    {
        var validationResult = await new CreatePromotionValidator().ValidateAsync(dto);

        if (!validationResult.IsValid) 
            throw new ValidationException(validationResult.Errors.ConvertValidationErrors());
        
        var game = await _gameRepository.GetByIdAsync(dto.GameId);

        if (game is null) throw new NotFoundException(nameof(Game), dto.GameId);

        var promotion = dto.ToEntity();
        
        await _promotionRepository.CreateAsync(promotion);

        return promotion.Id;
    }

    public async Task UpdateAsync(Guid id, UpdatePromotionDto dto)
    {
        var validationResult = await new UpdatePromotionValidator().ValidateAsync(dto);
        
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors.ConvertValidationErrors());
        
        var promotion = await _promotionRepository.GetByIdAsync(id);
        
        if (promotion is null) throw new NotFoundException(nameof(Promotion), id);
        
        var game = await _gameRepository.GetByIdAsync(dto.GameId);
        
        if (game is null) throw new NotFoundException(nameof(Game), dto.GameId);

        promotion = dto.ToEntity(promotion);
        
        await _promotionRepository.UpdateAsync(promotion);
    }

    public async Task DeleteAsync(Guid id)
    {
        var promotion = await _promotionRepository.GetByIdAsync(id);
        
        if (promotion is null) throw new NotFoundException(nameof(Promotion), id);
        
        if (promotion.Active) throw new DomainException("Cannot delete an active promotion.");
        
        await _promotionRepository.DeleteAsync(promotion);
    }

    public async Task<PromotionDto?> GetByIdAsync(Guid id)
    {
        var promotion = await _promotionRepository.GetByIdAsync(id, p => p.Game);
        
        if (promotion is null) throw new NotFoundException(nameof(Promotion), id);

        var gameDto = promotion.Game.ToGameDto();

        var dto = promotion.ToDto(gameDto);
        
        return dto;
    }

    public async Task<ICollection<PromotionDto>> GetAllAsync()
    {
        var promotionList = await _promotionRepository.GetAllAsync(p => p.Game);

        return promotionList.ToPromotionDtoList();
    }

    public async Task<ICollection<PromotionDto>> GetAllActiveAsync()
    {
        var activePromotionsList = await _promotionRepository.GetListBy(p => p.Active, p => p.Game);
        
        return activePromotionsList.ToPromotionDtoList();
    }

    public async Task ActivePromotionAsync(Guid id)
    {
        var promotion = await _promotionRepository.GetByIdAsync(id);
        
        if (promotion is null) throw new NotFoundException(nameof(Promotion), id);
        
        if (promotion.Active) throw new DomainException("The promotion is already active.");
        
        var existsAnActivePromotionForGame = await _promotionRepository.ExistsBy(p => 
            p.GameId == promotion.GameId && 
            p.Id != id && 
            p.Active);
        
        if (existsAnActivePromotionForGame) 
            throw new DomainException("Already exists an active promotion for this game.");
        
        promotion.Active = true;
        await _promotionRepository.UpdateAsync(promotion);
    }

    public async Task DeactivePromotionAsync(Guid id)
    {
        var promotion = await _promotionRepository.GetByIdAsync(id);
        
        if (promotion is null) throw new NotFoundException(nameof(Promotion), id);
        
        if (!promotion.Active) throw new DomainException("The promotion is already deactive.");
        
        promotion.Active = false;
        await _promotionRepository.UpdateAsync(promotion);
    }
}