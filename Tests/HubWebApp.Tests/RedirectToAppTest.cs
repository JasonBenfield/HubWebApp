using HubWebApp.Api;
using HubWebApp.Core;
using HubWebApp.Fakes;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XTI_App;

namespace HubWebApp.Tests
{
    public sealed class RedirectToAppTest
    {
        [Test]
        public async Task ShouldAddModifierToUrl()
        {
            var services = await setup();
            var adminUser = await services.AddAdminUser();
            services.LoginAs(adminUser);
            var httpContextAccessor = services.GetService<IHttpContextAccessor>();
            httpContextAccessor.HttpContext.Request.PathBase = "/Hub/Current";
            httpContextAccessor.HttpContext.Request.Path = "/Apps";
            var hubApp = await services.HubApp();
            var appsModCategory = await hubApp.ModCategory(HubInfo.ModCategories.Apps);
            var hubAppModifier = await appsModCategory.Modifier(hubApp.ID.Value);
            var hubApi = services.GetService<HubAppApi>();
            var result = await hubApi.Apps.RedirectToApp.Execute(hubApp.ID.Value);
            Assert.That
            (
                result.Data.Url,
                Is.EqualTo($"/Hub/Current/AppDashboard/Index/{hubAppModifier.ModKey().Value}").IgnoreCase,
                "Should add app modifier when redirecting to app"
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
    }
}
