using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XTI_App.Api;
using XTI_Core.Extensions;
using XTI_Hub;
using XTI_HubAppClient.ServiceApp.Extensions;
using XTI_HubDB.Extensions;
using XTI_PermanentLog;
using XTI_PermanentLog.Implementations;
using XTI_SupportServiceAppApi;

var hostBuilder = XtiServiceAppHost.CreateDefault(SupportAppKey.Value, args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddSupportAppApiServices();
        services.AddScoped<AppApiFactory, SupportAppApiFactory>();
        services.AddScoped(sp => (SupportAppApi)sp.GetRequiredService<IAppApi>());
        services.AddConfigurationOptions<SupportServiceAppOptions>();
        services.AddHubDbContextForSqlServer();
        services.AddScoped<EfHubDB>();
        services.AddScoped<EfPermanentLog>();
        services.AddScoped<HcPermanentLog>();
        services.AddScoped
        (
            sp =>
            {
                IPermanentLog permanentLog;
                var options = sp.GetRequiredService<SupportServiceAppOptions>();
                if (options.PermanentLogType.Equals("DB", StringComparison.OrdinalIgnoreCase))
                {
                    permanentLog = sp.GetRequiredService<EfPermanentLog>();
                }
                else
                {
                    permanentLog = sp.GetRequiredService<HcPermanentLog>();
                }
                return permanentLog;
            }
        );
        services.AddScoped<TempToPermanentLog>();
    });
if (args.Length <= 0 || !args[0].Equals("RunAsConsole", StringComparison.OrdinalIgnoreCase))
{
    hostBuilder.UseWindowsService();
}
var host = hostBuilder.Build();
await host.RunAsync();