namespace HubWebApp.Tests;

internal sealed class GetUserGroupsTest
{
    [Test]
    public async Task ShouldGetUserGroups()
    {
        var tester = await Setup();
        await tester.LoginAsAdmin();
        var userGroups = await tester.Execute(new EmptyRequest());
        Assert.That
        (
            userGroups.Select(ug => ug.GroupName),
            Is.EquivalentTo(new[] { AppUserGroupName.XTI, AppUserGroupName.General }),
            "Should get user groups"
        );
    }

    [Test]
    public async Task ShouldGetUserGroupsForWhichUserHasViewPermission()
    {
        var tester = await Setup();
        var modifier = await tester.UserGroupModifier(AppUserGroupName.General);
        await tester.Login(modifier, HubInfo.Roles.ViewUser);
        var userGroups = await tester.Execute(new EmptyRequest());
        Assert.That
        (
            userGroups.Select(ug => ug.GroupName),
            Is.EquivalentTo(new[] { AppUserGroupName.General }),
            "Should only get user groups for which user has view permission"
        );
    }

    private async Task<HubActionTester<EmptyRequest, AppUserGroupModel[]>> Setup()
    {
        var host = new HubTestHost();
        var sp = await host.Setup();
        return HubActionTester.Create(sp, hubApi => hubApi.UserGroups.GetUserGroups);
    }
}
