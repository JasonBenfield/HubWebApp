namespace XTI_HubWebAppApiActions.Logs;

public sealed class LogEntriesPage : AppAction<LogEntryQueryRequest, WebViewResult>
{
    private readonly WebViewResultFactory viewFactory;

    public LogEntriesPage(WebViewResultFactory viewFactory)
    {
        this.viewFactory = viewFactory;
    }

    public Task<WebViewResult> Execute(LogEntryQueryRequest model, CancellationToken stoppingToken) =>
        Task.FromResult(viewFactory.Default("logEntries", "Event Log"));
}
