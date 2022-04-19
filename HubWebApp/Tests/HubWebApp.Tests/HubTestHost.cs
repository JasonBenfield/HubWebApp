using HubWebApp.Fakes;
using Microsoft.Extensions.Hosting;
using XTI_Core;
using XTI_Core.Extensions;
using XTI_Core.Fakes;
using XTI_Hub.Abstractions;

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
        var setup = sp.GetRequiredService<IAppSetup>();
        await setup.Run(AppVersionKey.Current);
        var defaultFakeSetup = sp.GetRequiredService<DefaultFakeSetup>();
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

        var userContext = sp.GetRequiredService<FakeUserContext>();
        var adminUser = await addAdminUser(sp);
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