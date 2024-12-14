// Generated Code
namespace XTI_HubAppClient;
public sealed partial class AppUserInquiryGroup : AppClientGroup
{
    public AppUserInquiryGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "AppUserInquiry")
    {
        Actions = new AppUserInquiryGroupActions(GetAssignedRoles: CreatePostAction<UserModifierKey, AppRoleModel[]>("GetAssignedRoles"), GetExplicitlyUnassignedRoles: CreatePostAction<UserModifierKey, AppRoleModel[]>("GetExplicitlyUnassignedRoles"), GetExplicitUserAccess: CreatePostAction<UserModifierKey, UserAccessModel>("GetExplicitUserAccess"), Index: CreateGetAction<GetAppUserRequest>("Index"));
    }

    public AppUserInquiryGroupActions Actions { get; }

    public Task<AppRoleModel[]> GetAssignedRoles(string modifier, UserModifierKey model, CancellationToken ct = default) => Actions.GetAssignedRoles.Post(modifier, model, ct);
    public Task<AppRoleModel[]> GetExplicitlyUnassignedRoles(string modifier, UserModifierKey model, CancellationToken ct = default) => Actions.GetExplicitlyUnassignedRoles.Post(modifier, model, ct);
    public Task<UserAccessModel> GetExplicitUserAccess(string modifier, UserModifierKey model, CancellationToken ct = default) => Actions.GetExplicitUserAccess.Post(modifier, model, ct);
    public sealed record AppUserInquiryGroupActions(AppClientPostAction<UserModifierKey, AppRoleModel[]> GetAssignedRoles, AppClientPostAction<UserModifierKey, AppRoleModel[]> GetExplicitlyUnassignedRoles, AppClientPostAction<UserModifierKey, UserAccessModel> GetExplicitUserAccess, AppClientGetAction<GetAppUserRequest> Index);
}