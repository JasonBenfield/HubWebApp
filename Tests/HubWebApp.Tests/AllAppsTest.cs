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
using XTI_App.Api;

namespace HubWebApp.Tests
{
    public sealed class AllAppsTest
    {
        [Test]
        public async Task ShouldGetAllApps_WhenUserIsModCategoryAdmin()
        {
            var services = await setup();
            var adminUser = await services.AddAdminUser();
            services.LoginAs(adminUser);
            requestPage(services);
            var hubApp = await services.HubApp();
            var appsModCategory = await hubApp.ModCategory(new ModifierCategoryName("Apps"));
            await adminUser.GrantFullAccessToModCategory(appsModCategory);
            var apps = await execute(services);
            var appNames = apps.Select(a => a.AppName);
            Assert.That
            (
                appNames,
                Is.EquivalentTo(new[] { AppKey.Unknown.Name.Value, HubAppKey.Key.Name.Value }),
                "Should get all apps"
            );
        }

        [Test]
        public async Task ShouldGetOnlyAllowedApps()
        {
            var services = await setup();
            var adminUser = await services.AddAdminUser();
            services.LoginAs(adminUser);
            requestPage(services);
            var hubApp = await services.HubApp();
            var appsModCategory = await hubApp.ModCategory(new ModifierCategoryName("Apps"));
            var hubAppModifier = await appsModCategory.Modifier(hubApp.ID.Value);
            await adminUser.AddModifier(hubAppModifier);
            var apps = await execute(services);
            var appNames = apps.Select(a => a.AppName);
            Assert.That
            (
                appNames,
                Is.EquivalentTo(new[] { HubAppKey.Key.Name.Value }),
                "Should get only allowed apps"
            );
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
            await scope.ServiceProvider.Setup();
            return scope.ServiceProvider;
        }

        private static void requestPage(IServiceProvider services)
        {
            var hubApi = services.GetService<HubAppApi>();
            services.RequestPage(hubApi.Apps.All.Path);
        }

        private static async Task<AppModel[]> execute(IServiceProvider services)
        {
            var hubApi = services.GetService<HubAppApi>();
            var result = await hubApi.Apps.All.Execute(new EmptyRequest());
            return result.Data;
        }

    }
}
