using System.Threading.Tasks;
using XTI_App.Api;
using XTI_WebApp.Api;

namespace HubWebAppApi
{
    public sealed class VerifyLoginFormAction : AppAction<EmptyRequest, WebPartialViewResult>
    {
        public Task<WebPartialViewResult> Execute(EmptyRequest model)
            => Task.FromResult(new WebPartialViewResult("VerifyLoginForm"));
    }
}
