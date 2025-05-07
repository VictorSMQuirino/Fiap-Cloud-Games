using FIAP_CloudGames.API.Extensions.Converters.Game;
using FIAP_CloudGames.API.Requests.Game;
using FIAP_CloudGames.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace FIAP_CloudGames.API.Controllers;

[ApiController]
[Route("api/v1/games")]
public class GameController : ControllerBase
{
    private readonly IGameService _gameService;

    public GameController(IGameService gameService)
    {
        _gameService = gameService;
    }

    /// <summary>
    /// Create a game
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateGameRequest request)
    {
        var dto = request.ToCreateGameDto();

        var id = await _gameService.CreateAsync(dto);
        
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    /// <summary>
    /// Update a existing game
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Update(Guid id, [FromBody] UpdateGameRequest request)
    {
        var dto = request.ToUpdateGameDto();
        
        await _gameService.UpdateAsync(id, dto);
        
        return NoContent();
    }

    /// <summary>
    /// Delete a game by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _gameService.DeleteAsync(id);
        
        return NoContent();
    }

    /// <summary>
    /// Get a game by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult> GetById(Guid id)
    {
        var dto = await _gameService.GetByIdAsync(id);

        return Ok(dto?.ToResponse());
    }

    /// <summary>
    /// Get all games
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        var dtoList = await _gameService.GetAllAsync();
        
        return Ok(dtoList.ToResponse());
    }
}