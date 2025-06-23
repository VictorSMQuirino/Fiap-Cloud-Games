using FIAP_CloudGames.Application.Converters;
using FIAP_CloudGames.Domain.Entities;
using FIAP_CloudGames.Domain.Exceptions;
using FIAP_CloudGames.Tests.Fixtures;
using FluentAssertions;
using Moq;

namespace FIAP_CloudGames.Tests.Services;

[Collection(nameof(UserServiceCollection))]
public class UserServiceGetTests : UserServiceTests
{
    [Fact]
    public async Task ValidId_GetByIdAsync_MustReturnUser()
    {
        //Arrange
        var user = _fixture.GetValidUser();
        
        _repositoryMock
            .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(user);

        //Act
        var result = await _service.GetByIdAsync(user.Id);

        //Assert
        result.Should().Be(user.ToDto());
        _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
    }

    [Fact]
    public async Task ValidId_GetByIdAsync_MustFailBecauseUserNotFound()
    {
        //Arrange
        _repositoryMock
            .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(null as User);
        
        //Act
        var act = async () => await _service.GetByIdAsync(Guid.NewGuid());
        
        //Assert
        await Assert.ThrowsAsync<NotFoundException>(act);
        _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
    }

    [Fact]
    public async Task ValidList_GetAllAsync_MustReturnUsersList()
    {
        //Arrange
        var userList = _fixture.GetValidUserList();
        
        _repositoryMock
            .Setup(r => r.GetAllAsync())
            .ReturnsAsync(userList);
        
        //Act
        var result = await _service.GetAllAsync();
        
        //Assert
        result.Should().BeEquivalentTo(userList.ToDtoList());
        _repositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
    }
}