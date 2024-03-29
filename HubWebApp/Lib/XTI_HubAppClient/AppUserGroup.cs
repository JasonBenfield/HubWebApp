// Generated Code
namespace XTI_HubAppClient;
public sealed partial class AppUserGroup : AppClientGroup
{
    public AppUserGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "AppUser")
    {
        Actions = new AppUserGroupActions(Index: CreateGetAction<GetAppUserRequest>("Index"), GetUserAccess: CreatePostAction<UserModifierKey, UserAccessModel>("GetUserAccess"), GetUnassignedRoles: CreatePostAction<UserModifierKey, AppRoleModel[]>("GetUnassignedRoles"));
    }

    public AppUserGroupActions Actions { get; }

    public Task<UserAccessModel> GetUserAccess(string modifier, UserModifierKey model, CancellationToken ct = default) => Actions.GetUserAccess.Post(modifier, model, ct);
    public Task<AppRoleModel[]> GetUnassignedRoles(string modifier, UserModifierKey model, CancellationToken ct = default) => Actions.GetUnassignedRoles.Post(modifier, model, ct);
    public sealed record AppUserGroupActions(AppClientGetAction<GetAppUserRequest> Index, AppClientPostAction<UserModifierKey, UserAccessModel> GetUserAccess, AppClientPostAction<UserModifierKey, AppRoleModel[]> GetUnassignedRoles);
}