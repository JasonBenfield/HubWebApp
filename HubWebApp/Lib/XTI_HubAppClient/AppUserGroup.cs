// Generated Code
namespace XTI_HubAppClient;
public sealed partial class AppUserGroup : AppClientGroup
{
    public AppUserGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "AppUser")
    {
        Actions = new AppUserGroupActions(Index: CreateGetAction<GetAppUserRequest>("Index"), GetExplicitUserAccess: CreatePostAction<UserModifierKey, UserAccessModel>("GetExplicitUserAccess"), GetAssignedRoles: CreatePostAction<UserModifierKey, AppRoleModel[]>("GetAssignedRoles"), GetExplicitlyUnassignedRoles: CreatePostAction<UserModifierKey, AppRoleModel[]>("GetExplicitlyUnassignedRoles"));
    }

    public AppUserGroupActions Actions { get; }

    public Task<UserAccessModel> GetExplicitUserAccess(string modifier, UserModifierKey model, CancellationToken ct = default) => Actions.GetExplicitUserAccess.Post(modifier, model, ct);
    public Task<AppRoleModel[]> GetAssignedRoles(string modifier, UserModifierKey model, CancellationToken ct = default) => Actions.GetAssignedRoles.Post(modifier, model, ct);
    public Task<AppRoleModel[]> GetExplicitlyUnassignedRoles(string modifier, UserModifierKey model, CancellationToken ct = default) => Actions.GetExplicitlyUnassignedRoles.Post(modifier, model, ct);
    public sealed record AppUserGroupActions(AppClientGetAction<GetAppUserRequest> Index, AppClientPostAction<UserModifierKey, UserAccessModel> GetExplicitUserAccess, AppClientPostAction<UserModifierKey, AppRoleModel[]> GetAssignedRoles, AppClientPostAction<UserModifierKey, AppRoleModel[]> GetExplicitlyUnassignedRoles);
}