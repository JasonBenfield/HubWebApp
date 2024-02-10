using HubWebApp.Fakes;
using Microsoft.Extensions.Hosting;
using XTI_Core;
using XTI_Core.Extensions;
using XTI_Core.Fakes;

namespace HubWebApp.Tests;

internal sealed class HubTestHost
{
    public async Task<IServiceProvider> Setup(Action<IServiceCollection>? configure = null)
    {
        var envName = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Production";
        var builder = new XtiHostBuilder(XtiEnvironment.Parse(envName));
        builder.Services.AddSingleton<IHostEnvironment>
        (
            _ => new FakeHostEnvironment { EnvironmentName = envName }
        );
        builder.Services.AddFakesForHubWebApp();
        if (configure != null)
        {
            configure(builder.Services);
        }
        var sp = builder.Build().Scope();
        var initialSetup = sp.GetRequiredService<InitialSetup>();
        await initialSetup.Run();
        var hubAdmin = sp.GetRequiredService<IHubAdministration>();
        await hubAdmin.AddOrUpdateApps
        (
            new AppVersionName("HubWebApp"),
            [HubInfo.AppKey]
        );
        await hubAdmin.AddOrUpdateVersions
        (
            [HubInfo.AppKey],
            [
                new AddVersionRequest
                (
                    versionName: new AppVersionName("HubWebApp"),
                    versionKey: new AppVersionKey(1),
                    versionNumber: new AppVersionNumber(1,0,0),
                    status: AppVersionStatus.Values.Current,
                    versionType: AppVersionType.Values.Major
                )
            ]
        );
        var setup = sp.GetRequiredService<IAppSetup>();
        await setup.Run(AppVersionKey.Current);
        var defaultFakeSetup = sp.GetRequiredService<DefaultFakeSetup>();
        await defaultFakeSetup.Run(AppVersionKey.Current);
        var factory = sp.GetRequiredService<HubFactory>();
        var hubApp = await factory.Apps.App(HubInfo.AppKey);
        var adminUser = await AddAdminUser(sp);
        var currentUserName = sp.GetRequiredService<FakeCurrentUserName>();
        currentUserName.SetUserName(adminUser.ToModel().UserName);
        return sp;
    }

    private async Task<AppUser> AddAdminUser(IServiceProvider services)
    {
        var factory = services.GetRequiredService<HubFactory>();
        var userGroup = await factory.UserGroups.GetGeneral();
        var adminUser = await userGroup.AddOrUpdate(new AppUserName("hubadmin"), new FakeHashedPassword("Password12345"), DateTime.UtcNow);
        var hubApp = await factory.Apps.App(HubInfo.AppKey);
        var adminRole = await hubApp.Role(HubInfo.Roles.Admin);
        await adminUser.AssignRole(adminRole);
        return adminUser;
    }
}