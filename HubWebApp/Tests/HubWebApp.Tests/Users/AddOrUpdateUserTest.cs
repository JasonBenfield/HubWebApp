using Microsoft.EntityFrameworkCore;
using XTI_Core;
using XTI_HubDB.EF;

namespace HubWebApp.Tests;

internal sealed class AddOrUpdateUserTest
{
    [Test]
    public async Task ShouldThrowError_WhenModifierIsBlank()
    {
        var tester = await setup();
        await AccessAssertions.Create(tester).ShouldThrowError_WhenModifierIsBlank(createModel());
    }

    [Test]
    public async Task ShouldThrowError_WhenAccessIsDenied()
    {
        var tester = await setup();
        var modifier = await tester.GeneralUserGroupModifier();
        await AccessAssertions.Create(tester)
            .ShouldThrowError_WhenAccessIsDenied
            (
                createModel(),
                modifier,
                HubInfo.Roles.Admin,
                HubInfo.Roles.AddUser
            );
    }

    [Test]
    public async Task ShouldRequireUserName()
    {
        var tester = await setup();
        await tester.LoginAsAdmin();
        var model = createModel();
        model.UserName = "";
        var modifier = await tester.GeneralUserGroupModifier();
        var ex = Assert.ThrowsAsync<ValidationFailedException>
        (
            () => tester.Execute(model, modifier)
        );
        Assert.That
        (
            ex?.Errors,
            Has.One.EqualTo(new ErrorModel(UserErrors.UserNameIsRequired, "User Name", "UserName")),
            "Should require user name"
        );
    }

    [Test]
    public async Task ShouldRequirePassword()
    {
        var tester = await setup();
        await tester.LoginAsAdmin();
        var model = createModel();
        model.Password = "";
        var modifier = await tester.GeneralUserGroupModifier();
        var ex = Assert.ThrowsAsync<ValidationFailedException>
        (
            () => tester.Execute(model, modifier)
        );
        Assert.That
        (
            ex?.Errors,
            Has.One.EqualTo(new ErrorModel(UserErrors.PasswordIsRequired, "Password", "Password")),
            "Should require password"
        );
    }

    [Test]
    public async Task ShouldAddUser()
    {
        var tester = await setup();
        await tester.LoginAsAdmin();
        var model = createModel();
        var modifier = await tester.GeneralUserGroupModifier();
        await tester.Execute(model, modifier);
        var factory = tester.Services.GetRequiredService<HubFactory>();
        var user = await factory.Users.UserByUserName(new AppUserName(model.UserName));
        Assert.That(user.ToModel().UserName, Is.EqualTo(new AppUserName(model.UserName)), "Should add user with the given user name");
    }

    [Test]
    public async Task ShouldHashPassword()
    {
        var tester = await setup();
        await tester.LoginAsAdmin();
        var model = createModel();
        var modifier = await tester.GeneralUserGroupModifier();
        var userModel = await tester.Execute(model, modifier);
        var hubDbContext = tester.Services.GetRequiredService<HubDbContext>();
        var user = await hubDbContext.Users.Retrieve().FirstOrDefaultAsync(u => u.ID == userModel.ID);
        Assert.That(user?.Password, Is.EqualTo(new FakeHashedPassword(model.Password).Value()), "Should add user with the hashed password");
    }

    private async Task<HubActionTester<AddOrUpdateUserRequest, AppUserModel>> setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        return HubActionTester.Create(services, hubApi => hubApi.Users.AddOrUpdateUser);
    }

    private AddOrUpdateUserRequest createModel()
    {
        return new AddOrUpdateUserRequest
        {
            UserName = "test.user",
            Password = "Password12345;"
        };
    }
}