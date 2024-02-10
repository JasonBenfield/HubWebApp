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
        var modCategory = await GetModCategory(tester, app.App, ModifierCategoryName.Default);
        var modifier = await GetModifier(tester, app.App, modCategory, ModifierKey.Default);
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
        var modCategory = await GetModCategory(tester, app.App, HubInfo.ModCategories.UserGroups);
        var modifier = await GetModifier(tester, app.App, modCategory, new ModifierKey("General"));
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

    private static async Task<ModifierCategoryModel> GetModCategory(IHubActionTester sourceTester, AppModel app, ModifierCategoryName modCategoryName)
    {
        var tester = sourceTester.Create(api => api.App.GetModifierCategories);
        await tester.LoginAsAdmin();
        var modCategories = await tester.Execute(new EmptyRequest(), app.PublicKey);
        return modCategories.FirstOrDefault(mc => mc.Name.Equals(modCategoryName)) ?? new();
    }

    private static async Task<ModifierModel> GetModifier(IHubActionTester sourceTester, AppModel app, ModifierCategoryModel modCategory, ModifierKey modKey)
    {
        var tester = sourceTester.Create(api => api.ModCategory.GetModifiers);
        await tester.LoginAsAdmin();
        var modifiers = await tester.Execute(modCategory.ID, app.PublicKey);
        return modifiers.FirstOrDefault(m => m.ModKey.Equals(modKey)) ?? new();
    }

    private async Task<AppRoleModel[]> GetUserRoles(IHubActionTester tester, ModifierModel userModifier, AppUserModel user)
    {
        var getRolesTester = tester.Create(hubApi => hubApi.AppUser.GetExplicitUserAccess);
        await getRolesTester.LoginAsAdmin();
        var modifier = await tester.GeneralUserGroupModifier();
        var userAccess = await getRolesTester.Execute
        (
            new UserModifierKey
            (
                modifierID: userModifier.ID,
                userID: user.ID
            ),
            modifier
        );
        return userAccess.AssignedRoles;
    }
}
