namespace XTI_HubWebAppApi.Logs;

internal sealed class LogEntryViewAction : AppAction<LogEntryQueryRequest, WebViewResult>
{
    private readonly WebViewResultFactory viewFactory;

    public LogEntryViewAction(WebViewResultFactory viewFactory)
    {
        this.viewFactory = viewFactory;
    }

    public Task<WebViewResult> Execute(LogEntryQueryRequest model, CancellationToken stoppingToken) =>
        Task.FromResult(viewFactory.Default("logEntries", "Event Log"));
}
