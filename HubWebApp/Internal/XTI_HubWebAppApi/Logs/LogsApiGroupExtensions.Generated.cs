using XTI_HubWebAppApiActions.Logs;

// Generated Code
namespace XTI_HubWebAppApi;
internal static partial class LogsApiGroupExtensions
{
    internal static void AddLogsServices(this IServiceCollection services)
    {
        services.AddScoped<AppRequestPage>();
        services.AddScoped<AppRequestsPage>();
        services.AddScoped<GetLogEntryDetailAction>();
        services.AddScoped<GetLogEntryOrDefaultByKeyAction>();
        services.AddScoped<GetRequestDetailAction>();
        services.AddScoped<GetSessionDetailAction>();
        services.AddScoped<LogEntriesPage>();
        services.AddScoped<LogEntryPage>();
        services.AddScoped<SessionPage>();
        services.AddScoped<SessionsPage>();
    }
}