using XTI_HubWebAppApiActions.Logs;

// Generated Code
namespace XTI_HubWebAppApi.Logs;
public sealed partial class LogsGroupBuilder
{
    private readonly AppApiGroup source;
    internal LogsGroupBuilder(AppApiGroup source)
    {
        this.source = source;
        AppRequestsView = source.AddAction<AppRequestQueryRequest, WebViewResult>("AppRequestsView").WithExecution<AppRequestsViewAction>();
        AppRequestView = source.AddAction<AppRequestRequest, WebViewResult>("AppRequestView").WithExecution<AppRequestViewAction>();
        GetLogEntryDetail = source.AddAction<int, AppLogEntryDetailModel>("GetLogEntryDetail").WithExecution<GetLogEntryDetailAction>();
        GetLogEntryOrDefaultByKey = source.AddAction<string, AppLogEntryModel>("GetLogEntryOrDefaultByKey").WithExecution<GetLogEntryOrDefaultByKeyAction>();
        GetRequestDetail = source.AddAction<int, AppRequestDetailModel>("GetRequestDetail").WithExecution<GetRequestDetailAction>();
        GetSessionDetail = source.AddAction<int, AppSessionDetailModel>("GetSessionDetail").WithExecution<GetSessionDetailAction>();
        LogEntriesView = source.AddAction<LogEntryQueryRequest, WebViewResult>("LogEntriesView").WithExecution<LogEntriesViewAction>();
        LogEntryView = source.AddAction<LogEntryRequest, WebViewResult>("LogEntryView").WithExecution<LogEntryViewAction>();
        SessionsView = source.AddAction<EmptyRequest, WebViewResult>("SessionsView").WithExecution<SessionsViewAction>();
        SessionView = source.AddAction<SessionViewRequest, WebViewResult>("SessionView").WithExecution<SessionViewAction>();
        Configure();
    }

    partial void Configure();
    public AppApiActionBuilder<AppRequestQueryRequest, WebViewResult> AppRequestsView { get; }
    public AppApiActionBuilder<AppRequestRequest, WebViewResult> AppRequestView { get; }
    public AppApiActionBuilder<int, AppLogEntryDetailModel> GetLogEntryDetail { get; }
    public AppApiActionBuilder<string, AppLogEntryModel> GetLogEntryOrDefaultByKey { get; }
    public AppApiActionBuilder<int, AppRequestDetailModel> GetRequestDetail { get; }
    public AppApiActionBuilder<int, AppSessionDetailModel> GetSessionDetail { get; }
    public AppApiActionBuilder<LogEntryQueryRequest, WebViewResult> LogEntriesView { get; }
    public AppApiActionBuilder<LogEntryRequest, WebViewResult> LogEntryView { get; }
    public AppApiActionBuilder<EmptyRequest, WebViewResult> SessionsView { get; }
    public AppApiActionBuilder<SessionViewRequest, WebViewResult> SessionView { get; }

    public LogsGroup Build() => new LogsGroup(source, this);
}