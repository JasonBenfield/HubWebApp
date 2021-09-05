using HubWebAppApi;
using HubWebAppApi.Apps;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_App.Fakes;
using XTI_WebApp.Fakes;

namespace HubWebApp.Fakes
{
    public static class FakeExtensions
    {
        public static void AddFakesForHubWebApp(this IServiceCollection services, IConfiguration config)
        {
            services.AddFakesForXtiWebApp(config);
            services.AddTransient<AppFromPath>();
            services.AddScoped<AppApiFactory, HubAppApiFactory>();
            services.AddScoped(sp => (HubAppApi)sp.GetService<IAppApi>());
            services.AddScoped<HubSetup>();
            services.AddScoped(_ => HubInfo.AppKey);
            services.AddScoped<AccessForAuthenticate, FakeAccessForAuthenticate>();
            services.AddScoped<AccessForLogin, FakeAccessForLogin>();
            services.AddSingleton<IXtiPathAccessor, FakeXtiPathAccessor>();
        }
    }
}
