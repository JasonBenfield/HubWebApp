using XTI_App.Api;
using XTI_WebApp.Abstractions;
using XTI_WebApp.Api;

namespace XTI_HubAppApi.AppList;

public sealed class IndexAction : AppAction<EmptyRequest, WebViewResult>
{
    private readonly IPageContext pageContext;

    public IndexAction(IPageContext pageContext)
    {
        this.pageContext = pageContext;
    }

    public Task<WebViewResult> Execute(EmptyRequest model)
    {
        return new TitledViewAppAction<EmptyRequest>(pageContext, "Index", "Apps").Execute(model);
    }
}