using FIAP_CloudGames.Application.Converters;
using FIAP_CloudGames.Application.Validators.Game;
using FIAP_CloudGames.Domain.DTO;
using FIAP_CloudGames.Domain.Entities;
using FIAP_CloudGames.Domain.Exceptions;
using FIAP_CloudGames.Domain.Extensions;
using FIAP_CloudGames.Domain.Interfaces.Repositories;
using FIAP_CloudGames.Domain.Interfaces.Services;

namespace FIAP_CloudGames.Application.Services;

public class GameService : IGameService
{
    private readonly IGameRepository _gameRepository;

    public GameService(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<Guid> CreateAsync(CreateGameDto gameDto)
    {
        var validationResult = await new CreateGameValidator().ValidateAsync(gameDto);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors.ConvertValidationErrors());
        
        if (await _gameRepository.ExistsBy(game => game.Title.ToUpper().Equals(gameDto.Title.ToUpper())))
            throw new DuplicatedEntityException(nameof(Game), nameof(Game.Title), gameDto.Title);

        var newGame = gameDto.ConvertDtoToGame();

        await _gameRepository.CreateAsync(newGame);

        return newGame.Id;
    }

    public async Task UpdateAsync(Guid id, UpdateGameDto gameDto)
    {
        var validationResult = await new UpdateGameValidator().ValidateAsync(gameDto);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors.ConvertValidationErrors());

        var game = await _gameRepository.GetByIdAsync(id);

        if (game is null) throw new NotFoundException(nameof(Game), id);

        if (await _gameRepository.ExistsBy(gameInDb =>
                gameInDb.Title.ToUpper().Equals(gameDto.Title.ToUpper())
                && gameInDb.Id != game.Id))
            throw new DuplicatedEntityException(nameof(game), nameof(Game.Title), gameDto.Title);

        game = gameDto.ConvertDtoToGame(game);
        await _gameRepository.UpdateAsync(game);
    }

    public async Task DeleteAsync(Guid id)
    {
        var game = await _gameRepository.GetByIdAsync(id);

        if (game is null) throw new NotFoundException(nameof(Game), id);

        await _gameRepository.DeleteAsync(game);
    }

    public async Task<GameDto?> GetByIdAsync(Guid id)
    {
        var game = await _gameRepository.GetByIdAsync(id);

        return game is null
            ? throw new NotFoundException(nameof(Game), id)
            : game.ToGameDto();
    }

    public async Task<ICollection<GameDto>> GetAllAsync()
    {
        var games = await _gameRepository.GetAllAsync();

        return games.ToGameDtoList();
    }
}