using XTI_HubDB.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XTI_Hub;
using XTI_App.Abstractions;
using XTI_Core;
using XTI_DB;
using XTI_Secrets.Extensions;
using XTI_App.Api;

namespace XTI_Tool.Extensions
{
    public static class ToolExtensions
    {
        public static void AddHubToolServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<XtiFolder>();
            services.Configure<AppOptions>(configuration.GetSection(AppOptions.App));
            services.Configure<DbOptions>(configuration.GetSection(DbOptions.DB));
            services.AddHubDbContextForSqlServer(configuration);
            services.AddScoped<AppFactory>();
            services.AddScoped<Clock, UtcClock>();
            services.AddScoped<InstallationProcess>();
        }
    }
}
