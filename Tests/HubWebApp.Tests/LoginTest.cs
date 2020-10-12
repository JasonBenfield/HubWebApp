using HubWebApp.Api;
using HubWebApp.Fakes;
using HubWebApp.AuthApi;
using HubWebApp.UserAdminApi;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using XTI_App.Api;
using XTI_WebApp.Fakes;
using XTI_App.EF;
using System.Runtime.CompilerServices;
using System.Linq;
using XTI_App;
using Microsoft.AspNetCore.Http;

namespace HubWebApp.Tests
{
    public class LoginTest
    {
        [Test]
        public async Task ShouldRequireUserName()
        {
            var input = await setup();
            input.Model.UserName = "";
            var ex = Assert.ThrowsAsync<ValidationFailedException>
            (
                () => execute(input)
            );
            Assert.That
            (
                ex.Errors,
                Has.One.EqualTo(new ErrorModel(HubWebApp.AuthApi.ValidationErrors.UserNameIsRequired, "UserName")),
                "Should require user name"
            );
        }

        [Test]
        public async Task ShouldRequirePassword()
        {
            var input = await setup();
            input.Model.Password = "";
            var ex = Assert.ThrowsAsync<ValidationFailedException>
            (
                () => execute(input)
            );
            Assert.That
            (
                ex.Errors,
                Has.One.EqualTo(new ErrorModel(HubWebApp.AuthApi.ValidationErrors.PasswordIsRequired, "Password")),
                "Should require password"
            );
        }

        [Test]
        public async Task ShouldRequireCorrectPassword()
        {
            var input = await setup();
            input.Model.Password = "Incorrect";
            Assert.ThrowsAsync<PasswordIncorrectException>
            (
                () => execute(input)
            );
        }

        [Test]
        public async Task ShouldVerifyCorrectPassword()
        {
            var input = await setup();
            var result = await execute(input);
            Assert.That(result.Data?.Token, Is.Not.Null, "Should return token if password is correct");
        }

        [Test]
        public async Task ShouldSetUserForSession()
        {
            var input = await setup();
            await input.SessionContext.StartSession();
            await execute(input);
            var sessions = input.AppDbContext.Sessions.ToArray();
            var user = await input.AppFactory.Users().User(new AppUserName(input.Model.UserName));
            Assert.That(sessions[0].UserID, Is.EqualTo(user.ID));
        }

        private async Task<TestInput> setup()
        {
            var services = new ServiceCollection();
            services.AddFakesForXtiWebApp();
            services.AddFakesForHubWebApp();
            var sp = services.BuildServiceProvider();
            var fakeApi = sp.GetService<FakeHubAppApi>();
            var api = await fakeApi.Create();
            var input = new TestInput(sp, api);
            var userContext = (FakeUserContext)sp.GetService<IUserContext>();
            var anonUser = await input.AppFactory.Users().User(AppUserName.Anon);
            userContext.SetUser(anonUser);
            await input.Api.UserAdmin.AddUser.Execute(new AddUserModel
            {
                UserName = input.Model.UserName,
                Password = input.Model.Password
            });
            var sessionContext = sp.GetService<ISessionContext>();
            await sessionContext.StartSession();
            return input;
        }

        private static Task<ResultContainer<LoginResult>> execute(TestInput input)
        {
            return input.Api.Auth.Login.Execute(input.Model);
        }

        private class TestInput
        {
            public TestInput(IServiceProvider sp, HubAppApi api)
            {
                Api = api;
                Model = new LoginModel
                {
                    UserName = "xartogg",
                    Password = "password"
                };
                AppFactory = sp.GetService<AppFactory>();
                AppDbContext = sp.GetService<AppDbContext>();
                SessionContext = sp.GetService<ISessionContext>();
            }
            public HubAppApi Api { get; }
            public LoginModel Model { get; }
            public AppFactory AppFactory { get; }
            public AppDbContext AppDbContext { get; }
            public ISessionContext SessionContext { get; }
        }
    }
}