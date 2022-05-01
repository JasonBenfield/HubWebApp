using XTI_HubAppApi.Home;

namespace XTI_HubAppApi;

internal static class HomeGroupExtensions
{
    public static void RegisterHomeGroupServices(this IServiceCollection services)
    {
        services.AddScoped<IndexAction>();
    }
}
