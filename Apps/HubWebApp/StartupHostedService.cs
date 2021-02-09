using HubWebAppApi;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using XTI_App;

namespace HubWebApp
{
    public sealed class StartupHostedService : IHostedService
    {
        private readonly IServiceProvider sp;

        public StartupHostedService(IServiceProvider sp)
        {
            this.sp = sp;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = sp.CreateScope();
            var hubSetup = scope.ServiceProvider.GetService<HubSetup>();
            await hubSetup.Run(AppVersionKey.Current);
            await StopAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
