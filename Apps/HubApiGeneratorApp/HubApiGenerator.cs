using HubWebApp.ApiTemplate;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using XTI_ApiGeneratorApp.Extensions;

namespace HubApiGeneratorApp
{
    public sealed class HubApiGenerator : IHostedService
    {
        public HubApiGenerator(IHostApplicationLifetime lifetime, ApiGenerator apiGenerator)
        {
            this.lifetime = lifetime;
            this.apiGenerator = apiGenerator;
        }

        private readonly IHostApplicationLifetime lifetime;
        private readonly ApiGenerator apiGenerator;

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                var apiTemplateFactory = new HubAppApiTemplateFactory();
                var apiTemplate = apiTemplateFactory.Create();
                await apiGenerator.Execute(apiTemplate);
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
