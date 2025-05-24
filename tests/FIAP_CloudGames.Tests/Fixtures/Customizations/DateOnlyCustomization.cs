using AutoFixture;

namespace FIAP_CloudGames.Tests.Fixtures.Customizations;

public class DateOnlyCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize<DateOnly>(composer =>
            composer.FromFactory<DateTime>(DateOnly.FromDateTime)
            );
    }
}