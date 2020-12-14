using HubWebApp.Api;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using XTI_App;

namespace HubSetupConsoleApp
{
    public sealed class HubSetupService : IHostedService
    {
        private readonly IHostApplicationLifetime lifetime;
        private readonly AllAppSetup appSetup;
        private readonly HubSetup hubSetup;

        public HubSetupService(IHostApplicationLifetime lifetime, AllAppSetup appSetup, HubSetup hubSetup)
        {
            this.lifetime = lifetime;
            this.appSetup = appSetup;
            this.hubSetup = hubSetup;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                await appSetup.Run();
                await hubSetup.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Environment.ExitCode = 999;
            }
            lifetime.StopApplication();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
