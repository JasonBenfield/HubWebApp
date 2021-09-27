using HubWebApp.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_Hub;
using XTI_HubAppApi;
using XTI_HubAppApi.Auth;
using XTI_HubDB.Extensions;
using XTI_WebApp.Fakes;

namespace HubWebApp.Fakes
{
    public static class FakeExtensions
    {
        public static void AddFakesForHubWebApp(this IServiceCollection services, IConfiguration config)
        {
            services.AddFakesForXtiWebApp(config);
            services.AddHubDbContextForInMemory();
            services.AddScoped<AppFactory>();
            services.AddTransient<AppFromPath>();
            services.AddScoped<HubAppApiFactory>();
            services.AddScoped<AppApiFactory>(sp => sp.GetService<HubAppApiFactory>());
            services.AddScoped(sp => (HubAppApi)sp.GetService<IAppApi>());
            services.AddScoped<HubAppSetup>();
            services.AddScoped<IAppSetup>(sp => sp.GetService<HubAppSetup>());
            services.AddScoped(_ => HubInfo.AppKey);
            services.AddScoped<AccessForAuthenticate, FakeAccessForAuthenticate>();
            services.AddScoped<AccessForLogin, FakeAccessForLogin>();
            services.AddScoped<ISourceAppContext, DefaultAppContext>();
            services.AddScoped<ISourceUserContext, WebUserContext>();
        }
    }
}
