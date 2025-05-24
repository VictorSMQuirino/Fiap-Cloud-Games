using AutoFixture;
using FIAP_CloudGames.Tests.Fixtures.Customizations;

namespace FIAP_CloudGames.Tests.Fixtures;

public class ServiceFixture
{
    protected readonly Fixture _fixture;

    public ServiceFixture()
    {
        _fixture = new Fixture();
        _fixture.Customize(new DateOnlyCustomization());
    }
}