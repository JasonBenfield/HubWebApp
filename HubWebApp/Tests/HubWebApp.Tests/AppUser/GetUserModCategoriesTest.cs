using XTI_HubWebAppApi.AppUserInquiry;
using XTI_HubWebAppApi.UserList;

namespace HubWebApp.Tests;

internal sealed class GetUserModCategoriesTest
{
    [Test]
    public async Task ShouldThrowError_WhenModifierIsBlank()
    {
        var tester = await setup();
        var user = await addUser(tester, "some.user");
        await AccessAssertions.Create(tester).ShouldThrowError_WhenModifierIsBlank(user.ToModel().ID);
    }

    [Test]
    public async Task ShouldGetUserModifiers_WhenUserDoesNotHaveAccessToAllModifiers()
    {
        var tester = await setup();
        await tester.Login(HubInfo.Roles.ViewUser);
        var user = await addUser(tester, "some.user");
        var adminRole = await tester.AdminRole();
        var hubAppModifier = await tester.HubAppModifier();
        await user.Modifier(hubAppModifier).AssignRole(adminRole);
        var modCategories = await tester.Execute(user.ToModel().ID, hubAppModifier.ModKey());
        Assert.That(modCategories[0].Modifiers.Length, Is.EqualTo(1), "Should have access to one modifier");
        Assert.That(modCategories[0].Modifiers[0].ModKey, Is.EqualTo(hubAppModifier.ModKey()), "Should have access to one modifier");
    }

    private async Task<HubActionTester<int, UserModifierCategoryModel[]>> setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        return HubActionTester.Create(services, hubApi => hubApi.AppUser.GetUserModCategories);
    }

    private async Task<AppUser> addUser(IHubActionTester tester, string userName)
    {
        var addUserTester = tester.Create(hubApi => hubApi.Users.AddOrUpdateUser);
        await addUserTester.LoginAsAdmin();
        var userID = await addUserTester.Execute(new AddUserModel
        {
            UserName = userName,
            Password = "Password12345"
        });
        var factory = tester.Services.GetRequiredService<HubFactory>();
        var user = await factory.Users.UserByUserName(new AppUserName(userName));
        return user;
    }
}