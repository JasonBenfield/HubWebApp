﻿using HubWebApp.Api;
using HubWebApp.Fakes;
using HubWebApp.UserAdminApi;
using MainDB.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using XTI_App;
using XTI_App.Api;
using XTI_App.Fakes;

namespace HubWebApp.Tests
{
    public class AddUserTest
    {
        [Test]
        public async Task ShouldRequireUserName()
        {
            var input = await setup();
            input.Services.LoginAs(input.AdminUser);
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
            input.Services.LoginAs(input.AdminUser);
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
            input.Services.LoginAs(input.AdminUser);
            var result = await execute(input);
            var user = await input.MainDbContext.Users.FirstOrDefaultAsync(u => u.ID == result.Data);
            Assert.That(user, Is.Not.Null, "Should add user");
            Assert.That(user.UserName, Is.EqualTo(input.Model.UserName), "Should add user with the given user name");
        }

        [Test]
        public async Task ShouldHashPassword()
        {
            var input = await setup();
            input.Services.LoginAs(input.AdminUser);
            var result = await execute(input);
            var user = await input.MainDbContext.Users.FirstOrDefaultAsync(u => u.ID == result.Data);
            Assert.That(user.Password, Is.EqualTo(new FakeHashedPassword(input.Model.Password).Value()), "Should add user with the hashed password");
        }

        private async Task<TestInput> setup()
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
            await scope.ServiceProvider.Setup();
            var adminUser = await sp.AddAdminUser();
            var api = sp.GetService<HubAppApi>();
            return new TestInput(sp, api, adminUser);
        }

        private static Task<ResultContainer<int>> execute(TestInput input)
            => input.Api.UserAdmin.AddUser.Execute(input.Model);

        private class TestInput
        {
            public TestInput(IServiceProvider sp, HubAppApi api, AppUser adminUser)
            {
                Api = api;
                MainDbContext = sp.GetService<MainDbContext>();
                Model = new AddUserModel
                {
                    UserName = "xartogg",
                    Password = "password"
                };
                AdminUser = adminUser;
                Services = sp;
            }
            public HubAppApi Api { get; }
            public MainDbContext MainDbContext { get; }
            public AddUserModel Model { get; }
            public AppUser AdminUser { get; }
            public IServiceProvider Services { get; }
        }
    }
}
