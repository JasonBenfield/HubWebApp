namespace XTI_HubWebAppApiActions.Logs;

public sealed class SessionPage : AppAction<SessionViewRequest, WebViewResult>
{
    private readonly WebViewResultFactory viewFactory;

    public SessionPage(WebViewResultFactory viewFactory)
    {
        this.viewFactory = viewFactory;
    }

    public Task<WebViewResult> Execute(SessionViewRequest model, CancellationToken stoppingToken) =>
        Task.FromResult(viewFactory.Default("session", "Session"));
}
