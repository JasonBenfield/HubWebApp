namespace XTI_HubAppApi.AppUserInquiry;

public sealed class IndexAction : AppAction<int, WebViewResult>
{
    private readonly IPageContext pageContext;

    public IndexAction(IPageContext pageContext)
    {
        this.pageContext = pageContext;
    }

    public Task<WebViewResult> Execute(int model, CancellationToken stoppingToken)
    {
        var action = new TitledViewAppAction<int>(pageContext, "Index", "App User");
        return action.Execute(model, stoppingToken);
    }
}