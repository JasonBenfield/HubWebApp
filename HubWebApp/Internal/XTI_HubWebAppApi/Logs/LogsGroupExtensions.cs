using XTI_HubWebAppApi.Logs;

namespace XTI_HubWebAppApi;

internal static class LogsGroupExtensions
{
    public static void AddLogsGroupServices(this IServiceCollection services)
    {
        services.AddScoped<SessionViewAction>();
        services.AddScoped<RequestViewAction>();
        services.AddScoped<LogEntryViewAction>();
        services.AddScoped<SessionQueryAction>();
        services.AddScoped<RequestQueryAction>();
    }
}