using FIAP_CloudGames.API.Extensions.Converters.Promotion;
using FIAP_CloudGames.API.Requests.Promotion;
using FIAP_CloudGames.API.Responses.Promotion;
using FIAP_CloudGames.Domain.Enums;
using FIAP_CloudGames.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FIAP_CloudGames.API.Controllers;

[ApiController]
[Route("api/v1/promotions")]
[Authorize(Roles = nameof(UserRole.Admin))]
public class PromotionController : ControllerBase
{
    private readonly IPromotionService _promotionService;

    public PromotionController(IPromotionService promotionService)
    {
        _promotionService = promotionService;
    }

    /// <summary>
    /// Create a promotion
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreatePromotionRequest request)
    {
        var dto = request.ToDto();
        
        var id = await _promotionService.CreateAsync(dto);
        
        return CreatedAtAction(nameof(GetById), new { id = id }, id);
    }

    /// <summary>
    /// Update an existing promotion
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Update(Guid id, [FromBody] UpdatePromotionRequest request)
    {
        var dto = request.ToDto();
        
        await _promotionService.UpdateAsync(id, dto);

        return NoContent();
    }

    /// <summary>
    /// Delete a promotion by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _promotionService.DeleteAsync(id);
        
        return NoContent();
    }

    /// <summary>
    /// Get a promotion by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GetPromotionByIdResponse>> GetById(Guid id)
    {
        var dto = await _promotionService.GetByIdAsync(id);

        return Ok(dto.ToResponse());
    }

    /// <summary>
    /// Get all promotions
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<ICollection<GetPromotionByIdResponse>>> GetAll()
    {
        var dtoList = await _promotionService.GetAllAsync();
        
        return Ok(dtoList.ToResponse());
    }

    /// <summary>
    /// Get all active promotions
    /// </summary>
    /// <returns></returns>
    [HttpGet("active")]
    public async Task<ActionResult<ICollection<GetPromotionByIdResponse>>> GetAllActive()
    {
        var dtoList = await _promotionService.GetAllActiveAsync();
        
        return Ok(dtoList.ToResponse());
    }

    /// <summary>
    /// Active an existing promotion
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPatch("{id:guid}/active")]
    public async Task<ActionResult> Active(Guid id)
    {
        await _promotionService.ActivePromotionAsync(id);
        
        return NoContent();
    }

    /// <summary>
    /// Deactive an existing promotion
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPatch("{id:guid}/deactive")]
    public async Task<ActionResult> Deactive(Guid id)
    {
        await _promotionService.DeactivePromotionAsync(id);
        
        return NoContent();
    }
}