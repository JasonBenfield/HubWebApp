using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_App.Extensions;
using XTI_App.Fakes;
using XTI_Hub;
using XTI_Hub.Abstractions;
using XTI_HubAppApi;
using XTI_HubAppApi.PermanentLog;
using XTI_HubDB.Extensions;
using XTI_WebApp.Abstractions;
using XTI_WebApp.Api;
using XTI_WebApp.Fakes;

namespace HubWebApp.Fakes;

public static class FakeExtensions
{
    public static void AddFakesForHubWebApp(this IServiceCollection services)
    {
        services.AddFakesForXtiWebApp();
        services.AddScoped<ILoginReturnKey, LoginReturnKey>();
        services.AddHubDbContextForInMemory();
        services.AddScoped<HubFactory>();
        services.AddScoped<InitialSetup>();
        services.AddTransient<AppFromPath>();
        services.AddHubAppApiServices();
        services.AddScoped<HubAppApiFactory>();
        services.AddScoped<AppApiFactory>(sp => sp.GetRequiredService<HubAppApiFactory>());
        services.AddScoped(sp => (HubAppApi)sp.GetRequiredService<IAppApi>());
        services.AddScoped(sp =>
        {
            return new HubAppSetup
            (
                sp.GetRequiredService<HubFactory>(),
                sp.GetRequiredService<HubAppApiFactory>()
            );
        });
        services.AddScoped<IAppSetup>(sp => sp.GetRequiredService<HubAppSetup>());
        services.AddScoped(_ => HubInfo.AppKey);
        services.AddScoped<FakeAccessForAuthenticate>();
        services.AddScoped<AccessForAuthenticate>(sp => sp.GetRequiredService<FakeAccessForAuthenticate>());
        services.AddScoped<FakeAccessForLogin>();
        services.AddScoped<AccessForLogin>(sp => sp.GetRequiredService<FakeAccessForLogin>());
        services.AddScoped
        (
            sp => new DefaultFakeSetup
            (
                sp.GetRequiredService<AppApiFactory>(),
                sp.GetRequiredService<FakeAppContext>()
            )
        );
        services.AddScoped<IHubAdministration, EfHubAdministration>();
        services.AddScoped<IStoredObjectDB, EfStoredObjectDB>();
        services.AddScoped<StoredObjectFactory>();
        services.AddScoped<PermanentLog>();
        services.AddScoped<ICachedUserContext>(sp => sp.GetRequiredService<CachedUserContext>());
    }
}