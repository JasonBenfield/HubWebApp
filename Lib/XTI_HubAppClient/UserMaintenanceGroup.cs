// Generated Code
namespace XTI_HubAppClient;
public sealed partial class UserMaintenanceGroup : AppClientGroup
{
    public UserMaintenanceGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl) : base(httpClientFactory, xtiTokenAccessor, clientUrl, "UserMaintenance")
    {
    }

    public Task<EmptyActionResult> EditUser(EditUserForm model) => Post<EmptyActionResult, EditUserForm>("EditUser", "", model);
    public Task<IDictionary<string, object>> GetUserForEdit(int model) => Post<IDictionary<string, object>, int>("GetUserForEdit", "", model);
}