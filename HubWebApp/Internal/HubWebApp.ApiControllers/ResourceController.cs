// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
public sealed partial class ResourceController : Controller
{
    private readonly HubAppApi api;
    public ResourceController(HubAppApi api)
    {
        this.api = api;
    }

    [HttpPost]
    public Task<ResultContainer<ResourceModel>> GetResource([FromBody] GetResourceRequest model, CancellationToken ct)
    {
        return api.Group("Resource").Action<GetResourceRequest, ResourceModel>("GetResource").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppRoleModel[]>> GetRoleAccess([FromBody] GetResourceRoleAccessRequest model, CancellationToken ct)
    {
        return api.Group("Resource").Action<GetResourceRoleAccessRequest, AppRoleModel[]>("GetRoleAccess").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppRequestExpandedModel[]>> GetMostRecentRequests([FromBody] GetResourceLogRequest model, CancellationToken ct)
    {
        return api.Group("Resource").Action<GetResourceLogRequest, AppRequestExpandedModel[]>("GetMostRecentRequests").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppLogEntryModel[]>> GetMostRecentErrorEvents([FromBody] GetResourceLogRequest model, CancellationToken ct)
    {
        return api.Group("Resource").Action<GetResourceLogRequest, AppLogEntryModel[]>("GetMostRecentErrorEvents").Execute(model, ct);
    }
}