﻿using Microsoft.Extensions.DependencyInjection;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_App.Extensions;
using XTI_App.Fakes;
using XTI_Hub;
using XTI_HubDB.Extensions;
using XTI_HubWebAppApi;
using XTI_HubWebAppApiActions;
using XTI_WebApp.Abstractions;
using XTI_WebApp.Fakes;

namespace HubWebApp.Fakes;

public static class FakeExtensions
{
    public static void AddFakesForHubWebApp(this IServiceCollection services)
    {
        services.AddFakesForXtiWebApp();
        services.AddSingleton<HubWebAppOptions>();
        services.AddScoped<ISourceAppContext, EfAppContext>();
        services.AddScoped<IAppContext>(sp => sp.GetRequiredService<ISourceAppContext>());
        services.AddScoped<ISourceUserContext, EfUserContext>();
        services.AddScoped<IUserContext>(sp => sp.GetRequiredService<ISourceUserContext>());
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
        services.AddKeyedScoped<IAccess>("Authenticate", (sp, key) => sp.GetRequiredService<FakeAccessForAuthenticate>());
        services.AddScoped<FakeAccessForLogin>();
        services.AddKeyedScoped<IAccess>("Login", (sp, key) => sp.GetRequiredService<FakeAccessForLogin>());
        services.AddScoped
        (
            sp => new DefaultFakeSetup
            (
                sp.GetRequiredService<AppApiFactory>(),
                sp.GetRequiredService<FakeAppContext>()
            )
        );
        services.AddScoped<AppRegistration>();
        services.AddScoped<IHubAdministration, EfHubAdministration>();
        services.AddScoped<EfPermanentLog>();
        services.AddScoped<ICachedUserContext>(sp => sp.GetRequiredService<CachedUserContext>());
        services.AddScoped<IUserCacheManagement, FakeUserCacheManagement>();
        services.AddScoped<AuthenticationFactory>();
    }
}