namespace XTI_HubWebAppApi.Logs;

internal sealed class RequestsViewAction : AppAction<RequestQueryRequest, WebViewResult>
{
    private readonly WebViewResultFactory viewFactory;

    public RequestsViewAction(WebViewResultFactory viewFactory)
    {
        this.viewFactory = viewFactory;
    }

    public Task<WebViewResult> Execute(RequestQueryRequest model, CancellationToken stoppingToken) =>
        Task.FromResult(viewFactory.Default("requests", "Access Log"));
}
