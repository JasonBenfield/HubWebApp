namespace HubWebApp.Tests;

internal sealed class SetUserAccessTest
{
    [Test]
    public async Task ShouldSetUserAccessForDefaultModifier()
    {
        var tester = await Setup();
        var userModel = await AddUser(tester, "User1");
        await tester.Execute
        (
            new SetUserAccessRequest
            (
                userModel.UserName,
                new SetUserAccessRoleRequest(HubInfo.AppKey, HubInfo.Roles.ViewUser),
                new SetUserAccessRoleRequest(HubInfo.AppKey, HubInfo.Roles.ViewApp)
            )
        );
        var appContext = tester.Services.GetRequiredService<IAppContext>();
        var app = await appContext.App();
        var modCategory = app.ModifierCategories.First(mc => mc.ModifierCategory.Name.Equals(ModifierCategoryName.Default));
        var modifier = modCategory.Modifiers.First(m => m.ModKey.Equals(ModifierKey.Default));
        var roles = await GetUserRoles(tester, modifier, userModel);
        Assert.That
        (
            roles.Select(r => r.Name).ToArray(),
            Is.EquivalentTo(new[] { HubInfo.Roles.ViewUser, HubInfo.Roles.ViewApp })
        );
    }

    [Test]
    public async Task ShouldSetUserAccessForModifier()
    {
        var tester = await Setup();
        var userModel = await AddUser(tester, "User1");
        await tester.Execute
        (
            new SetUserAccessRequest
            (
                userModel.UserName,
                new SetUserAccessRoleRequest
                (
                    HubInfo.AppKey, 
                    HubInfo.ModCategories.UserGroups,
                    new ModifierKey("General"),
                    HubInfo.Roles.EditUser
                )
            )
        );
        var appContext = tester.Services.GetRequiredService<IAppContext>();
        var app = await appContext.App();
        var modCategory = app.ModifierCategories
            .First(mc => mc.ModifierCategory.Name.Equals(HubInfo.ModCategories.UserGroups));
        var modifier = modCategory.Modifiers.First(m => m.ModKey.Equals(new ModifierKey("General")));
        var roles = await GetUserRoles(tester, modifier, userModel);
        Assert.That
        (
            roles.Select(r => r.Name).ToArray(),
            Is.EquivalentTo(new[] { HubInfo.Roles.EditUser })
        );
    }

    private async Task<HubActionTester<SetUserAccessRequest, EmptyActionResult>> Setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        return HubActionTester.Create(services, hubApi => hubApi.Install.SetUserAccess);
    }

    private async Task<AppUserModel> AddUser(IHubActionTester tester, string userName)
    {
        var addUserTester = tester.Create(hubApi => hubApi.Users.AddOrUpdateUser);
        await addUserTester.LoginAsAdmin();
        var modifier = await tester.GeneralUserGroupModifier();
        var userModel = await addUserTester.Execute
        (
            new AddOrUpdateUserRequest
            {
                UserName = userName,
                Password = "Password12345"
            },
            modifier
        );
        return userModel;
    }

    private async Task<AppRoleModel[]> GetUserRoles(IHubActionTester tester, ModifierModel userModifier, AppUserModel user)
    {
        var getRolesTester = tester.Create(hubApi => hubApi.AppUser.GetUserAccess);
        await getRolesTester.LoginAsAdmin();
        var modifier = await tester.GeneralUserGroupModifier();
        var userAccess = await getRolesTester.Execute
        (
            new UserModifierKey
            {
                ModifierID = userModifier.ID,
                UserID = user.ID
            },
            modifier
        );
        return userAccess.AssignedRoles;
    }
}
