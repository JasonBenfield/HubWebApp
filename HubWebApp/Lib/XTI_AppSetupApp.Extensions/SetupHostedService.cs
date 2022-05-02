using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XTI_App.Abstractions;

namespace XTI_AppSetupApp.Extensions;

public sealed class SetupHostedService : IHostedService
{
    private readonly IServiceProvider sp;

    public SetupHostedService(IServiceProvider sp)
    {
        this.sp = sp;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = sp.CreateScope();
        try
        {
            var defaultSetup = scope.ServiceProvider.GetRequiredService<DefaultAppSetup>();
            var options = scope.ServiceProvider.GetRequiredService<SetupOptions>();
            var appKey = scope.ServiceProvider.GetRequiredService<AppKey>();
            var versionKey = string.IsNullOrWhiteSpace(options.VersionKey)
                ? AppVersionKey.Current
                : AppVersionKey.Parse(options.VersionKey);
            Console.WriteLine($"Running Default Setup for {appKey.Name.DisplayText} {appKey.Type.DisplayText} {versionKey.DisplayText}");
            await defaultSetup.Run(versionKey);
            var additionalSetup = scope.ServiceProvider.GetService<IAppSetup>();
            if (additionalSetup != null)
            {
                Console.WriteLine("Running Additional Setup");
                await additionalSetup.Run(versionKey);
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            Environment.ExitCode = 999;
        }
        var lifetime = scope.ServiceProvider.GetRequiredService<IHostApplicationLifetime>();
        lifetime.StopApplication();
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}