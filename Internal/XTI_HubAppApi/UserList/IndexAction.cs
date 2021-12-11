using System.Threading.Tasks;
using XTI_App.Api;
using XTI_WebApp;
using XTI_WebApp.Api;

namespace XTI_HubAppApi.UserList
{
    public sealed class IndexAction : AppAction<EmptyRequest, WebViewResult>
    {
        private readonly IPageContext pageContext;

        public IndexAction(IPageContext pageContext)
        {
            this.pageContext = pageContext;
        }

        public Task<WebViewResult> Execute(EmptyRequest model)
        {
            var action = new TitledViewAppAction<EmptyRequest>(pageContext, "Index", "Users");
            return action.Execute(model);
        }
    }
}
