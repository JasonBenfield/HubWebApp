using XTI_HubWebAppApiActions.Home;

// Generated Code
namespace XTI_HubWebAppApi;
internal static partial class HomeGroupExtensions
{
    internal static void AddHomeServices(this IServiceCollection services)
    {
        services.AddScoped<IndexAction>();
    }
}