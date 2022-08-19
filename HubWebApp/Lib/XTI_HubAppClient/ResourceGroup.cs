// Generated Code
namespace XTI_HubAppClient;
public sealed partial class ResourceGroup : AppClientGroup
{
    public ResourceGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "Resource")
    {
        Actions = new ResourceGroupActions(GetResource: CreatePostAction<GetResourceRequest, ResourceModel>("GetResource"), GetRoleAccess: CreatePostAction<GetResourceRoleAccessRequest, AppRoleModel[]>("GetRoleAccess"), GetMostRecentRequests: CreatePostAction<GetResourceLogRequest, AppRequestExpandedModel[]>("GetMostRecentRequests"), GetMostRecentErrorEvents: CreatePostAction<GetResourceLogRequest, AppLogEntryModel[]>("GetMostRecentErrorEvents"));
    }

    public ResourceGroupActions Actions { get; }

    public Task<ResourceModel> GetResource(string modifier, GetResourceRequest model, CancellationToken ct = default) => Actions.GetResource.Post(modifier, model, ct);
    public Task<AppRoleModel[]> GetRoleAccess(string modifier, GetResourceRoleAccessRequest model, CancellationToken ct = default) => Actions.GetRoleAccess.Post(modifier, model, ct);
    public Task<AppRequestExpandedModel[]> GetMostRecentRequests(string modifier, GetResourceLogRequest model, CancellationToken ct = default) => Actions.GetMostRecentRequests.Post(modifier, model, ct);
    public Task<AppLogEntryModel[]> GetMostRecentErrorEvents(string modifier, GetResourceLogRequest model, CancellationToken ct = default) => Actions.GetMostRecentErrorEvents.Post(modifier, model, ct);
    public sealed record ResourceGroupActions(AppClientPostAction<GetResourceRequest, ResourceModel> GetResource, AppClientPostAction<GetResourceRoleAccessRequest, AppRoleModel[]> GetRoleAccess, AppClientPostAction<GetResourceLogRequest, AppRequestExpandedModel[]> GetMostRecentRequests, AppClientPostAction<GetResourceLogRequest, AppLogEntryModel[]> GetMostRecentErrorEvents);
}