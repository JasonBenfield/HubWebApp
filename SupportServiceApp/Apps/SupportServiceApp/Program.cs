using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XTI_App.Api;
using XTI_Core;
using XTI_Core.Extensions;
using XTI_Hub;
using XTI_HubAppClient.ServiceApp.Extensions;
using XTI_HubDB.Extensions;
using XTI_PermanentLog;
using XTI_PermanentLog.Implementations;
using XTI_SupportServiceAppApi;
using XTI_TempLog;
using XTI_TempLog.Extensions;

var hostBuilder = XtiServiceAppHost.CreateDefault(SupportAppKey.Value, args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddSupportAppApiServices();
        services.AddScoped<AppApiFactory, SupportAppApiFactory>();
        services.AddScoped(sp => (SupportAppApi)sp.GetRequiredService<IAppApi>());
        services.AddScoped<ITempLogsV1>(sp =>
        {
            var dataProtector = sp.GetDataProtector("XTI_TempLog");
            var appDataFolder = sp.GetRequiredService<XtiFolder>().AppDataFolder();
            return new DiskTempLogsV1(dataProtector, appDataFolder.Path(), "TempLogs");
        });
        services.AddConfigurationOptions<SupportServiceAppOptions>();
        services.AddHubDbContextForSqlServer();
        services.AddScoped<HubFactory>();
        services.AddScoped<EfPermanentLog>();
        services.AddScoped<HcPermanentLog>();
        services.AddScoped
        (
            sp =>
            {
                IPermanentLog permanentLog;
                var options = sp.GetRequiredService<SupportServiceAppOptions>();
                if (options.PermanentLogType == "DB")
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
        services.AddScoped<TempToPermanentLogV1>();
    });
if (args.Length <= 0 || !args[0].Equals("RunAsConsole", StringComparison.OrdinalIgnoreCase))
{
    hostBuilder.UseWindowsService();
}
var host = hostBuilder.Build();
await host.RunAsync();