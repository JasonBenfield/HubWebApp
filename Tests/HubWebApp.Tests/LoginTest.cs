using HubWebApp.Api;
using HubWebApp.Fakes;
using HubWebApp.AuthApi;
using HubWebApp.UserAdminApi;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using XTI_WebApp.Api;
using XTI_WebApp.Fakes;

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

        private async Task<TestInput> setup()
        {
            var services = new ServiceCollection();
            services.AddFakesForXtiWebApp();
            services.AddFakesForHubWebApp();
            var sp = services.BuildServiceProvider();
            var fakeApi = sp.GetService<FakeHubAppApi>();
            var api = await fakeApi.Create();
            var input = new TestInput(sp, api);
            await input.Api.UserAdmin.AddUser.Execute(new AddUserModel
            {
                UserName = input.Model.UserName,
                Password = input.Model.Password
            });
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
                Api = api; sp.GetService<HubAppApi>();
                Model = new LoginModel
                {
                    UserName = "xartogg",
                    Password = "password"
                };
            }
            public HubAppApi Api { get; }
            public LoginModel Model { get; }
        }
    }
}