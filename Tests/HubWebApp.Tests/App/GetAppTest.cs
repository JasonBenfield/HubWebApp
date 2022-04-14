using XTI_Hub.Abstractions;

namespace HubWebApp.Tests;

internal sealed class GetAppTest
{
    [Test]
    public async Task ShouldThrowError_WhenModifierIsBlank()
    {
        var tester = await setup();
        AccessAssertions.Create(tester).ShouldThrowError_WhenModifierIsBlank(new EmptyRequest());
    }

    [Test]
    public async Task ShouldThrowError_WhenAccessIsDenied()
    {
        var tester = await setup();
        var modifier = tester.FakeHubAppModifier();
        AccessAssertions.Create(tester)
            .ShouldThrowError_WhenAccessIsDenied
            (
                new EmptyRequest(),
                modifier,
                HubInfo.Roles.Admin,
                HubInfo.Roles.ViewApp
            );
    }

    [Test]
    public async Task ShouldGetApp()
    {
        var tester = await setup();
        tester.LoginAsAdmin();
        var hubAppModifier = await tester.HubAppModifier();
        var app = await tester.Execute(new EmptyRequest(), hubAppModifier.ModKey());
        Assert.That(app?.Title, Is.EqualTo("Hub"), "Should get app");
    }

    private async Task<HubActionTester<EmptyRequest, AppModel>> setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        return HubActionTester.Create(services, hubApi => hubApi.App.GetApp);
    }
}