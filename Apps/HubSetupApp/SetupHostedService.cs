using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;
using XTI_App.Abstractions;
using XTI_HubAppApi;

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
            var defaultSetup = scope.ServiceProvider.GetService<HubAppSetup>();
            var options = scope.ServiceProvider.GetService<IOptions<SetupOptions>>().Value;
            var versionKey = string.IsNullOrWhiteSpace(options.VersionKey)
                ? AppVersionKey.Current
                : AppVersionKey.Parse(options.VersionKey);
            await defaultSetup.Run(versionKey);
            var lifetime = scope.ServiceProvider.GetService<IHostApplicationLifetime>();
            lifetime.StopApplication();
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
