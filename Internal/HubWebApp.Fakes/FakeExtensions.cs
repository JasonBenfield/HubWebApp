using HubWebApp.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_App.Fakes;
using XTI_Hub;
using XTI_HubAppApi;
using XTI_HubAppApi.Auth;
using XTI_HubDB.Extensions;
using XTI_HubSetup;
using XTI_WebApp.Fakes;

namespace HubWebApp.Fakes;

public static class FakeExtensions
{
    public static void AddFakesForHubWebApp(this IServiceCollection services, IConfiguration config)
    {
        services.AddFakesForXtiWebApp(config);
        services.AddHubDbContextForInMemory();
        services.AddScoped<AppFactory>();
        services.AddTransient<AppFromPath>();
        services.AddHubAppApiServices();
        services.AddScoped<HubAppApiFactory>();
        services.AddScoped<AppApiFactory>(sp => sp.GetRequiredService<HubAppApiFactory>());
        services.AddScoped(sp => (HubAppApi)sp.GetRequiredService<IAppApi>());
        services.AddScoped(sp => new VersionReader(""));
        services.AddScoped<HubAppSetup>();
        services.AddScoped<IAppSetup>(sp => sp.GetRequiredService<HubAppSetup>());
        services.AddScoped(_ => HubInfo.AppKey);
        services.AddScoped<AccessForAuthenticate, FakeAccessForAuthenticate>();
        services.AddScoped<AccessForLogin, FakeAccessForLogin>();
        services.AddScoped
        (
            sp => new DefaultFakeSetup
            (
                sp.GetRequiredService<AppApiFactory>(),
                sp.GetRequiredService<FakeAppContext>(),
                "Hub"
            )
        );
        //services.AddScoped<ISourceAppContext, DefaultAppContext>();
        //services.AddScoped<ISourceUserContext, WebUserContext>();
    }
}