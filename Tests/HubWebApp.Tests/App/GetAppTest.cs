using NUnit.Framework;
using XTI_App.Api;
using XTI_Hub;

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
    public async Task ShouldThrowError_WhenModifierIsNotAssignedToUser()
    {
        var tester = await setup();
        var modifier = await tester.FakeHubAppModifier();
        AccessAssertions.Create(tester)
            .ShouldThrowError_WhenModifierIsNotAssignedToUser_ButRoleIsAssignedToUser
            (
                new EmptyRequest(),
                HubInfo.Roles.ViewApp,
                modifier
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