using System.Linq.Expressions;
using FIAP_CloudGames.Domain.Entities;
using FIAP_CloudGames.Domain.Exceptions;
using FIAP_CloudGames.Tests.Fixtures;
using FluentAssertions;
using Moq;

namespace FIAP_CloudGames.Tests.Services.Auth;

[Collection(nameof(AuthServiceCollection))]
public class AuthServiceLoginTests : AuthServiceTests
{
    [Fact]
    public async Task ValidLoginDto_Login_MustReturnValidToken()
    {
        //Arrange
        var loginDto = _fixture.GetValidLoginDto();
        var user = _fixture.GetValidUser();
        var token = _fixture.GetValidToken();

        _userRepositoryMock
            .Setup(r => r.GetBy(It.IsAny<Expression<Func<User, bool>>>()))
            .ReturnsAsync(user);

        _passwordServiceMock
            .Setup(s => s.VerifyHashedPassword(user.Password, loginDto.Password))
            .Returns(true);

        _tokenServiceMock
            .Setup(s => s.GenerateToken(user))
            .Returns(token);

        //Act
        var result = await _service.Login(loginDto);

        //Assert
        result.Should().Be(token);
        _userRepositoryMock.Verify(r => r.GetBy(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
        _passwordServiceMock.Verify(s => s.VerifyHashedPassword(user.Password, loginDto.Password), Times.Once);
        _tokenServiceMock.Verify(s => s.GenerateToken(user), Times.Once);
    }

    [Fact]
    public async Task ValidLoginDto_Login_MustFailBecauseUserNotFound()
    {
        //Arrange
        var loginDto = _fixture.GetValidLoginDto();
        
        _userRepositoryMock
            .Setup(r => r.GetBy(It.IsAny<Expression<Func<User, bool>>>()))
            .ReturnsAsync(null as User);

        //Act
        Func<Task> act = async () => await _service.Login(loginDto);

        //Assert
        await Assert.ThrowsAsync<InvalidCredentialException>(act);
        _userRepositoryMock.Verify(r => r.GetBy(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
        _passwordServiceMock.Verify(s => s.VerifyHashedPassword(It.IsAny<string>(), loginDto.Password), Times.Never);
        _tokenServiceMock.Verify(s => s.GenerateToken(It.IsAny<User>()), Times.Never);
    }

    [Fact]
    public async Task ValidLoginDto_Login_MustFailBecausePasswordIsIncorrect()
    {
        //Arrange
        var loginDto = _fixture.GetValidLoginDto();
        var user = _fixture.GetValidUser();
        
        _userRepositoryMock
            .Setup(r => r.GetBy(It.IsAny<Expression<Func<User, bool>>>()))
            .ReturnsAsync(user);
        
        _passwordServiceMock
            .Setup(s => s.VerifyHashedPassword(user.Password, loginDto.Password))
            .Returns(false);

        //Act
        Func<Task> act = async () => await _service.Login(loginDto);

        //Assert
        await Assert.ThrowsAsync<InvalidCredentialException>(act);
        _userRepositoryMock.Verify(r => r.GetBy(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
        _passwordServiceMock.Verify(s => s.VerifyHashedPassword(It.IsAny<string>(), loginDto.Password), Times.Once);
        _tokenServiceMock.Verify(s => s.GenerateToken(It.IsAny<User>()), Times.Never);
    }
}