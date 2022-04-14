using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_App.Fakes;
using XTI_Core;
using XTI_Hub;
using XTI_Hub.Abstractions;
using XTI_HubAppApi;
using XTI_HubDB.Extensions;
using XTI_HubSetup;
using XTI_WebApp.Fakes;

namespace HubWebApp.Fakes;

public static class FakeExtensions
{
    public static void AddFakesForHubWebApp(this IServiceCollection services)
    {
        services.AddFakesForXtiWebApp();
        services.AddHubDbContextForInMemory();
        services.AddScoped<AppFactory>();
        services.AddTransient<AppFromPath>();
        services.AddHubAppApiServices();
        services.AddScoped<HubAppApiFactory>();
        services.AddScoped<AppApiFactory>(sp => sp.GetRequiredService<HubAppApiFactory>());
        services.AddScoped(sp => (HubAppApi)sp.GetRequiredService<IAppApi>());
        services.AddScoped<IVersionReader>
        (
            sp => new FakeVersionReader
            (
                new XtiVersionModel
                {
                    GroupName = "hubwebapp",
                    TimeAdded = DateTimeOffset.Now,
                    Status = AppVersionStatus.Values.Current,
                    VersionKey = new AppVersionKey(1),
                    VersionType = AppVersionType.Values.Major,
                    VersionNumber = new AppVersionNumber(1, 0, 0)
                }
            )
        );
        services.AddScoped(sp =>
        {
            return new HubAppSetup
            (
                sp.GetRequiredService<AppFactory>(),
                sp.GetRequiredService<IClock>(),
                sp.GetRequiredService<HubAppApiFactory>(),
                sp.GetRequiredService<IVersionReader>(),
                "webapps.example.com"
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
        services.AddScoped<IHubAdministration, DbHubAdministration>();
    }
}