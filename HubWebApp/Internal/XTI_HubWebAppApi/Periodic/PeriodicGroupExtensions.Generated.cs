using XTI_HubWebAppApiActions.Periodic;

// Generated Code
namespace XTI_HubWebAppApi;
internal static partial class PeriodicGroupExtensions
{
    internal static void AddPeriodicServices(this IServiceCollection services)
    {
        services.AddScoped<DeactivateUsersAction>();
        services.AddScoped<DeleteExpiredStoredObjectsAction>();
        services.AddScoped<EndExpiredSessionsAction>();
        services.AddScoped<PurgeLogsAction>();
    }
}