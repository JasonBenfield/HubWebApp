using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using XTI_App.Abstractions;
using XTI_HubAppApi;
using XTI_Processes;
using XTI_PublishTool;
using XTI_SupportAppApi;
using XTI_VersionToolApi;

namespace PublishApp
{
    public sealed class PublishHostedService : IHostedService
    {
        private readonly IServiceProvider services;

        public PublishHostedService(IServiceProvider services)
        {
            this.services = services;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = services.CreateScope();
            var sp = scope.ServiceProvider;
            try
            {
                var hostEnv = sp.GetService<IHostEnvironment>();
                var hubApi = sp.GetService<HubAppApi>();
                var publishProcess = new PublishProcess(hostEnv, hubApi);
                var options = sp.GetService<IOptions<PublishOptions>>().Value;
                var appKey = new AppKey(new AppName(options.AppName), AppType.Values.Value(options.AppType));
                await publishProcess.Run(appKey, options.AppsToImport, options.RepoOwner, options.RepoName);
                if (!appKey.Type.Equals(AppType.Values.Package))
                {
                    if (!options.NoInstall)
                    {
                        await publishProcess.RunInstall(appKey, options.DestinationMachine);
                    }
                    if (appKey.Equals(HubInfo.AppKey))
                    {
                        await publishProcess.Run(SupportInfo.AppKey, "", options.RepoOwner, options.RepoName);
                        if (!options.NoInstall)
                        {
                            await publishProcess.RunInstall(appKey, options.DestinationMachine);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Environment.ExitCode = 999;
            }
            var lifetime = scope.ServiceProvider.GetService<IHostApplicationLifetime>();
            lifetime.StopApplication();
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
