using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XTI_App.Abstractions;
using XTI_App.Secrets;
using XTI_Core;
using XTI_Credentials;
using XTI_Hub;
using XTI_HubDB.EF;
using XTI_HubSetup;

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

    private static async Task addSystemUser(IServiceScope scope)
    {
        var appFactory = scope.ServiceProvider.GetRequiredService<AppFactory>();
        var hashedPasswordFactory = scope.ServiceProvider.GetRequiredService<IHashedPasswordFactory>();
        var clock = scope.ServiceProvider.GetRequiredService<IClock>();
        var password = Guid.NewGuid().ToString();
        var options = scope.ServiceProvider.GetRequiredService<SetupOptions>();
        var systemUser = await appFactory.SystemUsers.AddOrUpdateSystemUser
        (
            HubInfo.AppKey,
            Environment.MachineName,
            options.Domain,
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