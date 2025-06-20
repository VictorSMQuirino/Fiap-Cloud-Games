using System.Linq.Expressions;
using FIAP_CloudGames.Domain.Entities;
using FIAP_CloudGames.Domain.Exceptions;
using FIAP_CloudGames.Tests.Fixtures;
using Microsoft.Extensions.Configuration;
using Moq;

namespace FIAP_CloudGames.Tests.Services.Auth;

[Collection(nameof(AuthServiceCollection))]
public class AuthServiceSignUpTests : AuthServiceTests
{
    [Fact]
    public async Task ValidCreateUserDto_SignUp_MustRegisterNewUserAccount()
    {
        //Arrange
        var createUserDto = _fixture.GetCreateUserDto();

        _userRepositoryMock
            .Setup(r => r.ExistsBy(It.IsAny<Expression<Func<User, bool>>>()))
            .ReturnsAsync(false);

        //Act
        await _service.SignUp(createUserDto, It.IsAny<IConfiguration>());
        
        //Assert
        _userRepositoryMock.Verify(r => r.ExistsBy(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
        _userRepositoryMock.Verify(r => r.CreateAsync(It.IsAny<User>()), Times.Once);
    }

    [Theory]
    [InlineData("", "teste@email.com", "Password@123")]
    [InlineData("A", "teste@email.com", "Password@123")]
    [InlineData("abcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabc", "teste@email.com", "Password@123")]
    [InlineData("Fulano", "", "Password@123")]
    [InlineData("Fulano", "invalidEmail", "Password@123")]
    [InlineData("Fulano", "teste@email.com", "")]
    [InlineData("Fulano", "teste@email.com", "invalidPassword")]
    public async Task InvalidCreateUserDto_SignUp_MustFailBecauseDtoIsInvalid(string name, string email, string password)
    {
        //Arrange
        var createUserDto = _fixture.GetCreateUserDto(name, email, password);
        
        //Act
        Func<Task> act = async () => await _service.SignUp(createUserDto, It.IsAny<IConfiguration>());
        
        //Assert
        await Assert.ThrowsAsync<ValidationException>(act);
        _userRepositoryMock.Verify(r => r.ExistsBy(It.IsAny<Expression<Func<User, bool>>>()), Times.Never);
        _userRepositoryMock.Verify(r => r.CreateAsync(It.IsAny<User>()), Times.Never);
    }

    [Fact]
    public async Task ValidCreateUserDto_SignUp_MustFailBecauseAlreadyExistsAccountWithEmail()
    {
        //Arrange
        var createUserDto = _fixture.GetCreateUserDto();
        
        _userRepositoryMock
            .Setup(r => r.ExistsBy(It.IsAny<Expression<Func<User, bool>>>()))
            .ReturnsAsync(true);
        
        //Act
        Func<Task> act = async () => await _service.SignUp(createUserDto, It.IsAny<IConfiguration>());

        //Assert
        await Assert.ThrowsAsync<DuplicatedEntityException>(act);
        _userRepositoryMock.Verify(r => r.ExistsBy(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
        _userRepositoryMock.Verify(r => r.CreateAsync(It.IsAny<User>()), Times.Never);
    }
}