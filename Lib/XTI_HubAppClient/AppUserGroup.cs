// Generated Code
namespace XTI_HubAppClient;
public sealed partial class AppUserGroup : AppClientGroup
{
    public AppUserGroup(IHttpClientFactory httpClientFactory, IXtiToken xtiToken, string baseUrl) : base(httpClientFactory, xtiToken, baseUrl, "AppUser")
    {
    }

    public Task<AppRoleModel[]> GetUserRoles(string modifier, GetUserRolesRequest model) => Post<AppRoleModel[], GetUserRolesRequest>("GetUserRoles", modifier, model);
    public Task<AppRoleModel[]> GetUnassignedRoles(string modifier, GetUnassignedRolesRequest model) => Post<AppRoleModel[], GetUnassignedRolesRequest>("GetUnassignedRoles", modifier, model);
    public Task<UserModifierCategoryModel[]> GetUserModCategories(string modifier, int model) => Post<UserModifierCategoryModel[], int>("GetUserModCategories", modifier, model);
}