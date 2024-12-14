namespace XTI_HubWebAppApiActions.Logs;

public sealed class LogEntriesViewAction : AppAction<LogEntryQueryRequest, WebViewResult>
{
    private readonly WebViewResultFactory viewFactory;

    public LogEntriesViewAction(WebViewResultFactory viewFactory)
    {
        this.viewFactory = viewFactory;
    }

    public Task<WebViewResult> Execute(LogEntryQueryRequest model, CancellationToken stoppingToken) =>
        Task.FromResult(viewFactory.Default("logEntries", "Event Log"));
}
