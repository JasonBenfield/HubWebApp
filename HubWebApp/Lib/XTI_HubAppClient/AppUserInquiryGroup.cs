// Generated Code
namespace XTI_HubAppClient;
public sealed partial class AppUserInquiryGroup : AppClientGroup
{
    public AppUserInquiryGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "AppUserInquiry")
    {
        Actions = new AppUserInquiryGroupActions(GetAssignedRoles: CreatePostAction<UserModifierKey, AppRoleModel[]>("GetAssignedRoles"), GetExplicitlyUnassignedRoles: CreatePostAction<UserModifierKey, AppRoleModel[]>("GetExplicitlyUnassignedRoles"), GetExplicitUserAccess: CreatePostAction<UserModifierKey, UserAccessModel>("GetExplicitUserAccess"), Index: CreateGetAction<AppUserIndexRequest>("Index"));
        Configure();
    }

    partial void Configure();
    public AppUserInquiryGroupActions Actions { get; }

    public Task<AppRoleModel[]> GetAssignedRoles(string modifier, UserModifierKey requestData, CancellationToken ct = default) => Actions.GetAssignedRoles.Post(modifier, requestData, ct);
    public Task<AppRoleModel[]> GetExplicitlyUnassignedRoles(string modifier, UserModifierKey requestData, CancellationToken ct = default) => Actions.GetExplicitlyUnassignedRoles.Post(modifier, requestData, ct);
    public Task<UserAccessModel> GetExplicitUserAccess(string modifier, UserModifierKey requestData, CancellationToken ct = default) => Actions.GetExplicitUserAccess.Post(modifier, requestData, ct);
    public sealed record AppUserInquiryGroupActions(AppClientPostAction<UserModifierKey, AppRoleModel[]> GetAssignedRoles, AppClientPostAction<UserModifierKey, AppRoleModel[]> GetExplicitlyUnassignedRoles, AppClientPostAction<UserModifierKey, UserAccessModel> GetExplicitUserAccess, AppClientGetAction<AppUserIndexRequest> Index);
}