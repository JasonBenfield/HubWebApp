namespace XTI_HubWebAppApiActions.Logs;

public sealed class LogEntryPage : AppAction<LogEntryRequest, WebViewResult>
{
    private readonly WebViewResultFactory viewFactory;

    public LogEntryPage(WebViewResultFactory viewFactory)
    {
        this.viewFactory = viewFactory;
    }

    public Task<WebViewResult> Execute(LogEntryRequest model, CancellationToken stoppingToken) =>
        Task.FromResult(viewFactory.Default("logEntry", "Log Entry"));
}
