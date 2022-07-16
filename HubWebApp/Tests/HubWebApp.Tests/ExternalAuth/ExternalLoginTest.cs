﻿using HubWebApp.Fakes;
using System.Security.Claims;
using XTI_Hub.Abstractions;
using XTI_HubWebAppApi.Auth;
using XTI_HubWebAppApi.ExternalAuth;
using XTI_HubWebAppApi.UserList;
using XTI_WebApp.Api;

namespace HubWebApp.Tests;

internal sealed class ExternalLoginTest
{
    private static readonly AppKey authAppKey = AppKey.WebApp("Auth");

    [Test]
    public async Task ShouldAuthenticateUser()
    {
        var tester = await setup();
        var user = await addUser(tester, "someone");
        var authApp = await getAuthApp(tester);
        const string externalUserKey = "external.id";
        await user.AddAuthenticator(authApp, externalUserKey);
        var authKey = await externalAuthKey(tester, authApp.Key(), externalUserKey);
        var returnKey = await loginReturnKey(tester, "/Home");
        var request = new LoginModel
        {
            AuthKey = authKey,
            ReturnKey = returnKey
        };
        await tester.Execute(request, authApp.ToAppModel().PublicKey);
        var access = tester.Services.GetRequiredService<FakeAccessForLogin>();
        Assert.That
        (
            access.Claims,
            Has.One.EqualTo
            (
                new Claim("UserName", user.ToModel().UserName.Value)
            )
            .Using<Claim>((x, y) => x.Type == y.Type && x.Value == y.Value),
            "Should authenticate user"
        );
    }

    private async Task<HubActionTester<LoginModel, WebRedirectResult>> setup()
    {
        var host = new HubTestHost();
        var services = await host.Setup();
        var tester = HubActionTester.Create
        (
            services,
            hubApi => hubApi.Auth.Login
        );
        var factory = tester.Services.GetRequiredService<HubFactory>();
        var appKey = AppKey.WebApp("Auth");
        var authApp = await factory.Apps.AddOrUpdate(new AppVersionName("auth"), appKey, DateTimeOffset.Now);
        await authApp.RegisterAsAuthenticator();
        var hubApp = await tester.HubApp();
        var modCategory = await hubApp.ModCategory(HubInfo.ModCategories.Apps);
        var modifier = await modCategory.AddOrUpdateModifier
        (
            authApp.ToAppModel().PublicKey,
            authApp.ID, 
            authApp.Key().Name.DisplayText
        );
        return tester;
    }

    private Task<App> getAuthApp(IHubActionTester tester)
    {
        var factory = tester.Services.GetRequiredService<HubFactory>();
        return factory.Apps.App(authAppKey);
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

    private Task<string> externalAuthKey(IHubActionTester tester, AppKey appKey, string externalUserKey)
    {
        var externalAuthKeyTester = tester.Create(hubApi => hubApi.ExternalAuth.ExternalAuthKey);
        externalAuthKeyTester.LoginAsAdmin();
        return externalAuthKeyTester.Execute(new ExternalAuthKeyModel
        {
            AppKey = appKey,
            ExternalUserKey = externalUserKey
        });
    }

    private async Task<string> loginReturnKey(IHubActionTester tester, string returnUrl)
    {
        var loginReturnKeyTester = tester.Create(hubApi => hubApi.Auth.LoginReturnKey);
        await loginReturnKeyTester.LoginAsAdmin();
        var result = await loginReturnKeyTester.Execute(new LoginReturnModel
        {
            ReturnUrl = returnUrl
        });
        return result;
    }
}