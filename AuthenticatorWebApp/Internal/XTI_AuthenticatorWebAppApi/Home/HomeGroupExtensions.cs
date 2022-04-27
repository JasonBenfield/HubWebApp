using XTI_AuthenticatorWebAppApi.Home;

namespace XTI_AuthenticatorWebAppApi;

internal static class HomeGroupExtensions
{
    public static void AddHomeGroupServices(this IServiceCollection services)
    {
        services.AddScoped<IndexAction>();
    }
}