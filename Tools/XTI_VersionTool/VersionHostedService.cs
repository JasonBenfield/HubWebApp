using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using XTI_Version;
using XTI_VersionToolApi;

namespace XTI_VersionTool;

internal sealed class VersionHostedService : IHostedService
{
    private readonly IServiceProvider services;

    public VersionHostedService(IServiceProvider services)
    {
        this.services = services;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine($"AppContext.BaseDirectory: '{AppContext.BaseDirectory}'");
        using var scope = services.CreateScope();
        try
        {
            var commandFactory = scope.ServiceProvider.GetRequiredService<VersionCommandFactory>();
            var options = scope.ServiceProvider.GetRequiredService<IOptions<VersionToolOptions>>().Value;
            var commandName = VersionCommandName.FromValue(options.Command);
            var command = commandFactory.Create(commandName);
            await command.Execute(options);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            Environment.ExitCode = 999;
        }
        var lifetime = scope.ServiceProvider.GetRequiredService<IHostApplicationLifetime>();
        lifetime.StopApplication();
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}