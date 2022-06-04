// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
public class VersionController : Controller
{
    private readonly HubAppApi api;
    public VersionController(HubAppApi api)
    {
        this.api = api;
    }

    [HttpPost]
    public Task<ResultContainer<XtiVersionModel>> GetVersion([FromBody] string model, CancellationToken ct)
    {
        return api.Group("Version").Action<string, XtiVersionModel>("GetVersion").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<ResourceGroupModel>> GetResourceGroup([FromBody] GetVersionResourceGroupRequest model, CancellationToken ct)
    {
        return api.Group("Version").Action<GetVersionResourceGroupRequest, ResourceGroupModel>("GetResourceGroup").Execute(model, ct);
    }
}