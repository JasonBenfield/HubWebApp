namespace XTI_HubWebAppApi.UserList;

public sealed class IndexAction : AppAction<GetUserRequest, WebViewResult>
{
    private readonly WebViewResultFactory viewFactory;

    public IndexAction(WebViewResultFactory viewFactory)
    {
        this.viewFactory = viewFactory;
    }

    public Task<WebViewResult> Execute(GetUserRequest model, CancellationToken stoppingToken) =>
        Task.FromResult(viewFactory.Default("users", "Users"));
}