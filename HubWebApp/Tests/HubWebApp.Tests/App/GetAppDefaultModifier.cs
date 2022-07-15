namespace HubWebApp.Tests;

sealed class GetAppDefaultModifier
{
    [Test]
    public async Task ShouldGetDefaultModifier()
    {
        var tester = await setup();
        await tester.LoginAsAdmin();
        var hubAppModifier = await tester.HubAppModifier();
        var defaultModifier = await tester.Execute(new EmptyRequest(), hubAppModifier);
        Assert.That(defaultModifier.ID, Is.GreaterThan(0));
        Assert.That(defaultModifier.ModKey, Is.EqualTo(""));
    }

    private async Task<HubActionTester<EmptyRequest, ModifierModel>> setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        return HubActionTester.Create(services, hubApi => hubApi.App.GetDefaultModifier);
    }
}