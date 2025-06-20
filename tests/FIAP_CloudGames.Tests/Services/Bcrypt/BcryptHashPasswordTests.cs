using FIAP_CloudGames.Tests.Fixtures;
using FluentAssertions;

namespace FIAP_CloudGames.Tests.Services.Bcrypt;

[Collection(nameof(BcryptServiceCollection))]
public class BcryptHashPasswordTests : BcryptServiceTests
{
    [Fact]
    public Task ValidPassword_HashPassword_MustHashPassword()
    {
        //Arrange
        var password = _fixture.GetValidPassword();
        
        //Act
        var result = _service.HashPassword(password);

        //Assert
        var assert = _service.VerifyHashedPassword(result, password);
        assert.Should().BeTrue();
        
        return Task.CompletedTask;
    }
}