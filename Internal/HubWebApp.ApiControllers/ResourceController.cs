// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
public class ResourceController : Controller
{
    private readonly HubAppApi api;
    public ResourceController(HubAppApi api)
    {
        this.api = api;
    }

    [HttpPost]
    public Task<ResultContainer<ResourceModel>> GetResource([FromBody] GetResourceRequest model)
    {
        return api.Group("Resource").Action<GetResourceRequest, ResourceModel>("GetResource").Execute(model);
    }

    [HttpPost]
    public Task<ResultContainer<AppRoleModel[]>> GetRoleAccess([FromBody] GetResourceRoleAccessRequest model)
    {
        return api.Group("Resource").Action<GetResourceRoleAccessRequest, AppRoleModel[]>("GetRoleAccess").Execute(model);
    }

    [HttpPost]
    public Task<ResultContainer<AppRequestExpandedModel[]>> GetMostRecentRequests([FromBody] GetResourceLogRequest model)
    {
        return api.Group("Resource").Action<GetResourceLogRequest, AppRequestExpandedModel[]>("GetMostRecentRequests").Execute(model);
    }

    [HttpPost]
    public Task<ResultContainer<AppEventModel[]>> GetMostRecentErrorEvents([FromBody] GetResourceLogRequest model)
    {
        return api.Group("Resource").Action<GetResourceLogRequest, AppEventModel[]>("GetMostRecentErrorEvents").Execute(model);
    }
}