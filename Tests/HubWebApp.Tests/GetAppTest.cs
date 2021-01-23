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
    public sealed class GetAppTest
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
        public async Task ShouldGetApp()
        {
            var services = await setup();
            var adminUser = await services.AddAdminUser();
            services.LoginAs(adminUser);
            var hubAppModifier = await getHubAppModifier(services);
            requestPage(services, hubAppModifier.ModKey());
            var app = await execute(services);
            Assert.That(app?.Title, Is.EqualTo("Hub"), "Should get app");
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
            services.RequestPage(hubApi.App.GetApp.Path.WithModifier(modifierKey ?? ModifierKey.Default));
        }

        private static async Task<AppModel> execute(IServiceProvider services)
        {
            var hubApi = services.GetService<HubAppApi>();
            var result = await hubApi.App.GetApp.Execute(new EmptyRequest());
            return result.Data;
        }

    }
}
