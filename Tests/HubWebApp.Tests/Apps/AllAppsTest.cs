using NUnit.Framework;
using XTI_App.Api;
using XTI_Hub;

namespace HubWebApp.Tests;

internal sealed class AllAppsTest
{
    [Test]
    public async Task ShouldGetAllApps_WhenUserIsAdmin()
    {
        var tester = await setup();
        var adminUser = await tester.AdminUser();
        var apps = await tester.Execute(new EmptyRequest(), adminUser);
        var appNames = apps.Select(a => a.AppName);
        Assert.That
        (
            appNames,
            Is.EquivalentTo(new[] { HubInfo.AppKey.Name.DisplayText }),
            "Should get all apps"
        );
    }

    [Test]
    public async Task ShouldOnlyGetAllowedApps()
    {
        var tester = await setup();
        var adminUser = await tester.AdminUser();
        var app = await tester.HubApp();
        var adminRole = await tester.AdminRole();
        await adminUser.RemoveRole(adminRole);
        var hubAppModifier = await tester.HubAppModifier();
        await adminUser.AddRole(adminRole, hubAppModifier);
        var apps = await tester.Execute(new EmptyRequest(), adminUser);
        var appNames = apps.Select(a => a.AppName);
        Assert.That
        (
            appNames,
            Is.EquivalentTo(new[] { HubInfo.AppKey.Name.DisplayText }),
            "Should get only allowed apps"
        );
    }

    private async Task<HubActionTester<EmptyRequest, AppModel[]>> setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        return HubActionTester.Create(services, hubApi => hubApi.Apps.All);
    }
}