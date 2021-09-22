using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.WindowsServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XTI_Configuration.Extensions;

namespace LocalInstallService
{
    class Program
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        static void Main(string[] args)
            => WebHost.CreateDefaultBuilder(args)
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
                        services.AddSingleton<Installer>();
                    }
                )
                .UseUrls("http://*:61862")
                .Configure(app =>
                {
                    app.Run((context) =>
                    {
                        var installer = context.RequestServices.GetService<Installer>();
                        return installer.Run(context);
                    });
                })
                .Build()
                .RunAsService();
    }
}
