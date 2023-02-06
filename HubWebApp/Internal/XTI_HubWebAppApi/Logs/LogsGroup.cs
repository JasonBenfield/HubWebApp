namespace XTI_HubWebAppApi.Logs;

public sealed class LogsGroup : AppApiGroupWrapper
{
    public LogsGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        GetLogEntryByKey = source.AddAction
        (
            nameof(GetLogEntryByKey),
            () => sp.GetRequiredService<GetLogEntryByKeyAction>(),
            access: Access.WithAllowed(HubInfo.Roles.ViewLog)
        );
        GetLogEntryDetail = source.AddAction
        (
            nameof(GetLogEntryDetail),
            () => sp.GetRequiredService<GetLogEntryDetailAction>()
        );
        GetRequestDetail = source.AddAction
        (
            nameof(GetRequestDetail),
            () => sp.GetRequiredService<GetRequestDetailAction>()
        );
        GetSessionDetail = source.AddAction
        (
            nameof(GetSessionDetail),
            () => sp.GetRequiredService<GetSessionDetailAction>()
        );
        Sessions = source.AddAction
        (
            nameof(Sessions),
            () => sp.GetRequiredService<SessionsViewAction>()
        );
        Session = source.AddAction
        (
            nameof(Session),
            () => sp.GetRequiredService<SessionViewAction>()
        );
        AppRequests = source.AddAction
        (
            nameof(AppRequests),
            () => sp.GetRequiredService<AppRequestsViewAction>()
        );
        AppRequest = source.AddAction
        (
            nameof(AppRequest),
            () => sp.GetRequiredService<AppRequestViewAction>()
        );
        LogEntry = source.AddAction
        (
            nameof(LogEntry),
            () => sp.GetRequiredService<LogEntryViewAction>()
        );
        LogEntries = source.AddAction
        (
            nameof(LogEntries),
            () => sp.GetRequiredService<LogEntriesViewAction>()
        );
    }

    public AppApiAction<string, AppLogEntryModel> GetLogEntryByKey { get; }
    public AppApiAction<int, AppLogEntryDetailModel> GetLogEntryDetail { get; }
    public AppApiAction<int, AppRequestDetailModel> GetRequestDetail { get; }
    public AppApiAction<int, AppSessionDetailModel> GetSessionDetail { get; }
    public AppApiAction<EmptyRequest, WebViewResult> Sessions { get; }
    public AppApiAction<SessionViewRequest, WebViewResult> Session { get; }
    public AppApiAction<AppRequestQueryRequest, WebViewResult> AppRequests { get; }
    public AppApiAction<AppRequestRequest, WebViewResult> AppRequest { get; }
    public AppApiAction<LogEntryRequest, WebViewResult> LogEntry { get; }
    public AppApiAction<LogEntryQueryRequest, WebViewResult> LogEntries { get; }
}