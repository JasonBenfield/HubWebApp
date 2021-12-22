using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_Hub;
using XTI_HubAppApi;
using XTI_HubAppApi.UserList;

namespace HubWebApp.Tests;

internal sealed class GetUsersTest
{
    [Test]
    public async Task ShouldGetUsers()
    {
        var tester = await setup();
        var userName = new AppUserName("Test.User");
        await addUser(tester, userName);
        var factory = tester.Services.GetRequiredService<AppFactory>();
        var users = (await factory.Users.Users()).ToArray();
        Assert.That(users.Select(u => u.UserName()), Has.One.EqualTo(userName), "Should get all users");
    }

    private async Task addUser(IHubActionTester tester, AppUserName userName)
    {
        var hubApiFactory = tester.Services.GetRequiredService<HubAppApiFactory>();
        var hubApi = hubApiFactory.CreateForSuperUser();
        await hubApi.Users.AddUser.Execute(new AddUserModel
        {
            UserName = userName.Value,
            Password = "Password123456"
        });
    }

    private async Task<HubActionTester<EmptyRequest, AppUserModel[]>> setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        return HubActionTester.Create(services, hubApi => hubApi.Users.GetUsers);
    }
}