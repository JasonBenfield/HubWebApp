using XTI_HubAppApi.PermanentLog;

namespace XTI_HubAppApi;

internal static class PermanentLogExtensions
{
    public static void AddPermanentLogGroupServices(this IServiceCollection services)
    {
        services.AddScoped<LogBatchAction>();
        services.AddScoped<EndExpiredSessionsAction>();
    }
}