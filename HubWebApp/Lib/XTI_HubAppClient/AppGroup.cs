// Generated Code
namespace XTI_HubAppClient;
public sealed partial class AppGroup : AppClientGroup
{
    public AppGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "App")
    {
        Actions = new AppGroupActions(Index: CreateGetAction<EmptyRequest>("Index"), GetApp: CreatePostAction<EmptyRequest, AppModel>("GetApp"), GetResourceGroups: CreatePostAction<EmptyRequest, ResourceGroupModel[]>("GetResourceGroups"), GetMostRecentRequests: CreatePostAction<int, AppRequestExpandedModel[]>("GetMostRecentRequests"), GetMostRecentErrorEvents: CreatePostAction<int, AppLogEntryModel[]>("GetMostRecentErrorEvents"), GetModifierCategories: CreatePostAction<EmptyRequest, ModifierCategoryModel[]>("GetModifierCategories"), GetDefaultModifier: CreatePostAction<EmptyRequest, ModifierModel>("GetDefaultModifier"));
    }

    public AppGroupActions Actions { get; }

    public Task<AppModel> GetApp(string modifier) => Actions.GetApp.Post(modifier, new EmptyRequest());
    public Task<ResourceGroupModel[]> GetResourceGroups(string modifier) => Actions.GetResourceGroups.Post(modifier, new EmptyRequest());
    public Task<AppRequestExpandedModel[]> GetMostRecentRequests(string modifier, int model) => Actions.GetMostRecentRequests.Post(modifier, model);
    public Task<AppLogEntryModel[]> GetMostRecentErrorEvents(string modifier, int model) => Actions.GetMostRecentErrorEvents.Post(modifier, model);
    public Task<ModifierCategoryModel[]> GetModifierCategories(string modifier) => Actions.GetModifierCategories.Post(modifier, new EmptyRequest());
    public Task<ModifierModel> GetDefaultModifier(string modifier) => Actions.GetDefaultModifier.Post(modifier, new EmptyRequest());
    public sealed record AppGroupActions(AppClientGetAction<EmptyRequest> Index, AppClientPostAction<EmptyRequest, AppModel> GetApp, AppClientPostAction<EmptyRequest, ResourceGroupModel[]> GetResourceGroups, AppClientPostAction<int, AppRequestExpandedModel[]> GetMostRecentRequests, AppClientPostAction<int, AppLogEntryModel[]> GetMostRecentErrorEvents, AppClientPostAction<EmptyRequest, ModifierCategoryModel[]> GetModifierCategories, AppClientPostAction<EmptyRequest, ModifierModel> GetDefaultModifier);
}