using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using XTI_App.Abstractions;
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
            var hubSetup = scope.ServiceProvider.GetRequiredService<HubAppSetup>();
            var options = scope.ServiceProvider.GetRequiredService<IOptions<SetupOptions>>().Value;
            var versionKey = string.IsNullOrWhiteSpace(options.VersionKey)
                ? AppVersionKey.Current
                : AppVersionKey.Parse(options.VersionKey);
            await hubSetup.Run(versionKey);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex);
            Environment.ExitCode = 999;
        }
        var lifetime = scope.ServiceProvider.GetRequiredService<IHostApplicationLifetime>();
        lifetime.StopApplication();
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}