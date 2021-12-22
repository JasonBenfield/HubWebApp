using BuildWebApp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XTI_Configuration.Extensions;
using XTI_Core;

await Host.CreateDefaultBuilder(args)
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
            services.Configure<BuildOptions>(hostContext.Configuration);
            services.AddSingleton<XtiFolder>();
            services.AddHostedService<BuildHostedService>();
        }
    )
    .Build()
    .RunAsync();