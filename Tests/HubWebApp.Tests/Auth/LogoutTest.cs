using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_App.Extensions;
using XTI_Core;
using XTI_Hub;
using XTI_HubAppApi.UserList;
using XTI_TempLog;
using XTI_WebApp.Api;

namespace HubWebApp.Tests;

internal sealed class LogoutTest
{
    [Test]
    public async Task ShouldEndSession()
    {
        var tester = await setup();
        await tester.Execute(new EmptyRequest());
        var tempLog = tester.Services.GetRequiredService<TempLog>();
        var clock = tester.Services.GetRequiredService<IClock>();
        var endSessionFiles = tempLog.EndSessionFiles(clock.Now().AddMinutes(1)).ToArray();
        Assert.That
        (
            endSessionFiles.Length,
            Is.EqualTo(1),
            "Should end session"
        );
    }

    [Test]
    public async Task ShouldRedirectToLogin()
    {
        var tester = await setup();
        var result = await tester.Execute(new EmptyRequest());
        Assert.That
        (
            result.Url,
            Is.EqualTo("/Hub/Current/Auth"),
            "Should redirect to login"
        );
    }

    private async Task<HubActionTester<EmptyRequest, WebRedirectResult>> setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup
        (
            (hc, s) =>
            {
                s.AddScoped<IAppContext>(sp => sp.GetRequiredService<CachedAppContext>());
                s.AddScoped<IUserContext>(sp => sp.GetRequiredService<CachedUserContext>());
            }
        );
        var tester = HubActionTester.Create(services, hubApi => hubApi.Auth.Logout);
        await addUser(tester, "test.user", "Password12345");
        var tempLogSession = services.GetRequiredService<TempLogSession>();
        await tempLogSession.StartSession();
        return tester;
    }

    private async Task<AppUser> addUser(IHubActionTester tester, string userName, string password)
    {
        var addUserTester = tester.Create(hubApi => hubApi.Users.AddUser);
        addUserTester.LoginAsAdmin();
        var userID = await addUserTester.Execute(new AddUserModel
        {
            UserName = userName,
            Password = password
        });
        var factory = tester.Services.GetRequiredService<AppFactory>();
        var user = await factory.Users.UserByUserName(new AppUserName(userName));
        return user;
    }
}