using Microsoft.EntityFrameworkCore;
using XTI_HubDB.Entities;

namespace HubWebApp.Tests;

internal sealed class AddUserGroupIfNotExistsTest
{
    [Test]
    public async Task ShouldThrowError_WhenAccessIsDenied()
    {
        var tester = await Setup();
        var request = new AddUserGroupIfNotExistsRequest
        {
            GroupName = "Some Group"
        };
        await AccessAssertions.Create(tester)
            .ShouldThrowError_WhenAccessIsDenied
            (
                request,
                HubInfo.Roles.Admin,
                HubInfo.Roles.AddUserGroup
            );
    }

    [Test]
    public async Task ShouldAddUserGroup()
    {
        var tester = await Setup();
        var addRequest = new AddUserGroupIfNotExistsRequest
        {
            GroupName = "Some Group"
        };
        await tester.Execute(addRequest);
        var db = tester.Services.GetRequiredService<IHubDbContext>();
        var userGroups = await db.UserGroups.Retrieve().ToArrayAsync();
        var userGroup = userGroups.FirstOrDefault
        (
            ug => new AppUserGroupName(ug.GroupName).Equals(new AppUserGroupName("Some Group"))
        );
        Assert.That(userGroup, Is.Not.Null, "Should add user group");
    }

    [Test]
    public async Task ShouldNotAddDuplicateUserGroupWithTheSameName()
    {
        var tester = await Setup();
        var addRequest = new AddUserGroupIfNotExistsRequest
        {
            GroupName = "Some Group"
        };
        await tester.Execute(addRequest);
        await tester.Execute(addRequest);
        var db = tester.Services.GetRequiredService<IHubDbContext>();
        var userGroups = await db.UserGroups.Retrieve().ToArrayAsync();
        var userGroupName = new AppUserGroupName("Some Group");
        Assert.That
        (
            userGroups.Select(ug => ug.GroupName),
            Has.One.EqualTo(userGroupName),
            "Should not add duplicate user group with the same name"
        );
    }

    [Test]
    public async Task ShouldAddUserGroupsWithDifferentNames()
    {
        var tester = await Setup();
        await tester.Execute
        (
            new AddUserGroupIfNotExistsRequest
            {
                GroupName = "Some Group"
            }
        );
        await tester.Execute
        (
            new AddUserGroupIfNotExistsRequest
            {
                GroupName = "Some Other Group"
            }
        );
        var db = tester.Services.GetRequiredService<IHubDbContext>();
        var userGroups = await db.UserGroups.Retrieve().ToArrayAsync();
        Assert.That
        (
            userGroups.Select(ug => ug.GroupName),
            Has.One.EqualTo(new AppUserGroupName("Some Group")),
            "Should add different user groups"
        );
        Assert.That
        (
            userGroups.Select(ug => ug.GroupName),
            Has.One.EqualTo(new AppUserGroupName("Some Other Group")),
            "Should add different user groups"
        );
    }

    [Test]
    public async Task ShouldAddModifierForUserGroup()
    {
        var tester = await Setup();
        var addRequest = new AddUserGroupIfNotExistsRequest
        {
            GroupName = "Some Group"
        };
        await tester.Execute(addRequest);
        var hubApp = await tester.HubApp();
        var userGroupsModCategory = await hubApp.ModCategory(HubInfo.ModCategories.UserGroups);
        var modKey = new ModifierKey("Some Group");
        var modifier = await userGroupsModCategory.ModifierByModKeyOrDefault(modKey);
        Assert.That(modifier.ToModel().ModKey, Is.EqualTo(modKey), "Should add modifier for user group");
    }

    private async Task<HubActionTester<AddUserGroupIfNotExistsRequest, AppUserGroupModel>> Setup()
    {
        var host = new HubTestHost();
        var sp = await host.Setup();
        return HubActionTester.Create(sp, hubApi => hubApi.UserGroups.AddUserGroupIfNotExists);
    }
}
