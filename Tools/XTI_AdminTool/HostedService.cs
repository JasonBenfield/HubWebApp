using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XTI_Admin;

namespace XTI_AdminTool;

internal sealed class HostedService : IHostedService
{
    private readonly IServiceProvider sp;

    public HostedService(IServiceProvider sp)
    {
        this.sp = sp;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            using var scope = sp.CreateScope();
            var appKeys = scope.ServiceProvider.GetRequiredService<SelectedAppKeys>().Values;
            var joinedAppKeys = string.Join(", ", appKeys.Select(ak => ak.Serialize()));
            Console.WriteLine($"App Keys: {joinedAppKeys}");
            var commandFactory = scope.ServiceProvider.GetRequiredService<CommandFactory>();
            var options = scope.ServiceProvider.GetRequiredService<AdminOptions>();
            var command = commandFactory.CreateCommand(options);
            await command.Execute();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        var lifetime = sp.GetRequiredService<IHostApplicationLifetime>();
        lifetime.StopApplication();
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}