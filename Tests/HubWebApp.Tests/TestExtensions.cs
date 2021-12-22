using HubWebApp.Fakes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_App.Fakes;
using XTI_AppSetupApp.Extensions;
using XTI_Core;
using XTI_Core.Fakes;
using XTI_Hub;
using XTI_HubAppApi;

namespace HubWebApp.Tests;

internal static class TestExtensions
{
    public static void AddServicesForTests(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddFakesForHubWebApp(configuration);
        services.AddSingleton<IClock, FakeClock>();
        services.AddScoped<AppFactory>();
        services.AddScoped<HubAppApiFactory>();
        services.AddScoped<AppApiFactory>(sp => sp.GetRequiredService<HubAppApiFactory>());
        services.AddScoped<FakeXtiPathAccessor>();
        services.AddScoped<IXtiPathAccessor>(sp => sp.GetRequiredService<FakeXtiPathAccessor>());
        services.AddScoped<FakeAppContext>();
        services.AddScoped<ISourceAppContext>(sp => sp.GetRequiredService<FakeAppContext>());
        services.AddScoped<IAppContext>(sp => sp.GetRequiredService<FakeAppContext>());
        services.AddScoped<FakeUserContext>();
        services.AddScoped<ISourceUserContext>(sp => sp.GetRequiredService<FakeUserContext>());
        services.AddScoped<IUserContext>(sp => sp.GetRequiredService<FakeUserContext>());
        services.AddScoped<IAppApiUser, AppApiUser>();
        services.AddScoped
        (
            sp => sp.GetRequiredService<HubAppApiFactory>().Create(sp.GetRequiredService<IAppApiUser>())
        );
        services.AddScoped<IAppSetup, DefaultAppSetup>();

    }
}