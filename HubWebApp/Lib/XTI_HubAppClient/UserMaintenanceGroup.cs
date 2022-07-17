// Generated Code
namespace XTI_HubAppClient;
public sealed partial class UserMaintenanceGroup : AppClientGroup
{
    public UserMaintenanceGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl) : base(httpClientFactory, xtiTokenAccessor, clientUrl, "UserMaintenance")
    {
    }

    public Task<EmptyActionResult> EditUser(string modifier, EditUserForm model) => Post<EmptyActionResult, EditUserForm>("EditUser", modifier, model);
    public Task<IDictionary<string, object>> GetUserForEdit(string modifier, int model) => Post<IDictionary<string, object>, int>("GetUserForEdit", modifier, model);
}