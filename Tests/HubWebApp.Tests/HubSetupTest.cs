using HubWebApp.Api;
using HubWebApp.Core;
using HubWebApp.Fakes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
using XTI_App;

namespace HubWebApp.Tests
{
    public sealed class HubSetupTest
    {
        [Test]
        public async Task ShouldAddUnknownApp()
        {
            var services = setup();
            var hubSetup = services.GetService<HubSetup>();
            await hubSetup.Run();
            var factory = services.GetService<AppFactory>();
            var unknownApp = await factory.Apps().App(AppKey.Unknown);
            Assert.That(unknownApp.ID.IsValid(), Is.True, "Should add unknown app");
        }

        [Test]
        public async Task ShouldAddHubApp()
        {
            var services = setup();
            var hubSetup = services.GetService<HubSetup>();
            await hubSetup.Run();
            var factory = services.GetService<AppFactory>();
            var hubApp = await factory.Apps().App(HubInfo.AppKey);
            Assert.That(hubApp.Key(), Is.EqualTo(HubInfo.AppKey), "Should add hub app");
        }

        [Test]
        public async Task ShouldAddModCategoryForApps()
        {
            var services = setup();
            var hubSetup = services.GetService<HubSetup>();
            await hubSetup.Run();
            var factory = services.GetService<AppFactory>();
            var hubApp = await factory.Apps().App(HubInfo.AppKey);
            var modCategoryName = HubInfo.ModCategories.Apps;
            var modCategory = await hubApp.ModCategory(modCategoryName);
            Assert.That(modCategory.Name(), Is.EqualTo(modCategoryName), "Should add mod category for apps");
        }

        [Test]
        public async Task ShouldAddModifierForEachApp()
        {
            var services = setup();
            var hubSetup = services.GetService<HubSetup>();
            await hubSetup.Run();
            var factory = services.GetService<AppFactory>();
            var hubApp = await factory.Apps().App(HubInfo.AppKey);
            var modCategoryName = HubInfo.ModCategories.Apps;
            var modCategory = await hubApp.ModCategory(modCategoryName);
            var modifiers = await modCategory.Modifiers();
            var modIDs = modifiers.Select(m => m.TargetKey);
            var apps = await factory.Apps().All();
            var appIDs = apps.Select(a => a.ID.Value.ToString());
            Assert.That(modIDs, Is.EquivalentTo(appIDs), "Should add modifier for each app");
        }

        private IServiceProvider setup()
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
            return scope.ServiceProvider;
        }
    }
}
