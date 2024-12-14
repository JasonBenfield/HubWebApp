// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
[Route("odata/AppRequestQuery")]
public sealed partial class AppRequestQueryController : XtiODataController<AppRequestQueryRequest, ExpandedRequest>
{
    public AppRequestQueryController(HubAppApi api) : base(api.AppRequestQuery)
    {
    }
}