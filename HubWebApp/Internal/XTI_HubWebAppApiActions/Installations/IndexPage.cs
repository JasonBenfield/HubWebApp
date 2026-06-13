namespace XTI_HubWebAppApiActions.Installations;

public sealed class IndexPage : AppAction<InstallationQueryRequest, WebViewResult>
{
    private readonly WebViewResultFactory viewFactory;

    public IndexPage(WebViewResultFactory viewFactory)
    {
        this.viewFactory = viewFactory;
    }

    public Task<WebViewResult> Execute(InstallationQueryRequest model, CancellationToken ct) =>
        Task.FromResult(viewFactory.Default("installations", "Installations"));
}