namespace XTI_HubWebAppApiActions.Installation;

public sealed class IndexPage : AppAction<InstallationViewRequest, WebViewResult>
{
    private readonly WebViewResultFactory viewFactory;

    public IndexPage(WebViewResultFactory viewFactory)
    {
        this.viewFactory = viewFactory;
    }

    public Task<WebViewResult> Execute(InstallationViewRequest model, CancellationToken stoppingToken) =>
        Task.FromResult(viewFactory.Default("installation", "Installation"));
}
