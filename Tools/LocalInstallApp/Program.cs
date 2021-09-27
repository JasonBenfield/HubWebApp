﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using XTI_Configuration.Extensions;
using XTI_Core;
using XTI_Secrets.Extensions;

namespace LocalInstallApp
{
    class Program
    {
        static Task Main(string[] args)
            => Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration
                (
                    (hostContext, configuration) =>
                    {
                        configuration.UseXtiConfiguration(hostContext.HostingEnvironment, args);
                    }
                )
                .ConfigureServices
                (
                    (hostContext, services) =>
                    {
                        services.Configure<InstallOptions>(hostContext.Configuration);
                        services.AddSingleton<XtiFolder>();
                        services.AddScoped<GitFactory>();
                        services.AddFileSecretCredentials(hostContext.HostingEnvironment);
                        services.AddHostedService<InstallHostedService>();
                    }
                )
                .Build()
                .RunAsync();
    }
}
