using HubWebApp.Fakes;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.Json;
using XTI_App.Extensions;
using XTI_Core;
using XTI_Hub.Abstractions;
using XTI_HubDB.Entities;
using XTI_HubWebAppApi.Auth;
using XTI_HubWebAppApi.Storage;
using XTI_HubWebAppApi.UserList;
using XTI_TempLog;
using XTI_TempLog.Abstractions;
using XTI_WebApp.Abstractions;
using XTI_WebApp.Api;
using XTI_WebApp.Fakes;

namespace HubWebApp.Tests;

internal sealed class LoginTest
{
    [Test]
    public async Task ShouldRequireUserName()
    {
        var tester = await Setup();
        var model = CreateLoginModel();
        model.UserName.SetValue("");
        var ex = Assert.ThrowsAsync<ValidationFailedException>
        (
            () => tester.Execute(model)
        );
        Assert.That
        (
            ex?.Errors.Select(err => err.Source),
            Is.EquivalentTo(new[] { "VerifyLoginForm_UserName" }),
            "Should require user name"
        );
    }

    [Test]
    public async Task ShouldRequirePassword()
    {
        var tester = await Setup();
        var model = CreateLoginModel();
        model.Password.SetValue("");
        var ex = Assert.ThrowsAsync<ValidationFailedException>
        (
            () => tester.Execute(model)
        );
        Assert.That
        (
            ex?.Errors.Select(err => err.Source),
            Is.EquivalentTo(new[] { "VerifyLoginForm_Password" }),
            "Should require password"
        );
    }

    [Test]
    public async Task ShouldRequireCorrectPassword()
    {
        var tester = await Setup();
        var model = CreateLoginModel();
        model.Password.SetValue("Incorrect");
        Assert.ThrowsAsync<PasswordIncorrectException>
        (
            () => tester.Execute(model)
        );
    }

    [Test]
    public async Task ShouldReturnAuthKey()
    {
        var tester = await Setup();
        var model = CreateLoginModel();
        var loginResult = await tester.Execute(model);
        var authenticated = await getAuthenticated(tester, loginResult.AuthKey);
        Assert.That(authenticated.UserName, Is.EqualTo(model.UserName.Value()), "Should return auth key");
    }

    private async Task<AuthenticatedModel> getAuthenticated(IHubActionTester tester, string authKey)
    {
        var getAuthKeyTester = tester.Create(hubApi => hubApi.Storage.GetStoredObject);
        var serialized = await getAuthKeyTester.Execute(new GetStoredObjectRequest
        {
            StorageName = "XTI Authenticated",
            StorageKey = authKey
        });
        return JsonSerializer.Deserialize<AuthenticatedModel>(serialized) ?? new AuthenticatedModel();
    }

    [Test]
    public async Task ShouldAuthenticateUser()
    {
        var tester = await Setup();
        var model = CreateLoginModel();
        var loginResult = await tester.Execute(model);
        var returnKey = await LoginReturnKey(tester, "./Home");
        await Login(tester, loginResult, returnKey);
        var access = tester.Services.GetRequiredService<FakeAccessForLogin>();
        Assert.That
        (
            access.Claims,
            Has.One.EqualTo
            (
                new Claim("UserName", new AppUserName(model.UserName.Value() ?? "").Value)
            )
            .Using<Claim>((x, y) => x.Type == y.Type && x.Value == y.Value),
            "Should authenticate user"
        );
    }

    [Test]
    public async Task ShouldAuthenticateTempLogSession()
    {
        var tester = await Setup();
        var model = CreateLoginModel();
        var loginResult = await tester.Execute(model);
        var returnKey = await LoginReturnKey(tester, "./Home");
        await Login(tester, loginResult, returnKey);
        var tempLog = tester.Services.GetRequiredService<TempLogV1>();
        var clock = tester.Services.GetRequiredService<IClock>();
        var authSessionFiles = tempLog.AuthSessionFiles(clock.Now().AddMinutes(1)).ToArray();
        Assert.That(authSessionFiles.Length, Is.EqualTo(1), "Should authenticate session");
        var serializedAuthSession = await authSessionFiles[0].Read();
        var authSession = XtiSerializer.Deserialize<AuthenticateSessionModel>(serializedAuthSession);
        var appFactory = tester.Services.GetRequiredService<HubFactory>();
        var user = await appFactory.Users.UserByUserName(new AppUserName(model.UserName.Value() ?? ""));
        Assert.That(authSession.UserName, Is.EqualTo(user.ToModel().UserName.Value), "Should authenticate session");
    }

