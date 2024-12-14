// Generated Code
namespace XTI_HubAppClient;
public sealed partial class ResourceInquiryGroup : AppClientGroup
{
    public ResourceInquiryGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "ResourceInquiry")
    {
        Actions = new ResourceInquiryGroupActions(GetMostRecentErrorEvents: CreatePostAction<GetResourceLogRequest, AppLogEntryModel[]>("GetMostRecentErrorEvents"), GetMostRecentRequests: CreatePostAction<GetResourceLogRequest, AppRequestExpandedModel[]>("GetMostRecentRequests"), GetResource: CreatePostAction<GetResourceRequest, ResourceModel>("GetResource"), GetRoleAccess: CreatePostAction<GetResourceRoleAccessRequest, AppRoleModel[]>("GetRoleAccess"));
    }

    public ResourceInquiryGroupActions Actions { get; }

    public Task<AppLogEntryModel[]> GetMostRecentErrorEvents(string modifier, GetResourceLogRequest model, CancellationToken ct = default) => Actions.GetMostRecentErrorEvents.Post(modifier, model, ct);
    public Task<AppRequestExpandedModel[]> GetMostRecentRequests(string modifier, GetResourceLogRequest model, CancellationToken ct = default) => Actions.GetMostRecentRequests.Post(modifier, model, ct);
    public Task<ResourceModel> GetResource(string modifier, GetResourceRequest model, CancellationToken ct = default) => Actions.GetResource.Post(modifier, model, ct);
    public Task<AppRoleModel[]> GetRoleAccess(string modifier, GetResourceRoleAccessRequest model, CancellationToken ct = default) => Actions.GetRoleAccess.Post(modifier, model, ct);
    public sealed record ResourceInquiryGroupActions(AppClientPostAction<GetResourceLogRequest, AppLogEntryModel[]> GetMostRecentErrorEvents, AppClientPostAction<GetResourceLogRequest, AppRequestExpandedModel[]> GetMostRecentRequests, AppClientPostAction<GetResourceRequest, ResourceModel> GetResource, AppClientPostAction<GetResourceRoleAccessRequest, AppRoleModel[]> GetRoleAccess);
}