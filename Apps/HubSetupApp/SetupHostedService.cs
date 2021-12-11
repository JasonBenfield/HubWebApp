using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;
using XTI_App.Abstractions;
using XTI_HubAppApi;
using XTI_HubDB.EF;
using XTI_HubDB.Entities;
using XTI_HubSetup;

namespace HubSetupApp
{
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
            var dbContext = scope.ServiceProvider.GetService<HubDbContext>();
            await dbContext.Database.MigrateAsync();
            var hubSetup = scope.ServiceProvider.GetService<HubAppSetup>();
            var options = scope.ServiceProvider.GetService<IOptions<SetupOptions>>().Value;
            var versionKey = string.IsNullOrWhiteSpace(options.VersionKey)
                ? AppVersionKey.Current
                : AppVersionKey.Parse(options.VersionKey);
            await hubSetup.Run(versionKey);
            var lifetime = scope.ServiceProvider.GetService<IHostApplicationLifetime>();
            lifetime.StopApplication();
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
