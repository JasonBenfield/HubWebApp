using XTI_HubWebAppApiActions.Command;

// Generated Code
namespace XTI_HubWebAppApi;
internal static partial class CommandGroupExtensions
{
    internal static void AddCommandServices(this IServiceCollection services)
    {
        services.AddScoped<IndexAction>();
    }
}