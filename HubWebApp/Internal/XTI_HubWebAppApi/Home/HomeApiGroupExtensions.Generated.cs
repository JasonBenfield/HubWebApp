using XTI_HubWebAppApiActions.Home;

// Generated Code
namespace XTI_HubWebAppApi;
internal static partial class HomeApiGroupExtensions
{
    internal static void AddHomeServices(this IServiceCollection services)
    {
        services.AddScoped<IndexAction>();
    }
}