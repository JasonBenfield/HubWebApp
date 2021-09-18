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
using XTI_SupportAppApi;
using XTI_TempLog.Extensions;

namespace SupportServiceApp.Extensions
{
    public static class Extensions
    {
        public static void AddSupportServiceAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(_ => SupportInfo.AppKey);
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
            services.AddScoped<AppApiFactory, SupportAppApiFactory>();
            services.AddScoped(sp => (SupportAppApi)sp.GetService<IAppApi>());
        }
    }
}
