namespace HubWebApp.Tests;

internal sealed class GetAppTest
{
    [Test]
    public async Task ShouldThrowError_WhenModifierIsBlank()
    {
        var tester = await Setup();
        await AccessAssertions.Create(tester).ShouldThrowError_WhenModifierIsBlank(new EmptyRequest());
    }

    [Test]
    public async Task ShouldThrowError_WhenAccessIsDenied()
    {
        var tester = await Setup();
        var modifier = await tester.HubAppModifier();
        await AccessAssertions.Create(tester)
            .ShouldThrowError_WhenAccessIsDenied
            (
                new EmptyRequest(),
                modifier,
                HubInfo.Roles.Admin,
                HubInfo.Roles.ViewApp,
                HubInfo.Roles.EditApp
            );
    }

    [Test]
    public async Task ShouldGetApp()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var hubAppModifier = await tester.HubAppModifier();
        var app = await tester.Execute(new EmptyRequest(), hubAppModifier.ModKey);
        Assert.That(app?.AppKey, Is.EqualTo(HubInfo.AppKey), "Should get app");
    }

    private async Task<HubActionTester<EmptyRequest, AppModel>> Setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        return HubActionTester.Create(services, hubApi => hubApi.App.GetApp);
    }
}