using HubWebApp.Fakes;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using XTI_App.Extensions;
using XTI_Core;
using XTI_HubAppApi.Auth;
using XTI_HubAppApi.UserList;
using XTI_TempLog;
using XTI_WebApp.Abstractions;
using XTI_WebApp.Api;
using XTI_WebApp.Fakes;

namespace HubWebApp.Tests;

internal sealed class LoginTest
{
    [Test]
    public async Task ShouldRequireUserName()
    {
        var tester = await setup();
        var model = createLoginModel();
        model.Credentials.UserName = "";
        var ex = Assert.ThrowsAsync<ValidationFailedException>
        (
            () => tester.Execute(model)
        );
        Assert.That
        (
            ex?.Errors,
            Has.One.EqualTo(new ErrorModel(AuthErrors.UserNameIsRequired, "User Name", "UserName")),
            "Should require user name"
        );
    }

    [Test]
    public async Task ShouldRequirePassword()
    {
        var tester = await setup();
        var model = createLoginModel();
        model.Credentials.Password = "";
        var ex = Assert.ThrowsAsync<ValidationFailedException>
        (
            () => tester.Execute(model)
        );
        Assert.That
        (
            ex?.Errors,
            Has.One.EqualTo(new ErrorModel(AuthErrors.PasswordIsRequired, "Password", "Password")),
            "Should require password"
        );
    }

    [Test]
    public async Task ShouldRequireCorrectPassword()
    {
        var tester = await setup();
        var model = createLoginModel();
        model.Credentials.Password = "Incorrect";
        Assert.ThrowsAsync<PasswordIncorrectException>
        (
            () => tester.Execute(model)
        );
    }

    [Test]
    public async Task ShouldVerifyCorrectPassword()
    {
        var tester = await setup();
        var model = createLoginModel();
        var result = await tester.Execute(model);
        Assert.That(result.Url, Is.EqualTo("~/User"), "Should redirect to start if password is correct");
    }

    [Test]
    public async Task ShouldAuthenticateUser()
    {
        var tester = await setup();
        var model = createLoginModel();
        await tester.Execute(model);
        var access = tester.Services.GetRequiredService<FakeAccessForLogin>();
        Assert.That
        (
            access.Claims,
            Has.One.EqualTo
            (
                new Claim("UserName", new AppUserName(model.Credentials.UserName).Value)
            )
            .Using<Claim>((x, y) => x.Type == y.Type && x.Value == y.Value),
            "Should authenticate user"
        );
    }

    [Test]
    public async Task ShouldAuthenticateTempLogSession()
    {
        var tester = await setup();
        var model = createLoginModel();
        await tester.Execute(model);
        var tempLog = tester.Services.GetRequiredService<TempLog>();
        var clock = tester.Services.GetRequiredService<IClock>();
        var authSessionFiles = tempLog.AuthSessionFiles(clock.Now().AddMinutes(1)).ToArray();
        Assert.That(authSessionFiles.Length, Is.EqualTo(1), "Should authenticate session");
        var serializedAuthSession = await authSessionFiles[0].Read();
        var authSession = XtiSerializer.Deserialize<AuthenticateSessionModel>(serializedAuthSession);
        var appFactory = tester.Services.GetRequiredService<AppFactory>();
        var user = await appFactory.Users.UserByUserName(new AppUserName(model.Credentials.UserName));
        Assert.That(authSession.UserName, Is.EqualTo(user.UserName().Value), "Should authenticate session");
    }

    [Test]
    public async Task ShouldClearSessionForAnonUser()
    {
        var tester = await setup();
        var model = createLoginModel();
        await tester.Execute(model);
        var anonClient = tester.Services.GetRequiredService<IAnonClient>();
        Assert.That(anonClient.SessionKey, Is.EqualTo(""), "Should clear session for anon client after authenticating");
    }

    [Test]
    public async Task ShouldResetCache()
    {
        var tester = await setup();
        var model = createLoginModel();
        var appFactory = tester.Services.GetRequiredService<AppFactory>();
        var user = await appFactory.Users.UserByUserName(new AppUserName(model.Credentials.UserName));
        var httpContextAccessor = tester.Services.GetRequiredService<IHttpContextAccessor>();
        httpContextAccessor.HttpContext = new DefaultHttpContext
        {
            User = new FakeHttpUser().Create("", user)
        };
        await tester.Execute(model);
        var userContext = tester.Services.GetRequiredService<IUserContext>();
        var firstCachedUser = await userContext.User();
        await tester.Execute(model);
        var secondCachedUser = await userContext.User();
        Assert.That(ReferenceEquals(firstCachedUser, secondCachedUser), Is.False, "Should reset cache after login");
    }

    private async Task<HubActionTester<LoginModel, WebRedirectResult>> setup()
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
        var tester = HubActionTester.Create(services, hubApi => hubApi.Auth.Login);
        await addUser(tester, "xartogg", "Password12345");
        return tester;
    }

    private async Task<AppUser> addUser(IHubActionTester tester, string userName, string password)
    {
        var addUserTester = tester.Create(hubApi => hubApi.Users.AddOrUpdateUser);
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

    private LoginModel createLoginModel()
        => new LoginModel
        {
            Credentials = new LoginCredentials
            {
                UserName = "xartogg",
                Password = "Password12345"
            }
        };
}