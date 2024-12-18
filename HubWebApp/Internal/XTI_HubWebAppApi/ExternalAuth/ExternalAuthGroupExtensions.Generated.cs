using XTI_HubWebAppApiActions.ExternalAuth;

// Generated Code
namespace XTI_HubWebAppApi;
internal static partial class ExternalAuthGroupExtensions
{
    internal static void AddExternalAuthServices(this IServiceCollection services)
    {
        services.AddScoped<ExternalAuthKeyAction>();
    }
}