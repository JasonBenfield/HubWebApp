namespace XTI_HubWebAppApiActions.Logs;

public sealed class AppRequestsViewAction : AppAction<AppRequestQueryRequest, WebViewResult>
{
    private readonly WebViewResultFactory viewFactory;

    public AppRequestsViewAction(WebViewResultFactory viewFactory)
    {
        this.viewFactory = viewFactory;
    }

    public Task<WebViewResult> Execute(AppRequestQueryRequest model, CancellationToken stoppingToken) =>
        Task.FromResult(viewFactory.Default("requests", "Access Log"));
}
