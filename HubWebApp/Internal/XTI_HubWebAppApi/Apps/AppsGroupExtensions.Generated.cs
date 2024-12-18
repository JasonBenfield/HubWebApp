using XTI_HubWebAppApiActions.AppList;

// Generated Code
namespace XTI_HubWebAppApi;
internal static partial class AppsGroupExtensions
{
    internal static void AddAppsServices(this IServiceCollection services)
    {
        services.AddScoped<GetAppDomainsAction>();
        services.AddScoped<GetAppsAction>();
        services.AddScoped<IndexAction>();
    }
}