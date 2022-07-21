namespace XTI_HubWebAppApi.AppUserInquiry;

public sealed class IndexAction : AppAction<GetAppUserRequest, WebViewResult>
{
    private readonly WebViewResultFactory viewFactory;

    public IndexAction(WebViewResultFactory viewFactory)
    {
        this.viewFactory = viewFactory;
    }

    public Task<WebViewResult> Execute(GetAppUserRequest model, CancellationToken stoppingToken) =>
        Task.FromResult(viewFactory.Default("appUser", "App User"));
}