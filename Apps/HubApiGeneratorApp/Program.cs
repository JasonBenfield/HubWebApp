﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using XTI_ApiGeneratorApp.Extensions;
using XTI_App.Api;
using XTI_Configuration.Extensions;
using HubWebApp.Api;

namespace HubApiGeneratorApp
{
    class Program
    {
        static Task Main(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.UseXtiConfiguration(hostingContext.HostingEnvironment, args);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddApiGenerator(hostContext.Configuration);
                    services.AddScoped<AppApiFactory, HubAppApiFactory>();
                    services.AddHostedService<ApiGeneratorHostedService>();
                })
                .RunConsoleAsync();
        }
    }
}
