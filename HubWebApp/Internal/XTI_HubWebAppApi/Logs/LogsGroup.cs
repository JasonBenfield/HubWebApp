namespace XTI_HubWebAppApi.Logs;

public sealed class LogsGroup : AppApiGroupWrapper
{
    public LogsGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        Sessions = source.AddAction
        (
            nameof(Sessions),
            () => sp.GetRequiredService<SessionViewAction>()
        );
        Requests = source.AddAction
        (
            nameof(Requests),
            () => sp.GetRequiredService<RequestViewAction>()
        );
        LogEntries = source.AddAction
        (
            nameof(LogEntries),
            () => sp.GetRequiredService<LogEntryViewAction>()
        );
    }

    public AppApiAction<EmptyRequest, WebViewResult> Sessions { get; }
    public AppApiAction<EmptyRequest, WebViewResult> Requests { get; }
    public AppApiAction<EmptyRequest, WebViewResult> LogEntries { get; }
}