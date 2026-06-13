using XTI_HubWebAppApiActions.Commands;

// Generated Code
namespace XTI_HubWebAppApi;
internal static partial class CommandsGroupExtensions
{
    internal static void AddCommandsServices(this IServiceCollection services)
    {
        services.AddScoped<GetCommandsInProgressAction>();
        services.AddScoped<GetPendingCommandsAction>();
        services.AddScoped<IndexAction>();
    }
}