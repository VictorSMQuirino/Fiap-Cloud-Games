using AutoFixture;
using FIAP_CloudGames.Domain.Entities;
using FIAP_CloudGames.Domain.Interfaces.Repositories;
using FIAP_CloudGames.Domain.Interfaces.Services;
using FIAP_CloudGames.Application.Services;
using Moq;

namespace FIAP_CloudGames.Tests.Fixtures;

[CollectionDefinition(nameof(UserServiceCollection))]
public class UserServiceCollection : ICollectionFixture<UserServiceFixture>;

public class UserServiceFixture : ServiceFixture
{
    public IUserService GetService(Mock<IUserRepository> repositoryMock, Mock<IApplicationUserService> applicationUserServiceMock)
    {
        return new UserService(repositoryMock.Object, applicationUserServiceMock.Object);
    }
    
    internal Mock<IUserRepository> GetRepositoryMock() => new();
    
    internal Mock<IApplicationUserService> GetApplicationUserServiceMock() => new();

    internal User GetValidUser()
    {
        return _fixture.Build<User>()
            .With(u => u.UserGames, [])
            .Create();
    }

    internal ICollection<User> GetValidUserList()
    {
        return _fixture.Build<User>()
            .With(u => u.UserGames, [])
            .CreateMany()
            .ToList();
    }
}