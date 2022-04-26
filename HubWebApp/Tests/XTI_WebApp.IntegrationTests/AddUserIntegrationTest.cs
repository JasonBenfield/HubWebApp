using HubWebApp.Api;
using HubWebApp.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Threading.Tasks;
using XTI_App;
using XTI_Configuration.Extensions;

namespace XTI_WebApp.IntegrationTests
{
    public sealed class AddUserIntegrationTest
    {
        [Test]
        public async Task ShouldAddUser()
        {
            var services = new ServiceCollection();
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.UseXtiConfiguration("Development", new string[] { });
            var configuration = configurationBuilder.Build();
            services.ConfigureForHubWebApp(configuration, typeof(AddUserIntegrationTest).Assembly);
            var sp = services.BuildServiceProvider();
            var factory = sp.GetService<AppFactory>();
            await factory.Init();
            var anonUser = await factory.UserRepository().RetrieveByUserName(AppUserName.Anon);
            Assert.That(anonUser.IsUnknown(), Is.False, "Should add anonymous user");
        }
    }
}
