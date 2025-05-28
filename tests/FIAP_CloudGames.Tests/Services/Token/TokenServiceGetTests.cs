using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FluentAssertions;
using Microsoft.IdentityModel.Tokens;

namespace FIAP_CloudGames.Tests.Services.Token;

public class TokenServiceGetTests : TokenServiceTests
{
    [Fact]
    public async Task ValidLogin_GenerateToken_MustGenerateAValidToken()
    {
        //Arrange
        var user = _fixture.GetValidUser();
        var handler = new JwtSecurityTokenHandler();
        
        //Act
        var token = _service.GenerateToken(user);
        var jwt = handler.ReadJwtToken(token);

        //Assert
        token.Should().NotBeNullOrWhiteSpace();
        jwt.Claims.Should().Contain(c =>
            c.Type == JwtRegisteredClaimNames.Sub && c.Value == user.Id.ToString());
        jwt.Claims.Should().Contain(c =>
            c.Type == ClaimTypes.NameIdentifier && c.Value == user.Id.ToString());
        jwt.Claims.Should().Contain(c => 
            c.Type == ClaimTypes.Name && c.Value == user.Name);
        jwt.Claims.Should().Contain(c => 
            c.Type == ClaimTypes.Role && c.Value == user.Role.ToString());
        jwt.ValidTo.Should()
            .BeCloseTo(DateTime.UtcNow.AddMinutes(_jwtSettings.ExpireMinutes), precision: TimeSpan.FromSeconds(5));
        jwt.Header.Alg.Should().Be(SecurityAlgorithms.HmacSha256);
    }
}