using AutoFixture;
using FIAP_CloudGames.Application.Auth;
using FIAP_CloudGames.Domain.Entities;
using FIAP_CloudGames.Domain.Interfaces.Services;
using FIAP_CloudGames.Application.Services;
using Microsoft.Extensions.Options;

namespace FIAP_CloudGames.Tests.Fixtures;

public class TokenServiceFixture : ServiceFixture
{
    public ITokenService GetService(JwtSettings jwtSettingsMock)
    {
        var options = Options.Create(jwtSettingsMock);
        
        return new TokenService(options);
    }

    internal JwtSettings GetJwtSettings() 
        => new()
        {
            Key = "EE86CB89-79FF-47B9-9686-269964C73CBD",
            Issuer = "Issuer_test",
            Audience = "Audience_test",
            ExpireMinutes = 30
        };

    internal User GetValidUser()
    {
        return _fixture.Build<User>()
            .With(u => u.UserGames, [])
            .Create();
    }
}