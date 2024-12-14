namespace XTI_HubWebAppApiActions.Logs;

public sealed class SessionViewAction : AppAction<SessionViewRequest, WebViewResult>
{
    private readonly WebViewResultFactory viewFactory;

    public SessionViewAction(WebViewResultFactory viewFactory)
    {
        this.viewFactory = viewFactory;
    }

    public Task<WebViewResult> Execute(SessionViewRequest model, CancellationToken stoppingToken) =>
        Task.FromResult(viewFactory.Default("session", "Session"));
}
