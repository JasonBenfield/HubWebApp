using XTI_HubWebAppApiActions.Logs;

// Generated Code
namespace XTI_HubWebAppApi;
internal static partial class LogsApiGroupExtensions
{
    internal static void AddLogsServices(this IServiceCollection services)
    {
        services.AddScoped<AppRequestsViewAction>();
        services.AddScoped<AppRequestViewAction>();
        services.AddScoped<GetLogEntryDetailAction>();
        services.AddScoped<GetLogEntryOrDefaultByKeyAction>();
        services.AddScoped<GetRequestDetailAction>();
        services.AddScoped<GetSessionDetailAction>();
        services.AddScoped<LogEntriesViewAction>();
        services.AddScoped<LogEntryViewAction>();
        services.AddScoped<SessionsViewAction>();
        services.AddScoped<SessionViewAction>();
    }
}