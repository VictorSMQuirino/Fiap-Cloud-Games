using FIAP_CloudGames.Domain.Entities;
using FIAP_CloudGames.Domain.Exceptions;
using FIAP_CloudGames.Tests.Fixtures;
using Moq;

namespace FIAP_CloudGames.Tests.Services;

[Collection(nameof(UserServiceCollection))]
public class UserServiceChangeRoleTests : UserServiceTests
{
    [Fact]
    public async Task ValidUserId_ChangeRoleAsync_MustChangeUserRole()
    {
        //Arrange
        var user = _fixture.GetValidUser();
        
        _applicationUserServiceMock
            .Setup(r => r.GetUserId())
            .Returns(Guid.NewGuid());
        
        _repositoryMock
            .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(user);
        
        //Act
        await _service.ChangeRoleAsync(user.Id);

        //Assert
        _applicationUserServiceMock.Verify(r => r.GetUserId(), Times.Once);
        _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<User>()), Times.Once);
    }

    [Fact]
    public async Task ValidUserId_ChangeRoleAsync_MustFailBecauseUserTriedToChangeOwnRole()
    {
        //Arrange
        var userId = Guid.NewGuid();
        _applicationUserServiceMock
            .Setup(r => r.GetUserId())
            .Returns(userId);
        
        //Act
        var act = async () => await _service.ChangeRoleAsync(userId);
        
        //Assert
        await Assert.ThrowsAsync<DomainException>(act);
        _applicationUserServiceMock.Verify(r => r.GetUserId(), Times.Once);
        _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()), Times.Never);
        _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<User>()), Times.Never);
    }

    [Fact]
    public async Task ValidUserId_ChangeRoleAsync_MustFailBecauseUserNotFound()
    {
        //Arrange
        _applicationUserServiceMock
            .Setup(r => r.GetUserId())
            .Returns(Guid.NewGuid());
        
        _repositoryMock
            .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(null as User);
        
        //Act
        var act = async () => await _service.ChangeRoleAsync(Guid.NewGuid());
        
        //Assert
        await Assert.ThrowsAsync<NotFoundException>(act);
        _applicationUserServiceMock.Verify(r => r.GetUserId(), Times.Once);
        _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<User>()), Times.Never);
    }
}