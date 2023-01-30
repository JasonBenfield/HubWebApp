namespace XTI_HubWebAppApi.Logs;

internal sealed class RequestViewAction : AppAction<RequestRequest, WebViewResult>
{
    private readonly WebViewResultFactory viewFactory;

    public RequestViewAction(WebViewResultFactory viewFactory)
    {
        this.viewFactory = viewFactory;
    }

    public Task<WebViewResult> Execute(RequestRequest model, CancellationToken stoppingToken) =>
        Task.FromResult(viewFactory.Default("request", "Request"));
}
