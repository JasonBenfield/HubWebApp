// Generated Code
namespace XTI_HubAppClient;
public sealed partial class AppUserMaintenanceGroup : AppClientGroup
{
    public AppUserMaintenanceGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "AppUserMaintenance")
    {
        Actions = new AppUserMaintenanceGroupActions(AssignRole: CreatePostAction<UserRoleRequest, int>("AssignRole"), UnassignRole: CreatePostAction<UserRoleRequest, EmptyActionResult>("UnassignRole"), DenyAccess: CreatePostAction<UserModifierKey, EmptyActionResult>("DenyAccess"), AllowAccess: CreatePostAction<UserModifierKey, EmptyActionResult>("AllowAccess"));
    }

    public AppUserMaintenanceGroupActions Actions { get; }

    public Task<int> AssignRole(string modifier, UserRoleRequest model) => Actions.AssignRole.Post(modifier, model);
    public Task<EmptyActionResult> UnassignRole(string modifier, UserRoleRequest model) => Actions.UnassignRole.Post(modifier, model);
    public Task<EmptyActionResult> DenyAccess(string modifier, UserModifierKey model) => Actions.DenyAccess.Post(modifier, model);
    public Task<EmptyActionResult> AllowAccess(string modifier, UserModifierKey model) => Actions.AllowAccess.Post(modifier, model);
    public sealed record AppUserMaintenanceGroupActions(AppClientPostAction<UserRoleRequest, int> AssignRole, AppClientPostAction<UserRoleRequest, EmptyActionResult> UnassignRole, AppClientPostAction<UserModifierKey, EmptyActionResult> DenyAccess, AppClientPostAction<UserModifierKey, EmptyActionResult> AllowAccess);
}