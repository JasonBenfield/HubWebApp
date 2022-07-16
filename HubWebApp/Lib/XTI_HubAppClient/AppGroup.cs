// Generated Code
namespace XTI_HubAppClient;
public sealed partial class AppGroup : AppClientGroup
{
    public AppGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl) : base(httpClientFactory, xtiTokenAccessor, clientUrl, "App")
    {
        Actions = new AppActions(clientUrl);
    }

    public AppActions Actions { get; }

    public Task<AppModel> GetApp(string modifier) => Post<AppModel, EmptyRequest>("GetApp", modifier, new EmptyRequest());
    public Task<ResourceGroupModel[]> GetResourceGroups(string modifier) => Post<ResourceGroupModel[], EmptyRequest>("GetResourceGroups", modifier, new EmptyRequest());
    public Task<AppRequestExpandedModel[]> GetMostRecentRequests(string modifier, int model) => Post<AppRequestExpandedModel[], int>("GetMostRecentRequests", modifier, model);
    public Task<AppLogEntryModel[]> GetMostRecentErrorEvents(string modifier, int model) => Post<AppLogEntryModel[], int>("GetMostRecentErrorEvents", modifier, model);
    public Task<ModifierCategoryModel[]> GetModifierCategories(string modifier) => Post<ModifierCategoryModel[], EmptyRequest>("GetModifierCategories", modifier, new EmptyRequest());
    public Task<ModifierModel> GetDefaultModifier(string modifier) => Post<ModifierModel, EmptyRequest>("GetDefaultModifier", modifier, new EmptyRequest());
}