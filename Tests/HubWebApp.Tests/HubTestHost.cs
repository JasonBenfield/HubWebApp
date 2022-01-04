﻿using HubWebApp.Fakes;
using Microsoft.Extensions.Hosting;

namespace HubWebApp.Tests;

internal sealed class HubTestHost
{
    public async Task<IServiceProvider> Setup(Action<HostBuilderContext, IServiceCollection>? configure = null)
    {
        var host = Host.CreateDefaultBuilder()
            .ConfigureServices
            (
                (hostContext, services) =>
                {
                    services.AddFakesForHubWebApp(hostContext.Configuration);
                    if (configure != null)
                    {
                        configure(hostContext, services);
                    }
                }
            )
            .Build();
        var scope = host.Services.CreateScope();
        var sp = scope.ServiceProvider;
        var setup = sp.GetRequiredService<IAppSetup>();
        await setup.Run(AppVersionKey.Current);
        var defaultFakeSetup = scope.ServiceProvider.GetRequiredService<DefaultFakeSetup>();
        await defaultFakeSetup.Run(AppVersionKey.Current);

        var factory = sp.GetRequiredService<AppFactory>();
        var hubApp = await factory.Apps.App(HubInfo.AppKey);
        var appsModCategory = await hubApp.ModCategory(HubInfo.ModCategories.Apps);
        var modifier = await appsModCategory.ModifierByTargetID(hubApp.ID.Value);

        var appContext = sp.GetRequiredService<FakeAppContext>();
        var fakeApp = appContext.App();
        var fakeHubApp = appContext.App(HubInfo.AppKey);
        var fakeModCategory = fakeHubApp.ModCategory(HubInfo.ModCategories.Apps);
        var fakeModifier = fakeModCategory.ModifierByTargetID(fakeApp.ID.Value.ToString());
        fakeModifier.SetModKey(modifier);

        var userContext = scope.ServiceProvider.GetRequiredService<FakeUserContext>();
        var adminUser = await addAdminUser(scope.ServiceProvider);
        var fakeAdminUser = userContext.AddUser(adminUser);
        fakeAdminUser.AddRole(HubInfo.Roles.Admin);
        return sp;
    }

    private async Task<AppUser> addAdminUser(IServiceProvider services)
    {
        var factory = services.GetRequiredService<AppFactory>();
        var adminUser = await factory.Users.Add(new AppUserName("hubadmin"), new FakeHashedPassword("Password12345"), DateTime.UtcNow);
        var hubApp = await factory.Apps.App(HubInfo.AppKey);
        var adminRole = await hubApp.Role(HubInfo.Roles.Admin);
        await adminUser.AssignRole(adminRole);
        return adminUser;
    }
}