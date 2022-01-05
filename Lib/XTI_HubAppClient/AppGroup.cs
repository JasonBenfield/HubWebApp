// Generated Code
namespace XTI_HubAppClient;
public sealed partial class AppGroup : AppClientGroup
{
    public AppGroup(IHttpClientFactory httpClientFactory, IXtiToken xtiToken, AppClientUrl clientUrl) : base(httpClientFactory, xtiToken, clientUrl, "App")
    {
    }

    public Task<AppModel> GetApp(string modifier) => Post<AppModel, EmptyRequest>("GetApp", modifier, new EmptyRequest());
    public Task<AppRoleModel[]> GetRoles(string modifier) => Post<AppRoleModel[], EmptyRequest>("GetRoles", modifier, new EmptyRequest());
    public Task<AppRoleModel> GetRole(string modifier, string model) => Post<AppRoleModel, string>("GetRole", modifier, model);
    public Task<ResourceGroupModel[]> GetResourceGroups(string modifier) => Post<ResourceGroupModel[], EmptyRequest>("GetResourceGroups", modifier, new EmptyRequest());
    public Task<AppRequestExpandedModel[]> GetMostRecentRequests(string modifier, int model) => Post<AppRequestExpandedModel[], int>("GetMostRecentRequests", modifier, model);
    public Task<AppEventModel[]> GetMostRecentErrorEvents(string modifier, int model) => Post<AppEventModel[], int>("GetMostRecentErrorEvents", modifier, model);
    public Task<ModifierCategoryModel[]> GetModifierCategories(string modifier) => Post<ModifierCategoryModel[], EmptyRequest>("GetModifierCategories", modifier, new EmptyRequest());
    public Task<ModifierCategoryModel> GetModifierCategory(string modifier, string model) => Post<ModifierCategoryModel, string>("GetModifierCategory", modifier, model);
    public Task<ModifierModel> GetDefaultModifier(string modifier) => Post<ModifierModel, EmptyRequest>("GetDefaultModifier", modifier, new EmptyRequest());
}