    [Test]
    public async Task ShouldClearSessionForAnonUser()
    {
        var tester = await Setup();
        var model = CreateLoginModel();
        await tester.Execute(model);
        var anonClient = tester.Services.GetRequiredService<IAnonClient>();
        Assert.That(anonClient.SessionKey, Is.EqualTo(""), "Should clear session for anon client after authenticating");
    }

    [Test]
    public async Task ShouldResetCache()
    {
        var tester = await Setup();
        var returnKey = await LoginReturnKey(tester, "./Home");
        var currentUserName = tester.Services.GetRequiredService<FakeCurrentUserName>();
        currentUserName.SetUserName(AppUserName.Anon);
        var model = CreateLoginModel();
        var user = await tester.Services.GetRequiredService<ISourceUserContext>()
            .User(new AppUserName(model.UserName.Value() ?? ""));
        var httpContextAccessor = tester.Services.GetRequiredService<IHttpContextAccessor>();
        httpContextAccessor.HttpContext = new DefaultHttpContext
        {
            User = new FakeHttpUser().Create("", user)
        };
        var loginResult = await tester.Execute(model);
        await Login(tester, loginResult, returnKey);
        var userContext = tester.Services.GetRequiredService<IUserContext>();
        var firstCachedUser = await userContext.User();
        var db = tester.Services.GetRequiredService<IHubDbContext>();
        var userEntity = await db.Users.Retrieve().FirstAsync(u => u.ID == user.ID);
        await db.Users.Update(userEntity, u => u.Name = "Changed Name");
        loginResult = await tester.Execute(model);
        await Login(tester, loginResult, returnKey);
        var secondCachedUser = await userContext.User();
        Assert.That(secondCachedUser.Name, Is.EqualTo(new PersonName("Changed Name")), "Should reset cache after login");
    }

    private async Task<string> LoginReturnKey(IHubActionTester tester, string returnUrl)
    {
        var loginReturnKeyTester = tester.Create(hubApi => hubApi.Auth.LoginReturnKey);
        await loginReturnKeyTester.LoginAsAdmin();
        var result = await loginReturnKeyTester.Execute(new LoginReturnModel
        {
            ReturnUrl = returnUrl
        });
        return result;
    }

    private async Task Login(IHubActionTester tester, AuthenticatedLoginResult loginResult, string returnKey)
    {
        var loginTester = tester.Create(hubApi => hubApi.Auth.Login);
        await loginTester.Execute
        (
            new AuthenticatedLoginRequest
            (
                authKey: loginResult.AuthKey, 
                authID: loginResult.AuthID, 
                returnKey: returnKey
            )
        );
        var httpContextAccessor = tester.Services.GetRequiredService<IHttpContextAccessor>();
        var claims = new XtiClaims(httpContextAccessor.HttpContext!);
        var currentUserName = tester.Services.GetRequiredService<FakeCurrentUserName>();
        currentUserName.SetUserName(claims.UserName());
    }

    private async Task<HubActionTester<VerifyLoginForm, AuthenticatedLoginResult>> Setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup
        (
            (s) =>
            {
                s.AddScoped<IAppContext>(sp => sp.GetRequiredService<CachedAppContext>());
                s.AddScoped<IUserContext>(sp => sp.GetRequiredService<CachedUserContext>());
            }
        );
        var tester = HubActionTester.Create(services, hubApi => hubApi.Auth.VerifyLogin);
        await AddUser(tester, "xartogg", "Password12345");
        return tester;
    }

    private async Task<AppUserModel> AddUser(IHubActionTester tester, string userName, string password)
    {
        var addUserTester = tester.Create(hubApi => hubApi.Users.AddOrUpdateUser);
        await addUserTester.LoginAsAdmin();
        var modifier = await tester.GeneralUserGroupModifier();
        var user = await addUserTester.Execute
        (
            new AddOrUpdateUserRequest
            {
                UserName = userName,
                Password = password
            },
            modifier
        );
        return user;
    }

    private VerifyLoginForm CreateLoginModel()
    {
        var form = new VerifyLoginForm();
        form.UserName.SetValue("xartogg");
        form.Password.SetValue("Password12345");
        return form;
    }
}