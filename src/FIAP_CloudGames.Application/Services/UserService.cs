using FIAP_CloudGames.Application.Converters;
using FIAP_CloudGames.Domain.DTO.User;
using FIAP_CloudGames.Domain.Entities;
using FIAP_CloudGames.Domain.Enums;
using FIAP_CloudGames.Domain.Exceptions;
using FIAP_CloudGames.Domain.Interfaces.Repositories;
using FIAP_CloudGames.Domain.Interfaces.Services;

namespace FIAP_CloudGames.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IApplicationUserService _applicationUserService;

    public UserService(IUserRepository userRepository, IApplicationUserService applicationUserService)
    {
        _userRepository = userRepository;
        _applicationUserService = applicationUserService;
    }

    public async Task<UserDto?> GetByIdAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user is null) throw new NotFoundException(nameof(User), id);

        return user.ToDto();
    }

    public async Task<ICollection<UserDto>> GetAllAsync() 
        => (await _userRepository.GetAllAsync()).ToDtoList();

    public async Task ChangeRoleAsync(Guid id)
    {
        if (_applicationUserService.GetUserId() == id)
            throw new DomainException("Cannot change own role");

        var user = await _userRepository.GetByIdAsync(id);

        if (user is null) throw new NotFoundException(nameof(User), id);

        user.Role = user.Role == UserRole.Admin
            ? UserRole.Common
            : UserRole.Admin;

        await _userRepository.UpdateAsync(user);
    }
}