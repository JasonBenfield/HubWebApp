using XTI_HubWebAppApi.Periodic;

namespace XTI_HubWebAppApi;

internal static class PeriodicGroupExtensions
{
    public static void AddPeriodicGroupServices(this IServiceCollection services)
    {
        services.AddScoped<DeleteExpiredStoredObjectsAction>();
        services.AddScoped<EndExpiredSessionsAction>();
        services.AddScoped<PurgeLogsAction>();
    }
}