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
    public Task<ResultContainer<AppVersionModel>> GetVersion([FromBody] string model)
    {
        return api.Group("Version").Action<string, AppVersionModel>("GetVersion").Execute(model);
    }

    [HttpPost]
    public Task<ResultContainer<ResourceGroupModel>> GetResourceGroup([FromBody] GetVersionResourceGroupRequest model)
    {
        return api.Group("Version").Action<GetVersionResourceGroupRequest, ResourceGroupModel>("GetResourceGroup").Execute(model);
    }
}