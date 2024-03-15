using XTI_HubWebAppApi.ExternalAuth;

namespace XTI_HubWebAppApi;

internal static class ExternalAuthExtensions
{
    public static void AddExternalAuthGroupServices(this IServiceCollection services)
    {
        services.AddScoped<ExternalAuthKeyAction>();
    }
}