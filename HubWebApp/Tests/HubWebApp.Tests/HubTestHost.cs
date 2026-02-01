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
        await initialSetup.Run(ct: default);
        var hubAdmin = sp.GetRequiredService<IHubAdministration>();
        await hubAdmin.AddOrUpdateApps
        (
            new AppVersionName("HubWebApp"),
            [HubInfo.AppKey],
            default
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
            ],
            default
        );
        var setup = sp.GetRequiredService<IAppSetup>();
        await setup.Run(AppVersionKey.Current, ct: default);
        var defaultFakeSetup = sp.GetRequiredService<DefaultFakeSetup>();
        await defaultFakeSetup.Run(AppVersionKey.Current, ct: default);
        var factory = sp.GetRequiredService<EfHubDB>();
        var hubApp = await factory.Apps.App(HubInfo.AppKey, ct: default);
        var adminUser = await AddAdminUser(sp);
        var currentUserName = sp.GetRequiredService<FakeCurrentUserName>();
        currentUserName.SetUserName(adminUser.ToModel().UserName);
        return sp;
    }

    private async Task<EfAppUser> AddAdminUser(IServiceProvider services)
    {
        var factory = services.GetRequiredService<EfHubDB>();
        var userGroup = await factory.UserGroups.GetGeneral(ct: default);
        var adminUser = await userGroup.AddOrUpdate(new AppUserName("hubadmin"), new FakeHashedPassword("Password12345"), DateTime.UtcNow, ct: default);
        var hubApp = await factory.Apps.App(HubInfo.AppKey, ct: default);
        var adminRole = await hubApp.Role(HubInfo.Roles.Admin, ct: default);
        await adminUser.AssignRole(adminRole, ct: default);
        return adminUser;
    }
}