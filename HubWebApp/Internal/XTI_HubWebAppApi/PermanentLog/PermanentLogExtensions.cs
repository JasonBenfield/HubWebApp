using XTI_HubWebAppApi.PermanentLog;

namespace XTI_HubWebAppApi;

internal static class PermanentLogExtensions
{
    public static void AddPermanentLogGroupServices(this IServiceCollection services)
    {
        services.AddScoped<LogBatchAction>();
        services.AddScoped<EndExpiredSessionsAction>();
    }
}