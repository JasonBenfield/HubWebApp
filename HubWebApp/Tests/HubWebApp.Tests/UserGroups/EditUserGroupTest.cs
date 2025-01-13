using XTI_Core;
using XTI_HubWebAppApiActions;

namespace HubWebApp.Tests;

internal sealed class EditUserGroupTest
{
    [Test]
    public async Task ShouldThrowError_WhenModifierIsBlank()
    {
        var sp = await Setup();
        var group1 = await AddUserGroup(sp, "Group1");
        var group2 = await AddUserGroup(sp, "Group2");
        var user = await AddUser(sp, group1);
        var tester = HubActionTester.Create(sp, api => api.AppUserMaintenance.EditUserGroup);
        await AccessAssertions.Create(tester)
            .ShouldThrowError_WhenModifierIsBlank(new(userID: user.ID, userGroupID: group2.ID));
    }

    [Test]
    public async Task ShouldThrowError_WhenAccessIsDeniedToSourceGroup()
    {
        var sp = await Setup();
        var tester = HubActionTester.Create(sp, api => api.AppUserMaintenance.EditUserGroup);
        var group1 = await AddUserGroup(sp, "Group1");
        var group2 = await AddUserGroup(sp, "Group2");
        var group1Modifier = await tester.UserGroupModifier(group1.GroupName);
        var userIndex = 1;
        await AccessAssertions.Create(tester)
            .ShouldThrowError_WhenAccessIsDenied
            (
                async () =>
                {
                    await tester.LoginAsAdmin();
                    var user = await AddUser(sp, group1, new AppUserName($"test.user{userIndex}"));
                    var editRequest = new EditUserGroupRequest
                    (
                        userID: user.ID,
                        userGroupID: group2.ID
                    );
                    userIndex++;
                    return editRequest;
                },
                group1Modifier,
                HubInfo.Roles.Admin,
                HubInfo.Roles.EditUser
            );
    }

    [Test]
    public async Task ShouldThrowError_WhenAccessIsDeniedToDestinationGroup()
    {
        var sp = await Setup();
        var tester = HubActionTester.Create(sp, api => api.AppUserMaintenance.EditUserGroup);
        var group1 = await AddUserGroup(sp, "Group1");
        var group2 = await AddUserGroup(sp, "Group2");
        var user = await AddUser(sp, group1);
        var group1Modifier = await tester.UserGroupModifier(group1.GroupName);
        var loggedInUser = await tester.Login(group1Modifier, HubInfo.Roles.EditUser);
        Assert.ThrowsAsync<AccessDeniedException>
        (
            () => EditUserGroup(sp, group1, user, group2)
        );
    }

    [Test]
    public async Task ShouldRequireUserID()
    {
        var sp = await Setup();
        var group1 = await AddUserGroup(sp, "Group1");
        var group2 = await AddUserGroup(sp, "Group2");
        var user = await AddUser(sp, group1);
        var ex = Assert.ThrowsAsync<ValidationFailedException>(() => EditUserGroup(sp, group1, new AppUserModel(), group2));
        Assert.That
        (
            ex?.Errors,
            Has.One.EqualTo(new ErrorModel(UserErrors.UserIDIsRequired)),
            "Should require user ID"
        );
    }

    [Test]
    public async Task ShouldRequireUserGroupID()
    {
        var sp = await Setup();
        var group1 = await AddUserGroup(sp, "Group1");
        var group2 = await AddUserGroup(sp, "Group2");
        var user = await AddUser(sp, group1);
        var ex = Assert.ThrowsAsync<ValidationFailedException>(() => EditUserGroup(sp, group1, user, new()));
        Assert.That
        (
            ex?.Errors,
            Has.One.EqualTo(new ErrorModel(UserErrors.UserGroupIDIsRequired)),
            "Should require user group ID"
        );
    }

    [Test]
    public async Task ShouldEditUserGroup()
    {
        var sp = await Setup();
        var group1 = await AddUserGroup(sp, "Group1");
        var group2 = await AddUserGroup(sp, "Group2");
        var user = await AddUser(sp, group1);
        await EditUserGroup(sp, group1, user, group2);
        var movedUser = await GetUser(sp, group2, user);
        Assert.That(movedUser.UserName, Is.EqualTo(user.UserName), "Should edit user group");
    }

    private Task<IServiceProvider> Setup()
    {
        var host = new HubTestHost();
        return host.Setup();
    }

    private Task<AppUserGroupModel> AddUserGroup(IServiceProvider sp, string groupName)
    {
        var tester = HubActionTester.Create(sp, api => api.UserGroups.AddUserGroupIfNotExists);
        return tester.Execute(new(groupName: groupName));
    }

    private Task<AppUserModel> AddUser(IServiceProvider sp, AppUserGroupModel userGroup) =>
        AddUser(sp, userGroup, new AppUserName("test.user"));

    private Task<AppUserModel> AddUser(IServiceProvider sp, AppUserGroupModel userGroup, AppUserName userName)
    {
        var tester = HubActionTester.Create(sp, api => api.Users.AddOrUpdateUser);
        return tester.Execute
        (
            new
            (
                userName: userName,
                password: "Pwd,123456",
                personName: new PersonName("Test User")
            ),
            userGroup.PublicKey
        );
    }

    private Task EditUserGroup(IServiceProvider sp, AppUserGroupModel sourceGroup, AppUserModel user, AppUserGroupModel destinationGroup)
    {
        var tester = HubActionTester.Create(sp, api => api.AppUserMaintenance.EditUserGroup);
        return tester.Execute(new(userID: user.ID, userGroupID: destinationGroup.ID), sourceGroup.PublicKey);
    }

    private Task<AppUserModel> GetUser(IServiceProvider sp, AppUserGroupModel group, AppUserModel user)
    {
        var tester = HubActionTester.Create(sp, api => api.UserInquiry.GetUserOrAnon);
        return tester.Execute(new(userName: user.UserName), group.PublicKey);
    }

}
