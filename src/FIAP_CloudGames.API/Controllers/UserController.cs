using FIAP_CloudGames.API.Extensions.Converters.User;
using FIAP_CloudGames.API.Responses.User;
using FIAP_CloudGames.Domain.DTO.User;
using FIAP_CloudGames.Domain.Enums;
using FIAP_CloudGames.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FIAP_CloudGames.API.Controllers;

[ApiController]
[Route("api/v1/users")]
[Authorize(Roles = nameof(UserRole.Admin))]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Get a user by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GetUserByIdResponse>> GetById(Guid id)
    {
        var dto = await _userService.GetByIdAsync(id);

        return Ok(dto?.ToResponse());
    }

    /// <summary>
    /// Get all users
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<ICollection<UserDto>>> GetAll()
    {
        var dtoList = await _userService.GetAllAsync();

        return Ok(dtoList.ToResponse());
    }

    /// <summary>
    /// Change a user role by user id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPatch("{id:guid}/change-role")]
    public async Task<ActionResult> ChangeRole(Guid id)
    {
        await _userService.ChangeRoleAsync(id);
        
        return Ok();
    }
}