namespace XTI_HubWebAppApi.UserGroups;

internal sealed class IndexView : AppAction<EmptyRequest, WebViewResult>
{
    private readonly WebViewResultFactory viewFactory;

    public IndexView(WebViewResultFactory viewFactory)
    {
        this.viewFactory = viewFactory;
    }

    public Task<WebViewResult> Execute(EmptyRequest model, CancellationToken stoppingToken) =>
        Task.FromResult(viewFactory.Default("userGroups", "User Groups"));
}
