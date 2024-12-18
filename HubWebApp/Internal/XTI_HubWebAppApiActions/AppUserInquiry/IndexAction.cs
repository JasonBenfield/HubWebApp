namespace XTI_HubWebAppApiActions.AppUserInquiry;

public sealed class IndexAction : AppAction<AppUserIndexRequest, WebViewResult>
{
    private readonly WebViewResultFactory viewFactory;

    public IndexAction(WebViewResultFactory viewFactory)
    {
        this.viewFactory = viewFactory;
    }

    public Task<WebViewResult> Execute(AppUserIndexRequest indexRequest, CancellationToken stoppingToken) =>
        Task.FromResult(viewFactory.Default("appUser", "App User"));
}