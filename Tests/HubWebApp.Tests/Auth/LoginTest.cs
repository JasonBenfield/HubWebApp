using HubWebApp.Fakes;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_App.Fakes;
using XTI_Core;
using XTI_Hub;
using XTI_HubAppApi;
using XTI_HubAppApi.Auth;
using XTI_TempLog;
using XTI_WebApp;
using XTI_WebApp.Api;
using XTI_WebApp.Fakes;

namespace HubWebApp.Tests
{
    public sealed class LoginTest
    {
        [Test]
        public async Task ShouldRequireUserName()
        {
            var services = await setup();
            var model = createLoginModel();
            model.Credentials.UserName = "";
            var ex = Assert.ThrowsAsync<ValidationFailedException>
            (
                () => execute(services, model)
            );
            Assert.That
            (
                ex.Errors,
                Has.One.EqualTo(new ErrorModel(AuthErrors.UserNameIsRequired, "User Name", "UserName")),
                "Should require user name"
            );
        }

        [Test]
        public async Task ShouldRequirePassword()
        {
            var services = await setup();
            var model = createLoginModel();
            model.Credentials.Password = "";
            var ex = Assert.ThrowsAsync<ValidationFailedException>
            (
                () => execute(services, model)
            );
            Assert.That
            (
                ex.Errors,
                Has.One.EqualTo(new ErrorModel(AuthErrors.PasswordIsRequired, "Password", "Password")),
                "Should require password"
            );
        }

        [Test]
        public async Task ShouldRequireCorrectPassword()
        {
            var services = await setup();
            var model = createLoginModel();
            model.Credentials.Password = "Incorrect";
            Assert.ThrowsAsync<PasswordIncorrectException>
            (
                () => execute(services, model)
            );
        }

        [Test]
        public async Task ShouldVerifyCorrectPassword()
        {
            var services = await setup();
            var model = createLoginModel();
            var result = await execute(services, model);
            Assert.That(result.Data?.Url, Is.EqualTo("~/User"), "Should redirect to start if password is correct");
        }

        [Test]
        public async Task ShouldAuthenticateSession()
        {
            var services = await setup();
            var model = createLoginModel();
            await execute(services, model);
            var tempLog = services.GetService<TempLog>();
            var clock = services.GetService<Clock>();
            var authSessionFiles = tempLog.AuthSessionFiles(clock.Now().AddMinutes(1)).ToArray();
            Assert.That(authSessionFiles.Length, Is.EqualTo(1), "Should authenticate session");
            var serializedAuthSession = await authSessionFiles[0].Read();
            var authSession = JsonSerializer.Deserialize<AuthenticateSessionModel>(serializedAuthSession);
            var appFactory = services.GetService<AppFactory>();
            var user = await appFactory.Users.User(new AppUserName(model.Credentials.UserName));
            Assert.That(authSession.UserName, Is.EqualTo(user.UserName().Value), "Should authenticate session");
        }

        [Test]
        public async Task ShouldClearSessionForAnonUser()
        {
            var services = await setup();
            var model = createLoginModel();
            await execute(services, model);
            var anonClient = services.GetService<IAnonClient>();
            Assert.That(anonClient.SessionKey, Is.EqualTo(""), "Should clear session for anon client after authenticating");
        }

        [Test]
        public async Task ShouldResetCache()
        {
            var services = await setup();
            var model = createLoginModel();
            var appFactory = services.GetService<AppFactory>();
            var user = await appFactory.Users.User(new AppUserName(model.Credentials.UserName));
            var httpContextAccessor = services.GetService<IHttpContextAccessor>();
            httpContextAccessor.HttpContext = new DefaultHttpContext
            {
                User = new FakeHttpUser().Create("", user)
            };
            await execute(services, model);
            var userContext = services.GetService<IUserContext>();
            var firstCachedUser = await userContext.User();
            await execute(services, model);
            var secondCachedUser = await userContext.User();
            Assert.That(ReferenceEquals(firstCachedUser, secondCachedUser), Is.False, "Should reset cache after login");
        }

        private async Task<IServiceProvider> setup()
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices
                (
                    (hostContext, services) =>
                    {
                        services.AddFakesForHubWebApp(hostContext.Configuration);
                    }
                )
                .Build();
            var scope = host.Services.CreateScope();
            var sp = scope.ServiceProvider;
            var appFactory = sp.GetService<AppFactory>();
            var model = createLoginModel();
            await appFactory.Users.Add
            (
                new AppUserName(model.Credentials.UserName),
                new FakeHashedPassword(model.Credentials.Password),
                DateTime.UtcNow
            );
            var user = await appFactory.Users.User(new AppUserName(model.Credentials.UserName));
            var app = await appFactory.Apps.App(HubInfo.AppKey);
            var tempLogSession = sp.GetService<TempLogSession>();
            await tempLogSession.StartSession();
            return sp;
        }

        private static Task<ResultContainer<WebRedirectResult>> execute(IServiceProvider services, LoginModel model)
        {
            var api = services.GetService<HubAppApi>();
            return api.Auth.Login.Execute(model);
        }

        private LoginModel createLoginModel()
            => new LoginModel
            {
                Credentials = new LoginCredentials
                {
                    UserName = "xartogg",
                    Password = "password"
                }
            };
    }
}