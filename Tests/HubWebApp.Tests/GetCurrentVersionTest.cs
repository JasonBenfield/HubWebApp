using HubWebApp.Api;
using HubWebApp.Apps;
using HubWebApp.Core;
using HubWebApp.Fakes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using XTI_App;
using XTI_App.Api;

namespace HubWebApp.Tests
{
    public sealed class GetCurrentVersionTest
    {
        [Test]
        public async Task ShouldThrowError_WhenModifierIsBlank()
        {
            var services = await setup();
            var adminUser = await services.AddAdminUser();
            services.LoginAs(adminUser);
            requestPage(services);
            var ex = Assert.ThrowsAsync<Exception>(() => execute(services));
            Assert.That(ex.Message, Is.EqualTo(AppErrors.ModifierIsRequired));
        }

        [Test]
        public async Task ShouldThrowError_WhenModifierIsNotFound()
        {
            var services = await setup();
            var adminUser = await services.AddAdminUser();
            services.LoginAs(adminUser);
            var modKey = new ModifierKey("NotFound");
            requestPage(services, modKey);
            var ex = Assert.ThrowsAsync<Exception>(() => execute(services));
            Assert.That(ex.Message, Is.EqualTo(string.Format(AppErrors.ModifierNotFound, modKey.Value)));
        }

        [Test]
        public async Task ShouldGetCurrentVersion()
        {
            var services = await setup();
            var adminUser = await services.AddAdminUser();
            services.LoginAs(adminUser);
            var hubAppModifier = await getHubAppModifier(services);
            requestPage(services, hubAppModifier.ModKey());
            var hubApp = await services.HubApp();
            var currentVersion = await hubApp.CurrentVersion();
            var currentVersionModel = await execute(services);
            Assert.That(currentVersionModel?.ID, Is.EqualTo(currentVersion.ID.Value), "Should get current version");
            var version = currentVersion.Version();
            Assert.That(currentVersionModel?.Major, Is.EqualTo(version.Major), "Should get current version");
            Assert.That(currentVersionModel?.Minor, Is.EqualTo(version.Minor), "Should get current version");
            Assert.That(currentVersionModel?.Patch, Is.EqualTo(version.Build), "Should get current version");
            Assert.That(currentVersionModel?.VersionType, Is.EqualTo(currentVersion.Type()), "Should get current version");
            Assert.That(currentVersionModel?.Status, Is.EqualTo(AppVersionStatus.Values.Current), "Should get current version");
        }

        private static async Task<Modifier> getHubAppModifier(IServiceProvider services)
        {
            var hubApp = await services.HubApp();
            var appsModCategory = await hubApp.ModCategory(HubInfo.ModCategories.Apps);
            var hubAppModifier = await appsModCategory.Modifier(hubApp.ID.Value);
            return hubAppModifier;
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

        private static void requestPage(IServiceProvider services, ModifierKey modifierKey = null)
        {
            var hubApi = services.GetService<HubAppApi>();
            services.RequestPage(hubApi.App.GetCurrentVersion.Path.WithModifier(modifierKey ?? ModifierKey.Default));
        }

        private static async Task<AppVersionModel> execute(IServiceProvider services)
        {
            var hubApi = services.GetService<HubAppApi>();
            var result = await hubApi.App.GetCurrentVersion.Execute(new EmptyRequest());
            return result.Data;
        }

    }
}
