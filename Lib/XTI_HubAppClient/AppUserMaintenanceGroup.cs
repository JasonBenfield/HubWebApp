// Generated Code
namespace XTI_HubAppClient;
public sealed partial class AppUserMaintenanceGroup : AppClientGroup
{
    public AppUserMaintenanceGroup(IHttpClientFactory httpClientFactory, IXtiToken xtiToken, AppClientUrl clientUrl) : base(httpClientFactory, xtiToken, clientUrl, "AppUserMaintenance")
    {
    }

    public Task<int> AssignRole(string modifier, UserRoleRequest model) => Post<int, UserRoleRequest>("AssignRole", modifier, model);
    public Task<EmptyActionResult> UnassignRole(string modifier, UserRoleRequest model) => Post<EmptyActionResult, UserRoleRequest>("UnassignRole", modifier, model);
    public Task<EmptyActionResult> DenyAccess(string modifier, UserModifierKey model) => Post<EmptyActionResult, UserModifierKey>("DenyAccess", modifier, model);
    public Task<EmptyActionResult> AllowAccess(string modifier, UserModifierKey model) => Post<EmptyActionResult, UserModifierKey>("AllowAccess", modifier, model);
}