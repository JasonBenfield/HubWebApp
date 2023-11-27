namespace XTI_HubWebAppApi.UserRoles;

internal sealed class IndexAction : AppAction<UserRoleQueryRequest, WebViewResult>
{
    private readonly WebViewResultFactory viewFactory;

    public IndexAction(WebViewResultFactory viewFactory)
    {
        this.viewFactory = viewFactory;
    }

    public Task<WebViewResult> Execute(UserRoleQueryRequest model, CancellationToken ct) =>
        Task.FromResult(viewFactory.Default("userRoles", "User Roles"));
}