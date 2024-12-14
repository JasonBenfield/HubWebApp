// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
[Route("odata/InstallationQuery")]
public sealed partial class InstallationQueryController : XtiODataController<InstallationQueryRequest, ExpandedInstallation>
{
    public InstallationQueryController(HubAppApi api) : base(api.InstallationQuery)
    {
    }
}