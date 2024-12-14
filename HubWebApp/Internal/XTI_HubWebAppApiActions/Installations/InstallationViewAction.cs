namespace XTI_HubWebAppApiActions.Installations;

public sealed class InstallationViewAction : AppAction<InstallationViewRequest, WebViewResult>
{
    private readonly WebViewResultFactory viewFactory;

    public InstallationViewAction(WebViewResultFactory viewFactory)
    {
        this.viewFactory = viewFactory;
    }

    public Task<WebViewResult> Execute(InstallationViewRequest model, CancellationToken stoppingToken) =>
        Task.FromResult(viewFactory.Default("installation", "Installation"));
}
