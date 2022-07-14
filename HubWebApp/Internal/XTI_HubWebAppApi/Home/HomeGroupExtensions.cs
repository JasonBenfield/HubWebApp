using XTI_HubWebAppApi.Home;

namespace XTI_HubWebAppApi;

internal static class HomeGroupExtensions
{
    public static void RegisterHomeGroupServices(this IServiceCollection services)
    {
        services.AddScoped<IndexAction>();
    }
}
