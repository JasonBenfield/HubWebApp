// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
public sealed partial class ResourceInquiryController : Controller
{
    private readonly HubAppApi api;
    public ResourceInquiryController(HubAppApi api)
    {
        this.api = api;
    }

    [HttpPost]
    public Task<ResultContainer<AppLogEntryModel[]>> GetMostRecentErrorEvents([FromBody] GetResourceLogRequest requestData, CancellationToken ct)
    {
        return api.ResourceInquiry.GetMostRecentErrorEvents.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppRequestExpandedModel[]>> GetMostRecentRequests([FromBody] GetResourceLogRequest requestData, CancellationToken ct)
    {
        return api.ResourceInquiry.GetMostRecentRequests.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<ResourceModel>> GetResource([FromBody] GetResourceRequest requestData, CancellationToken ct)
    {
        return api.ResourceInquiry.GetResource.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppRoleModel[]>> GetRoleAccess([FromBody] GetResourceRoleAccessRequest requestData, CancellationToken ct)
    {
        return api.ResourceInquiry.GetRoleAccess.Execute(requestData, ct);
    }
}