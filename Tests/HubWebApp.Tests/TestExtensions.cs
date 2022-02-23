using HubWebApp.Fakes;
using XTI_AppSetupApp.Extensions;
using XTI_Core;
using XTI_Core.Fakes;

namespace HubWebApp.Tests;

internal static class TestExtensions
{
    public static void AddServicesForTests(this IServiceCollection services)
    {
        services.AddFakesForHubWebApp();
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