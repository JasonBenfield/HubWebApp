﻿using HubWebApp.Api;
using HubWebApp.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Threading.Tasks;
using XTI_App;
using XTI_Configuration.Extensions;
using XTI_WebApp.Extensions;

namespace HubWebApp.IntegrationTests
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
            services.AddXtiServices(configuration, typeof(AddUserIntegrationTest).Assembly);
            services.AddServicesForHub();
            var sp = services.BuildServiceProvider();
            var factory = sp.GetService<AppFactory>();
            var setup = new AppSetup(factory);
            await setup.Run();
            var anonUser = await factory.UserRepository().RetrieveByUserName(AppUserName.Anon);
            Assert.That(anonUser.IsUnknown(), Is.False, "Should add anonymous user");
        }
    }
}
