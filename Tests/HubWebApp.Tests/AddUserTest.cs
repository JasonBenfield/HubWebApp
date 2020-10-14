using HubWebApp.Api;
using HubWebApp.Fakes;
using HubWebApp.UserAdminApi;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using XTI_App.Api;
using XTI_App.EF;
using Microsoft.Extensions.DependencyInjection;
using System;
using XTI_WebApp.Fakes;
using XTI_App;

namespace HubWebApp.Tests
{
    public class AddUserTest
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
                Has.One.EqualTo(new ErrorModel(UserAdminErrors.UserNameIsRequired, "UserName")),
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
                Has.One.EqualTo(new ErrorModel(UserAdminErrors.PasswordIsRequired, "Password")),
                "Should require password"
            );
        }

        [Test]
        public async Task ShouldAddUser()
        {
            var input = await setup();
            var result = await execute(input);
            var user = await input.AppDbContext.Users.FirstOrDefaultAsync(u => u.ID == result.Data);
            Assert.That(user, Is.Not.Null, "Should add user");
            Assert.That(user.UserName, Is.EqualTo(input.Model.UserName), "Should add user with the given user name");
        }

        [Test]
        public async Task ShouldHashPassword()
        {
            var input = await setup();
            var result = await execute(input);
            var user = await input.AppDbContext.Users.FirstOrDefaultAsync(u => u.ID == result.Data);
            Assert.That(user.Password, Is.EqualTo(new FakeHashedPassword(input.Model.Password).Value()), "Should add user with the hashed password");
        }

        private async Task<TestInput> setup()
        {
            var services = new ServiceCollection();
            services.AddFakesForXtiWebApp();
            services.AddFakesForHubWebApp();
            var sp = services.BuildServiceProvider();
            var fakeApi = sp.GetService<FakeHubAppApi>();
            var api = await fakeApi.Create();
            return new TestInput(sp, api);
        }

        private static Task<ResultContainer<int>> execute(TestInput input)
        {
            return input.Api.UserAdmin.AddUser.Execute(AccessModifier.Default, input.Model);
        }

        private class TestInput
        {
            public TestInput(IServiceProvider sp, HubAppApi api)
            {
                Api = api;
                AppDbContext = sp.GetService<AppDbContext>();
                Model = new AddUserModel
                {
                    UserName = "xartogg",
                    Password = "password"
                };
            }
            public HubAppApi Api { get; }
            public AppDbContext AppDbContext { get; }
            public AddUserModel Model { get; }
        }
    }
}
