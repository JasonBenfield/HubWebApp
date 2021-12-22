using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using XTI_App.Abstractions;

namespace XTI_AppSetupApp.Extensions;

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
        var defaultSetup = scope.ServiceProvider.GetRequiredService<DefaultAppSetup>();
        var options = scope.ServiceProvider.GetRequiredService<IOptions<SetupOptions>>().Value;
        var versionKey = string.IsNullOrWhiteSpace(options.VersionKey)
            ? AppVersionKey.Current
            : AppVersionKey.Parse(options.VersionKey);
        await defaultSetup.Run(versionKey);
        var additionalSetup = scope.ServiceProvider.GetRequiredService<IAppSetup>();
        if (additionalSetup != null)
        {
            await additionalSetup.Run(versionKey);
        }
        var lifetime = scope.ServiceProvider.GetRequiredService<IHostApplicationLifetime>();
        lifetime.StopApplication();
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}