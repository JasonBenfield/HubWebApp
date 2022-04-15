using LocalInstallService;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting.WindowsServices;
using XTI_Core;
using XTI_Core.Extensions;

#pragma warning disable CA1416 // Validate platform compatibility
WebHost.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration
    (
        (hostContext, configuration) =>
        {
            configuration.UseXtiConfiguration(hostContext.HostingEnvironment, "", "", args);
        }
    )
    .ConfigureServices
    (
        (hostContext, services) =>
        {
            services.AddSingleton(_ => XtiEnvironment.Parse(hostContext.HostingEnvironment.EnvironmentName));
            services.AddSingleton<Installer>();
        }
    )
    .UseUrls("http://*:61862")
    .Configure(app =>
    {
        app.Run((context) =>
        {
            var installer = context.RequestServices.GetRequiredService<Installer>();
            return installer.Run(context);
        });
    })
    .Build()
    .RunAsService();
#pragma warning restore CA1416 // Validate platform compatibility