using XTI_HubDB.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
using XTI_Hub;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_App.Fakes;
using XTI_Core;
using XTI_HubAppApi;
using XTI_HubAppApi.UserList;

namespace HubWebApp.Tests
{
    public class GetUsersTest
    {
        [Test]
        public async Task ShouldGetUsers()
        {
            var tester = await setup();
            var userName = new AppUserName("Test.User");
            await addUser(tester, userName);
            var factory = tester.Services.GetService<AppFactory>();
            var users = (await factory.Users().Users()).ToArray();
            Assert.That(users.Select(u => u.UserName()), Has.One.EqualTo(userName), "Should get all users");
        }

        private async Task addUser(IHubActionTester tester, AppUserName userName)
        {
            var hubApiFactory = tester.Services.GetService<HubAppApiFactory>();
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

        private AddUserModel createModel()
        {
            return new AddUserModel
            {
                UserName = "test.user",
                Password = "Password12345;"
            };
        }
    }
}
