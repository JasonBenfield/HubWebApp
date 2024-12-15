using XTI_HubWebAppApiActions.Logs;

// Generated Code
#nullable enable
namespace XTI_HubWebAppApi.Logs;
public sealed partial class LogsGroup : AppApiGroupWrapper
{
    internal LogsGroup(AppApiGroup source, LogsGroupBuilder builder) : base(source)
    {
        AppRequest = builder.AppRequest.Build();
        AppRequests = builder.AppRequests.Build();
        GetLogEntryDetail = builder.GetLogEntryDetail.Build();
        GetLogEntryOrDefaultByKey = builder.GetLogEntryOrDefaultByKey.Build();
        GetRequestDetail = builder.GetRequestDetail.Build();
        GetSessionDetail = builder.GetSessionDetail.Build();
        LogEntries = builder.LogEntries.Build();
        LogEntry = builder.LogEntry.Build();
        Session = builder.Session.Build();
        Sessions = builder.Sessions.Build();
        Configure();
    }

    partial void Configure();
    public AppApiAction<AppRequestRequest, WebViewResult> AppRequest { get; }
    public AppApiAction<AppRequestQueryRequest, WebViewResult> AppRequests { get; }
    public AppApiAction<int, AppLogEntryDetailModel> GetLogEntryDetail { get; }
    public AppApiAction<string, AppLogEntryModel> GetLogEntryOrDefaultByKey { get; }
    public AppApiAction<int, AppRequestDetailModel> GetRequestDetail { get; }
    public AppApiAction<int, AppSessionDetailModel> GetSessionDetail { get; }
    public AppApiAction<LogEntryQueryRequest, WebViewResult> LogEntries { get; }
    public AppApiAction<LogEntryRequest, WebViewResult> LogEntry { get; }
    public AppApiAction<SessionViewRequest, WebViewResult> Session { get; }
    public AppApiAction<EmptyRequest, WebViewResult> Sessions { get; }
}