﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XTI_Core;
using XTI_DB;
using XTI_Hub;
using XTI_HubDB.EF;
using XTI_HubDB.Entities;

namespace HubDbTool;

internal sealed class HostedService : IHostedService
{
    private readonly IServiceProvider sp;

    public HostedService(IServiceProvider sp)
    {
        this.sp = sp;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = sp.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IHubDbContext>();
        var options = scope.ServiceProvider.GetRequiredService<HubDbToolOptions>();
        var xtiEnv = scope.ServiceProvider.GetRequiredService<XtiEnvironment>();
        try
        {
            if (options.Command == "reset")
            {
                if (!xtiEnv.IsTest() && !options.Force)
                {
                    throw new ArgumentException("Database reset can only be run for the test environment");
                }
                var dbAdmin = scope.ServiceProvider.GetRequiredService<DbAdmin<HubDbContext>>();
                await dbAdmin.Reset();
            }
            else if (options.Command == "backup")
            {
                if (string.IsNullOrWhiteSpace(options.BackupFilePath))
                {
                    throw new ArgumentException("Backup file path is required for backup");
                }
                var dbAdmin = scope.ServiceProvider.GetRequiredService<DbAdmin<HubDbContext>>();
                await dbAdmin.BackupTo(options.BackupFilePath);
            }
            else if (options.Command == "restore")
            {
                if (xtiEnv.IsProduction())
                {
                    throw new ArgumentException("Database restore cannot be run for the production environment");
                }
                if (string.IsNullOrWhiteSpace(options.BackupFilePath))
                {
                    throw new ArgumentException("Backup file path is required for restore");
                }
                var dbAdmin = scope.ServiceProvider.GetRequiredService<DbAdmin<HubDbContext>>();
                await dbAdmin.RestoreFrom(options.BackupFilePath);
            }
            else if (options.Command == "update")
            {
                var dbAdmin = scope.ServiceProvider.GetRequiredService<DbAdmin<HubDbContext>>();
                await dbAdmin.Update();
                var setup = scope.ServiceProvider.GetRequiredService<InitialSetup>();
                await setup.Run();
            }
            else
            {
                throw new NotSupportedException($"Command '{options.Command}' is not supported");
            }
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