namespace XTI_HubAppApi.Home;

public sealed class IndexAction : AppAction<EmptyRequest, WebViewResult>
{
    private readonly IPageContext pageContext;

    public IndexAction(IPageContext pageContext)
    {
        this.pageContext = pageContext;
    }

    public Task<WebViewResult> Execute(EmptyRequest model, CancellationToken stoppingToken)
    {
        return new TitledViewAppAction<EmptyRequest>(pageContext, "Index", "Home").Execute(model, stoppingToken);
    }
}