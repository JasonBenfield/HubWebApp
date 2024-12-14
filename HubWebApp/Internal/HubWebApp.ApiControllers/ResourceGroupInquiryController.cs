// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
public sealed partial class ResourceGroupInquiryController : Controller
{
    private readonly HubAppApi api;
    public ResourceGroupInquiryController(HubAppApi api)
    {
        this.api = api;
    }

    [HttpPost]
    public Task<ResultContainer<ModifierCategoryModel>> GetModCategory([FromBody] GetResourceGroupModCategoryRequest requestData, CancellationToken ct)
    {
        return api.ResourceGroupInquiry.GetModCategory.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppLogEntryModel[]>> GetMostRecentErrorEvents([FromBody] GetResourceGroupLogRequest requestData, CancellationToken ct)
    {
        return api.ResourceGroupInquiry.GetMostRecentErrorEvents.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppRequestExpandedModel[]>> GetMostRecentRequests([FromBody] GetResourceGroupLogRequest requestData, CancellationToken ct)
    {
        return api.ResourceGroupInquiry.GetMostRecentRequests.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<ResourceGroupModel>> GetResourceGroup([FromBody] GetResourceGroupRequest requestData, CancellationToken ct)
    {
        return api.ResourceGroupInquiry.GetResourceGroup.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<ResourceModel[]>> GetResources([FromBody] GetResourcesRequest requestData, CancellationToken ct)
    {
        return api.ResourceGroupInquiry.GetResources.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppRoleModel[]>> GetRoleAccess([FromBody] GetResourceGroupRoleAccessRequest requestData, CancellationToken ct)
    {
        return api.ResourceGroupInquiry.GetRoleAccess.Execute(requestData, ct);
    }
}