using XTI_HubWebAppApi.Logs;

namespace XTI_HubWebAppApi;

internal static class LogsGroupExtensions
{
    public static void AddLogsGroupServices(this IServiceCollection services)
    {
        services.AddScoped<GetEventByKeyAction>();
        services.AddScoped<GetLogEntryDetailAction>();
        services.AddScoped<GetRequestDetailAction>();
        services.AddScoped<GetSessionDetailAction>();
        services.AddScoped<LogEntryQueryAction>();
        services.AddScoped<LogEntriesViewAction>();
        services.AddScoped<LogEntryViewAction>();
        services.AddScoped<RequestQueryAction>();
        services.AddScoped<RequestsViewAction>();
        services.AddScoped<RequestViewAction>();
        services.AddScoped<SessionQueryAction>();
        services.AddScoped<SessionsViewAction>();
        services.AddScoped<SessionViewAction>();
    }
}