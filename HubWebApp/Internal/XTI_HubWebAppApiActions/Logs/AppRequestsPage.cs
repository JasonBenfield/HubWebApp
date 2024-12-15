namespace XTI_HubWebAppApiActions.Logs;

public sealed class AppRequestsPage : AppAction<AppRequestQueryRequest, WebViewResult>
{
    private readonly WebViewResultFactory viewFactory;

    public AppRequestsPage(WebViewResultFactory viewFactory)
    {
        this.viewFactory = viewFactory;
    }

    public Task<WebViewResult> Execute(AppRequestQueryRequest model, CancellationToken stoppingToken) =>
        Task.FromResult(viewFactory.Default("requests", "Access Log"));
}
