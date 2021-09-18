using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XTI_App.Api;
using XTI_Core;
using XTI_HubAppClient;
using XTI_HubAppClient.Extensions;
using XTI_ServiceApp.Extensions;
using XTI_TempLog;
using XTI_TempLog.Api;
using XTI_TempLog.Extensions;

namespace TempLogServiceApp.Extensions
{
    public static class Extensions
    {
        public static void AddTempLogServiceAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(_ => TempLogAppKey.AppKey);
            services.AddSingleton<IAppApiUser, AppApiSuperUser>();
            services.AddXtiServiceAppServices(configuration);
            services.Configure<LogOptions>(configuration.GetSection(LogOptions.Log));
            services.AddHubClientServices();
            services.AddScoped<ISourceAppContext>(sp => sp.GetService<HubClientAppContext>());
            services.AddScoped<ISourceUserContext>(sp => sp.GetService<HubClientUserContext>());
            services.AddScoped<TempLogs>(sp =>
            {
                var dataProtector = sp.GetDataProtector("XTI_TempLog");
                var hostEnv = sp.GetService<IHostEnvironment>();
                var appDataFolder = new AppDataFolder()
                    .WithHostEnvironment(hostEnv);
                return new DiskTempLogs(dataProtector, appDataFolder.Path(), "TempLogs");
            });
            services.AddScoped<AppApiFactory, TempLogApiFactory>();
            services.AddScoped(sp => sp.GetService<TempLogSetup>());
            services.AddScoped(sp => (TempLogApi)sp.GetService<IAppApi>());
        }
    }
}
