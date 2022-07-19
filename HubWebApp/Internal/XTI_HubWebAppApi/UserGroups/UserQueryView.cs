namespace XTI_HubWebAppApi.UserGroups;

internal sealed class UserQueryView : AppAction<UserGroupKey, WebViewResult>
{
    private readonly WebViewResultFactory viewFactory;

    public UserQueryView(WebViewResultFactory viewFactory)
    {
        this.viewFactory = viewFactory;
    }

    public Task<WebViewResult> Execute(UserGroupKey model, CancellationToken stoppingToken) =>
        Task.FromResult(viewFactory.Default("userQuery", "Users"));
}
