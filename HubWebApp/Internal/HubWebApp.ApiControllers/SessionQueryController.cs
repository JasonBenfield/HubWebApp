// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
[Route("odata/SessionQuery")]
public sealed partial class SessionQueryController : XtiODataController<EmptyRequest, ExpandedSession>
{
    public SessionQueryController(HubAppApi api) : base(api.SessionQuery)
    {
    }
}