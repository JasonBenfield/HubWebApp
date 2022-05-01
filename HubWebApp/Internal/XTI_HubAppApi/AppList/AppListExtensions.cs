using XTI_HubAppApi.AppList;

namespace XTI_HubAppApi;

internal static class AppListExtensions
{
    public static void AddAppListGroupServices(this IServiceCollection services)
    {
        services.AddScoped<IndexAction>();
        services.AddScoped<GetAppsAction>();
        services.AddScoped<GetAppByAppKeyAction>();
        services.AddScoped<RedirectToAppAction>();
        services.AddScoped<GetAppDomainsAction>();
    }
}