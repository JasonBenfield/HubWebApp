using XTI_HubWebAppApiActions.Logs;

// Generated Code
namespace XTI_HubWebAppApi.Logs;
public sealed partial class LogsGroup : AppApiGroupWrapper
{
    internal LogsGroup(AppApiGroup source, LogsGroupBuilder builder) : base(source)
    {
        AppRequestsView = builder.AppRequestsView.Build();
        AppRequestView = builder.AppRequestView.Build();
        GetLogEntryDetail = builder.GetLogEntryDetail.Build();
        GetLogEntryOrDefaultByKey = builder.GetLogEntryOrDefaultByKey.Build();
        GetRequestDetail = builder.GetRequestDetail.Build();
        GetSessionDetail = builder.GetSessionDetail.Build();
        LogEntriesView = builder.LogEntriesView.Build();
        LogEntryView = builder.LogEntryView.Build();
        SessionsView = builder.SessionsView.Build();
        SessionView = builder.SessionView.Build();
        Configure();
    }

    partial void Configure();
    public AppApiAction<AppRequestQueryRequest, WebViewResult> AppRequestsView { get; }
    public AppApiAction<AppRequestRequest, WebViewResult> AppRequestView { get; }
    public AppApiAction<int, AppLogEntryDetailModel> GetLogEntryDetail { get; }
    public AppApiAction<string, AppLogEntryModel> GetLogEntryOrDefaultByKey { get; }
    public AppApiAction<int, AppRequestDetailModel> GetRequestDetail { get; }
    public AppApiAction<int, AppSessionDetailModel> GetSessionDetail { get; }
    public AppApiAction<LogEntryQueryRequest, WebViewResult> LogEntriesView { get; }
    public AppApiAction<LogEntryRequest, WebViewResult> LogEntryView { get; }
    public AppApiAction<EmptyRequest, WebViewResult> SessionsView { get; }
    public AppApiAction<SessionViewRequest, WebViewResult> SessionView { get; }
}