using FIAP_CloudGames.Domain.DTO.User;
using FIAP_CloudGames.Domain.Entities;
using FIAP_CloudGames.Domain.Enums;
using FIAP_CloudGames.Domain.Exceptions;
using FIAP_CloudGames.Domain.Extensions;
using FIAP_CloudGames.Domain.Interfaces.Repositories;
using FIAP_CloudGames.Domain.Interfaces.Services;
using FIAP_CloudGames.Domain.Validators.User;
using Microsoft.Extensions.Configuration;

namespace FIAP_CloudGames.Domain.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IPasswordService _passwordService;

    public AuthService(IUserRepository userRepository, ITokenService tokenService, IPasswordService passwordService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _passwordService = passwordService;
    }

    public async Task SignUp(CreateUserDto dto, IConfiguration configuration)
    {
        var validationResult = await new CreateUserValidator().ValidateAsync(dto);
        
        if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors.ConvertValidationErrors());
        
        var alreadyExistisUserWithEmail = await _userRepository.ExistsBy(user => user.Email == dto.Email);
        
        if (alreadyExistisUserWithEmail) throw new DuplicatedEntityException(nameof(User), nameof(dto.Email), dto.Email);

        var hashedPassword = _passwordService.HashPassword(dto.Password);
        
        var newUser = dto.ToUser(hashedPassword);

        await _userRepository.CreateAsync(newUser);
    }
}