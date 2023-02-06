// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
[Route("odata/RequestQuery")]
public sealed partial class RequestQueryController : XtiODataController<AppRequestQueryRequest, ExpandedRequest>
{
    public RequestQueryController(HubAppApi api) : base(api.Group("RequestQuery"))
    {
    }
}