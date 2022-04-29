using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XTI_App.Abstractions;
using XTI_App.Secrets;
using XTI_Core;
using XTI_Credentials;
using XTI_Hub;
using XTI_Hub.Abstractions;
using XTI_HubAppApi;
using XTI_HubDB.EF;

namespace HubSetupApp;

public sealed class SetupHostedService : IHostedService
{
    private readonly IServiceProvider services;

    public SetupHostedService(IServiceProvider services)
    {
        this.services = services;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = services.CreateScope();
        try
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<HubDbContext>();
            await dbContext.Database.MigrateAsync();
            await addOrUpdateApp(scope);
            await addSystemUser(scope);
            await runSetup(scope);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            Environment.ExitCode = 999;
        }
        var lifetime = scope.ServiceProvider.GetRequiredService<IHostApplicationLifetime>();
        lifetime.StopApplication();
    }

    private static Task addOrUpdateApp(IServiceScope scope)
    {
        var appFactory = scope.ServiceProvider.GetRequiredService<AppFactory>();
        var options = scope.ServiceProvider.GetRequiredService<SetupOptions>();
        var clock = scope.ServiceProvider.GetRequiredService<IClock>();
        return appFactory.Apps.AddOrUpdate
        (
            new AppVersionName(options.VersionName),
            HubInfo.AppKey,
            clock.Now()
        );
    }

    private static async Task addSystemUser(IServiceScope scope)
    {
        var appFactory = scope.ServiceProvider.GetRequiredService<AppFactory>();
        var hashedPasswordFactory = scope.ServiceProvider.GetRequiredService<IHashedPasswordFactory>();
        var clock = scope.ServiceProvider.GetRequiredService<IClock>();
        var password = Guid.NewGuid().ToString();
        var systemUser = await appFactory.SystemUsers.AddOrUpdateSystemUser
        (
            HubInfo.AppKey,
            Environment.MachineName,
            hashedPasswordFactory.Create(password),
            clock.Now()
        );
        var systemUserCredentials = scope.ServiceProvider.GetRequiredService<SystemUserCredentials>();
        await systemUserCredentials.Update
        (
            new CredentialValue
            (
                systemUser.UserName().Value,
                password
            )
        );
    }

    private static async Task runSetup(IServiceScope scope)
    {
        var hubSetup = scope.ServiceProvider.GetRequiredService<HubAppSetup>();
        var options = scope.ServiceProvider.GetRequiredService<SetupOptions>();
        var versionKey = string.IsNullOrWhiteSpace(options.VersionKey)
            ? AppVersionKey.Current
            : AppVersionKey.Parse(options.VersionKey);
        await hubSetup.Run(versionKey);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}