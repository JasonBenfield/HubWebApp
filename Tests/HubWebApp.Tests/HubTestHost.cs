using HubWebApp.Fakes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XTI_App.Abstractions;
using XTI_App.Fakes;
using XTI_Hub;

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
        var factory = sp.GetRequiredService<AppFactory>();
        var hubApp = await factory.Apps.App(HubInfo.AppKey);
        var defaultModifier = await hubApp.DefaultModifier();
        var defaultFakeSetup = scope.ServiceProvider.GetRequiredService<DefaultFakeSetup>();
        await defaultFakeSetup.Run(AppVersionKey.Current);
        //defaultFakeSetup.App.SetDefaultModifierID(defaultModifier.ID);
        defaultFakeSetup.App.AddModCategory(HubInfo.ModCategories.Apps);
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
        await adminUser.AddRole(adminRole);
        return adminUser;
    }
}