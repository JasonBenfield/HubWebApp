namespace XTI_HubAppApi.AppUserInquiry;

public sealed class IndexAction : AppAction<int, WebViewResult>
{
    private readonly WebViewResultFactory viewFactory;

    public IndexAction(WebViewResultFactory viewFactory)
    {
        this.viewFactory = viewFactory;
    }

    public Task<WebViewResult> Execute(int model, CancellationToken stoppingToken) =>
        Task.FromResult(viewFactory.Default("appUser", "App User"));
}