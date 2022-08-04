// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
[Route("odata/RequestQuery")]
public sealed partial class RequestQueryController : XtiODataController<RequestQueryRequest, ExpandedRequest>
{
    public RequestQueryController(HubAppApi api) : base(api.Group("RequestQuery"))
    {
    }
}