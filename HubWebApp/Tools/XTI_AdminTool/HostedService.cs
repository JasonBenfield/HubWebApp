using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XTI_Admin;
using XTI_DB;
using XTI_HubDB.EF;

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
            var hubDbTypeAccessor = scope.ServiceProvider.GetRequiredService<HubDbTypeAccessor>();
            if(hubDbTypeAccessor.Value == HubAdministrationTypes.DB)
            {
                var dbAdmin = scope.ServiceProvider.GetRequiredService<DbAdmin<HubDbContext>>();
                await dbAdmin.Update();
            }
            var commandFactory = scope.ServiceProvider.GetRequiredService<CommandFactory>();
            var options = scope.ServiceProvider.GetRequiredService<AdminOptions>();
            var command = commandFactory.CreateCommand(options);
            await command.Execute();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            Environment.ExitCode = 999;
        }
        var lifetime = sp.GetRequiredService<IHostApplicationLifetime>();
        lifetime.StopApplication();
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}