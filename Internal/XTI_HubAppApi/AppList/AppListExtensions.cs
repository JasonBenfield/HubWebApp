using Microsoft.Extensions.DependencyInjection;
using XTI_HubAppApi.AppList;

namespace XTI_HubAppApi
{
    internal static class AppListExtensions
    {
        public static void AddAppListGroupServices(this IServiceCollection services)
        {
            services.AddScoped<IndexAction>();
            services.AddScoped<GetAllAppsAction>();
            services.AddScoped<GetAppModifierKeyAction>();
            services.AddScoped<RedirectToAppAction>();
        }
    }
}
