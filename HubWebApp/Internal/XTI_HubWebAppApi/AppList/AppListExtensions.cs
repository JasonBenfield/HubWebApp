using XTI_HubWebAppApi.AppList;

namespace XTI_HubWebAppApi;

internal static class AppListExtensions
{
    public static void AddAppListGroupServices(this IServiceCollection services)
    {
        services.AddScoped<IndexAction>();
        services.AddScoped<GetAppsAction>();
        services.AddScoped<GetAppDomainsAction>();
    }
}