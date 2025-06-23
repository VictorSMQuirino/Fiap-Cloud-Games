using AutoFixture;
using FIAP_CloudGames.Domain.DTO.Auth;
using FIAP_CloudGames.Domain.DTO.User;
using FIAP_CloudGames.Domain.Entities;
using FIAP_CloudGames.Domain.Interfaces.Repositories;
using FIAP_CloudGames.Domain.Interfaces.Services;
using FIAP_CloudGames.Application.Services;
using Moq;

namespace FIAP_CloudGames.Tests.Fixtures;

[CollectionDefinition(nameof(AuthServiceCollection))]
public class AuthServiceCollection : ICollectionFixture<AuthServiceFixture>;

public class AuthServiceFixture : ServiceFixture
{
    public IAuthService GetService(Mock<IUserRepository> userRepositoryMock, Mock<ITokenService> tokenServiceMock, Mock<IPasswordService> passwordServiceMock)
    {
        return new AuthService(userRepositoryMock.Object, tokenServiceMock.Object, passwordServiceMock.Object);
    }
    
    internal static Mock<IUserRepository> GetUserRepositoryMock() => new();
    
    internal static Mock<ITokenService> GetTokenServiceMock() => new();
    
    internal static Mock<IPasswordService> GetPasswordServiceMock() => new();

    internal CreateUserDto GetCreateUserDto(string name = "Fulano", string email = "teste@email.com", string password = "Password@123")
    {
        return _fixture.Build<CreateUserDto>()
            .With(dto => dto.Name, name)
            .With(dto => dto.Email, email)
            .With(dto => dto.Password, password)
            .Create();
    }
    
    internal LoginDto GetValidLoginDto()
    {
        return _fixture.Build<LoginDto>().Create();
    }

    internal User GetValidUser()
    {
        return _fixture.Build<User>()
            .With(u => u.UserGames, new List<UserGame>())
            .Create();
    }

    internal string GetValidToken()
    {
        return _fixture.Build<string>().Create();
    }
}