namespace HubWebApp.Tests;

internal sealed class GetAppsTest
{
    [Test]
    public async Task ShouldGetAllApps_WhenUserIsAdmin()
    {
        var tester = await setup();
        tester.LoginAsAdmin();
        var apps = await tester.Execute(new EmptyRequest());
        var appNames = apps.Select(a => a.App.AppKey);
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
        var adminUser = tester.LoginAsAdmin();
        var app = tester.HubApp();
        adminUser.RemoveRole(HubInfo.Roles.Admin);
        var hubAppModifier = await tester.HubAppModifier();
        adminUser.AddRole(tester.FakeHubAppModifier(), HubInfo.Roles.Admin);
        var apps = await tester.Execute(new EmptyRequest());
        var appNames = apps.Select(a => a.App.AppKey);
        Assert.That
        (
            appNames,
            Is.EquivalentTo(new[] { HubInfo.AppKey }),
            "Should get only allowed apps"
        );
    }

    private async Task<HubActionTester<EmptyRequest, AppWithModKeyModel[]>> setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        return HubActionTester.Create(services, hubApi => hubApi.Apps.GetApps);
    }
}