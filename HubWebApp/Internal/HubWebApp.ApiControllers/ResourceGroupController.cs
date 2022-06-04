// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
public class ResourceGroupController : Controller
{
    private readonly HubAppApi api;
    public ResourceGroupController(HubAppApi api)
    {
        this.api = api;
    }

    [HttpPost]
    public Task<ResultContainer<ResourceGroupModel>> GetResourceGroup([FromBody] GetResourceGroupRequest model, CancellationToken ct)
    {
        return api.Group("ResourceGroup").Action<GetResourceGroupRequest, ResourceGroupModel>("GetResourceGroup").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<ResourceModel[]>> GetResources([FromBody] GetResourcesRequest model, CancellationToken ct)
    {
        return api.Group("ResourceGroup").Action<GetResourcesRequest, ResourceModel[]>("GetResources").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<ResourceModel>> GetResource([FromBody] GetResourceGroupResourceRequest model, CancellationToken ct)
    {
        return api.Group("ResourceGroup").Action<GetResourceGroupResourceRequest, ResourceModel>("GetResource").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppRoleModel[]>> GetRoleAccess([FromBody] GetResourceGroupRoleAccessRequest model, CancellationToken ct)
    {
        return api.Group("ResourceGroup").Action<GetResourceGroupRoleAccessRequest, AppRoleModel[]>("GetRoleAccess").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<ModifierCategoryModel>> GetModCategory([FromBody] GetResourceGroupModCategoryRequest model, CancellationToken ct)
    {
        return api.Group("ResourceGroup").Action<GetResourceGroupModCategoryRequest, ModifierCategoryModel>("GetModCategory").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppRequestExpandedModel[]>> GetMostRecentRequests([FromBody] GetResourceGroupLogRequest model, CancellationToken ct)
    {
        return api.Group("ResourceGroup").Action<GetResourceGroupLogRequest, AppRequestExpandedModel[]>("GetMostRecentRequests").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppLogEntryModel[]>> GetMostRecentErrorEvents([FromBody] GetResourceGroupLogRequest model, CancellationToken ct)
    {
        return api.Group("ResourceGroup").Action<GetResourceGroupLogRequest, AppLogEntryModel[]>("GetMostRecentErrorEvents").Execute(model, ct);
    }
}