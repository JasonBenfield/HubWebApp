using XTI_HubWebAppApiActions.Logs;

// Generated Code
#nullable enable
namespace XTI_HubWebAppApi.Logs;
public sealed partial class LogsGroupBuilder
{
    private readonly AppApiGroup source;
    internal LogsGroupBuilder(AppApiGroup source)
    {
        this.source = source;
        AppRequest = source.AddAction<AppRequestRequest, WebViewResult>("AppRequest").WithExecution<AppRequestPage>();
        AppRequests = source.AddAction<AppRequestQueryRequest, WebViewResult>("AppRequests").WithExecution<AppRequestsPage>();
        GetLogEntryDetail = source.AddAction<int, AppLogEntryDetailModel>("GetLogEntryDetail").WithExecution<GetLogEntryDetailAction>();
        GetLogEntryOrDefaultByKey = source.AddAction<string, AppLogEntryModel>("GetLogEntryOrDefaultByKey").WithExecution<GetLogEntryOrDefaultByKeyAction>();
        GetRequestDetail = source.AddAction<int, AppRequestDetailModel>("GetRequestDetail").WithExecution<GetRequestDetailAction>();
        GetSessionDetail = source.AddAction<int, AppSessionDetailModel>("GetSessionDetail").WithExecution<GetSessionDetailAction>();
        LogEntries = source.AddAction<LogEntryQueryRequest, WebViewResult>("LogEntries").WithExecution<LogEntriesPage>();
        LogEntry = source.AddAction<LogEntryRequest, WebViewResult>("LogEntry").WithExecution<LogEntryPage>();
        Session = source.AddAction<SessionViewRequest, WebViewResult>("Session").WithExecution<SessionPage>();
        Sessions = source.AddAction<EmptyRequest, WebViewResult>("Sessions").WithExecution<SessionsPage>();
        Configure();
    }

    partial void Configure();
    public AppApiActionBuilder<AppRequestRequest, WebViewResult> AppRequest { get; }
    public AppApiActionBuilder<AppRequestQueryRequest, WebViewResult> AppRequests { get; }
    public AppApiActionBuilder<int, AppLogEntryDetailModel> GetLogEntryDetail { get; }
    public AppApiActionBuilder<string, AppLogEntryModel> GetLogEntryOrDefaultByKey { get; }
    public AppApiActionBuilder<int, AppRequestDetailModel> GetRequestDetail { get; }
    public AppApiActionBuilder<int, AppSessionDetailModel> GetSessionDetail { get; }
    public AppApiActionBuilder<LogEntryQueryRequest, WebViewResult> LogEntries { get; }
    public AppApiActionBuilder<LogEntryRequest, WebViewResult> LogEntry { get; }
    public AppApiActionBuilder<SessionViewRequest, WebViewResult> Session { get; }
    public AppApiActionBuilder<EmptyRequest, WebViewResult> Sessions { get; }

    public LogsGroup Build() => new LogsGroup(source, this);
}