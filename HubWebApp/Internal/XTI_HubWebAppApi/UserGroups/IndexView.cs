namespace XTI_HubWebAppApi.UserGroups;

internal sealed class IndexView : AppAction<UserGroupKey, WebViewResult>
{
    private readonly WebViewResultFactory viewFactory;

    public IndexView(WebViewResultFactory viewFactory)
    {
        this.viewFactory = viewFactory;
    }

    public Task<WebViewResult> Execute(UserGroupKey model, CancellationToken stoppingToken) =>
        Task.FromResult(viewFactory.Default("userGroups", "Users"));
}
