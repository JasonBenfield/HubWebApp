namespace XTI_HubWebAppApi.UserRoles;

internal sealed class UserRolePage : AppAction<UserRoleIDRequest, WebViewResult>
{
    private readonly WebViewResultFactory viewFactory;

    public UserRolePage(WebViewResultFactory viewFactory)
    {
        this.viewFactory = viewFactory;
    }

    public Task<WebViewResult> Execute(UserRoleIDRequest model, CancellationToken stoppingToken) =>
        Task.FromResult(viewFactory.Default("userRole", "User Role"));
}
