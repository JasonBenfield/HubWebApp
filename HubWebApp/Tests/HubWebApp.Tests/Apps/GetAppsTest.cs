namespace HubWebApp.Tests;

internal sealed class GetAppsTest
{
    [Test]
    public async Task ShouldGetAllApps_WhenUserIsAdmin()
    {
        var tester = await setup();
        await tester.LoginAsAdmin();
        var apps = await tester.Execute(new EmptyRequest());
        var appNames = apps.Select(a => a.AppKey);
        Assert.That
        (
            appNames,
            Is.EquivalentTo(new[] { HubInfo.AppKey }),
            "Should get all apps"
        );
    }

    [Test]
    public async Task ShouldOnlyGetAllowedApps()
    {
        var tester = await setup();
        var hubAppModifier = await tester.HubAppModifier();
        await tester.Login(hubAppModifier, HubInfo.Roles.ViewApp);
        var apps = await tester.Execute(new EmptyRequest());
        var appNames = apps.Select(a => a.AppKey);
        Assert.That
        (
            appNames,
            Is.EquivalentTo(new[] { HubInfo.AppKey }),
            "Should get only allowed apps"
        );
    }

    private async Task<HubActionTester<EmptyRequest, AppModel[]>> setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        return HubActionTester.Create(services, hubApi => hubApi.Apps.GetApps);
    }
}