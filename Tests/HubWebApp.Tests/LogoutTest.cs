using HubWebApp.Api;
using HubWebApp.Fakes;
using HubWebApp.UserAdminApi;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using XTI_App;
using XTI_App.Api;
using XTI_WebApp.Fakes;

namespace HubWebApp.Tests
{
    public class LogoutTest
    {
        [Test]
        public async Task ShouldEndSession()
        {
            var input = await setup();
            await execute(input);
            Assert.That
            (
                input.SessionContext.CurrentSession.HasEnded(),
                Is.True,
                "Should end session"
            );
        }

        [Test]
        public async Task ShouldRedirectToLogin()
        {
            var input = await setup();
            var result = await execute(input);
            Assert.That
            (
                result.Data.Url,
                Is.EqualTo("/Hub/Current/Auth"),
                "Should end session"
            );
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
                UserName = "Xartogg",
                Password = "Password12345"
            });
            var sessionContext = sp.GetService<ISessionContext>();
            await sessionContext.StartSession();
            return input;
        }

        private static Task<ResultContainer<AppActionRedirectResult>> execute(TestInput input)
        {
            return input.Api.Auth.Logout.Execute(new EmptyRequest());
        }

        private class TestInput
        {
            public TestInput(IServiceProvider sp, HubAppApi api)
            {
                Api = api;
                AppFactory = sp.GetService<AppFactory>();
                SessionContext = sp.GetService<ISessionContext>();
            }
            public HubAppApi Api { get; }
            public AppFactory AppFactory { get; }
            public ISessionContext SessionContext { get; }
        }
    }
}