using FIAP_CloudGames.Tests.Fixtures;
using FluentAssertions;

namespace FIAP_CloudGames.Tests.Services.Bcrypt;

[Collection(nameof(BcryptServiceCollection))]
public class BcryptVerifyHashedPasswordTests : BcryptServiceTests
{
    [Fact]
    public Task ValidProvidedPassword_VerifyHashedPassword_MustReturnTrue()
    {
        //Arrange
        var password = _fixture.GetValidPassword();
        var hashedPassword = _service.HashPassword(password);

        //Act
        var result = _service.VerifyHashedPassword(hashedPassword, password);

        //Assert
        result.Should().BeTrue();
        
        return Task.CompletedTask;
    }

    [Fact]
    public Task InvalidProvidedPassword_VerifyHashedPassword_MustReturnFalse()
    {
        //Arrange
        var password = _fixture.GetValidPassword();
        const string invalidPassword = "InvalidPassword@123";
        var hashedPassword = _service.HashPassword(password);
        
        //Act
        var result = _service.VerifyHashedPassword(hashedPassword, invalidPassword);

        //Assert
        result.Should().BeFalse();

        return Task.CompletedTask;
    }
}