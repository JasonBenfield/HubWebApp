namespace XTI_HubWebAppApi.Installations;

internal sealed class IndexAction : AppAction<InstallationQueryRequest, WebViewResult>
{
    private readonly WebViewResultFactory viewFactory;

    public IndexAction(WebViewResultFactory viewFactory)
    {
        this.viewFactory = viewFactory;
    }

    public Task<WebViewResult> Execute(InstallationQueryRequest model, CancellationToken ct) =>
        Task.FromResult(viewFactory.Default("installations", "Installations"));
}