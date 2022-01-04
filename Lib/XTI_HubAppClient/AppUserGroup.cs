// Generated Code
namespace XTI_HubAppClient;
public sealed partial class AppUserGroup : AppClientGroup
{
    public AppUserGroup(IHttpClientFactory httpClientFactory, IXtiToken xtiToken, AppClientUrl clientUrl) : base(httpClientFactory, xtiToken, clientUrl, "AppUser")
    {
    }

    public Task<UserAccessModel> GetUserAccess(string modifier, UserModifierKey model) => Post<UserAccessModel, UserModifierKey>("GetUserAccess", modifier, model);
    public Task<AppRoleModel[]> GetUnassignedRoles(string modifier, UserModifierKey model) => Post<AppRoleModel[], UserModifierKey>("GetUnassignedRoles", modifier, model);
    public Task<UserModifierCategoryModel[]> GetUserModCategories(string modifier, int model) => Post<UserModifierCategoryModel[], int>("GetUserModCategories", modifier, model);
}