using Microsoft.EntityFrameworkCore;
using XTI_Core;
using XTI_HubDB.EF;
using XTI_HubWebAppApi.UserList;

namespace HubWebApp.Tests;

internal sealed class AddUserTest
{
    [Test]
    public async Task ShouldRequireUserName()
    {
        var tester = await setup();
        await tester.LoginAsAdmin();
        var model = createModel();
        model.UserName = "";
        var ex = Assert.ThrowsAsync<ValidationFailedException>
        (
            () => tester.Execute(model)
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
        var ex = Assert.ThrowsAsync<ValidationFailedException>
        (
            () => tester.Execute(model)
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
        await tester.Execute(model);
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
        var userID = await tester.Execute(model);
        var hubDbContext = tester.Services.GetRequiredService<HubDbContext>();
        var user = await hubDbContext.Users.Retrieve().FirstOrDefaultAsync(u => u.ID == userID);
        Assert.That(user?.Password, Is.EqualTo(new FakeHashedPassword(model.Password).Value()), "Should add user with the hashed password");
    }

    private async Task<HubActionTester<AddUserModel, int>> setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        return HubActionTester.Create(services, hubApi => hubApi.Users.AddOrUpdateUser);
    }

    private AddUserModel createModel()
    {
        return new AddUserModel
        {
            UserName = "test.user",
            Password = "Password12345;"
        };
    }
}