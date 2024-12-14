// Generated Code
namespace XTI_HubAppClient;
public sealed partial class AppUserMaintenanceGroup : AppClientGroup
{
    public AppUserMaintenanceGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "AppUserMaintenance")
    {
        Actions = new AppUserMaintenanceGroupActions(AllowAccess: CreatePostAction<UserModifierKey, EmptyActionResult>("AllowAccess"), AssignRole: CreatePostAction<UserRoleRequest, int>("AssignRole"), DenyAccess: CreatePostAction<UserModifierKey, EmptyActionResult>("DenyAccess"), UnassignRole: CreatePostAction<UserRoleRequest, EmptyActionResult>("UnassignRole"));
    }

    public AppUserMaintenanceGroupActions Actions { get; }

    public Task<EmptyActionResult> AllowAccess(string modifier, UserModifierKey model, CancellationToken ct = default) => Actions.AllowAccess.Post(modifier, model, ct);
    public Task<int> AssignRole(string modifier, UserRoleRequest model, CancellationToken ct = default) => Actions.AssignRole.Post(modifier, model, ct);
    public Task<EmptyActionResult> DenyAccess(string modifier, UserModifierKey model, CancellationToken ct = default) => Actions.DenyAccess.Post(modifier, model, ct);
    public Task<EmptyActionResult> UnassignRole(string modifier, UserRoleRequest model, CancellationToken ct = default) => Actions.UnassignRole.Post(modifier, model, ct);
    public sealed record AppUserMaintenanceGroupActions(AppClientPostAction<UserModifierKey, EmptyActionResult> AllowAccess, AppClientPostAction<UserRoleRequest, int> AssignRole, AppClientPostAction<UserModifierKey, EmptyActionResult> DenyAccess, AppClientPostAction<UserRoleRequest, EmptyActionResult> UnassignRole);
}