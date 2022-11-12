namespace XTI_HubWebAppApi.CurrentUser;

internal sealed class IndexAction : AppAction<EmptyRequest, WebViewResult>
{
    private readonly WebViewResultFactory viewFactory;

    public IndexAction(WebViewResultFactory viewFactory)
    {
        this.viewFactory = viewFactory;
    }

    public async Task<WebViewResult> Execute(EmptyRequest model, CancellationToken ct) =>
        viewFactory.Default("currentUser", "Current User");
}