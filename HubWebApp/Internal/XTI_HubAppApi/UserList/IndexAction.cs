namespace XTI_HubAppApi.UserList;

public sealed class IndexAction : AppAction<EmptyRequest, WebViewResult>
{
    private readonly IPageContext pageContext;

    public IndexAction(IPageContext pageContext)
    {
        this.pageContext = pageContext;
    }

    public Task<WebViewResult> Execute(EmptyRequest model, CancellationToken stoppingToken)
    {
        var action = new TitledViewAppAction<EmptyRequest>(pageContext, "Index", "Users");
        return action.Execute(model, stoppingToken);
    }
}