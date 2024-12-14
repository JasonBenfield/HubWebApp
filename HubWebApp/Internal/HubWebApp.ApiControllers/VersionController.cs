// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
public sealed partial class VersionController : Controller
{
    private readonly HubAppApi api;
    public VersionController(HubAppApi api)
    {
        this.api = api;
    }

    [HttpPost]
    public Task<ResultContainer<XtiVersionModel>> GetVersion([FromBody] string requestData, CancellationToken ct)
    {
        return api.Version.GetVersion.Execute(requestData, ct);
    }
}