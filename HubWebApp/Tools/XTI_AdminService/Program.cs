using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting.WindowsServices;
using XTI_Core;
using XTI_Core.Extensions;
using XTI_AdminService;

#pragma warning disable CA1416 // Validate platform compatibility
var host = WebHost.CreateDefaultBuilder(args)
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
            services.AddSingleton<AdminToolRunner>();
        }
    )
    .UseUrls("http://*:61862")
    .Configure(app =>
    {
        app.Run((context) =>
        {
            var runner = context.RequestServices.GetRequiredService<AdminToolRunner>();
            return runner.Run(context);
        });
    })
    .Build();
if (args.Length > 0 && args[0].Equals("mode=console", StringComparison.OrdinalIgnoreCase))
{
    host.Run();
}
else
{
    host.RunAsService();
}
#pragma warning restore CA1416 // Validate platform compatibility