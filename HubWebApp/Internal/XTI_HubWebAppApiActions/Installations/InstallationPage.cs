namespace XTI_HubWebAppApiActions.Installations;

public sealed class InstallationPage : AppAction<InstallationViewRequest, WebViewResult>
{
    private readonly WebViewResultFactory viewFactory;

    public InstallationPage(WebViewResultFactory viewFactory)
    {
        this.viewFactory = viewFactory;
    }

    public Task<WebViewResult> Execute(InstallationViewRequest model, CancellationToken stoppingToken) =>
        Task.FromResult(viewFactory.Default("installation", "Installation"));
}
