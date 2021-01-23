using HubWebApp.Api;
using HubWebApp.Apps;
using HubWebApp.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XTI_App.Api;
using XTI_WebApp.Fakes;

namespace HubWebApp.Fakes
{
    public static class FakeExtensions
    {
        public static void AddFakesForHubWebApp(this IServiceCollection services, IConfiguration config)
        {
            services.AddFakesForXtiWebApp(config);
            services.AddScoped<AppFromPath>();
            services.AddScoped<AppApiFactory, HubAppApiFactory>();
            services.AddTransient<HubAppApi>();
            services.AddTransient<IAppApi>(sp => sp.GetService<HubAppApi>());
            services.AddScoped<HubSetup>();
            services.AddScoped(_ => HubInfo.AppKey);
        }
    }
}
