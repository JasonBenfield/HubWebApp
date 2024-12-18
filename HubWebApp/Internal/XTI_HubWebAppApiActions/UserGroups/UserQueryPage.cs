namespace XTI_HubWebAppApiActions.UserGroups;

public sealed class UserQueryPage : AppAction<UserGroupKey, WebViewResult>
{
    private readonly WebViewResultFactory viewFactory;

    public UserQueryPage(WebViewResultFactory viewFactory)
    {
        this.viewFactory = viewFactory;
    }

    public Task<WebViewResult> Execute(UserGroupKey model, CancellationToken stoppingToken) =>
        Task.FromResult(viewFactory.Default("userQuery", "Users"));
}
