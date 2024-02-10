// Generated Code
namespace XTI_HubAppClient;
public sealed partial class AppGroup : AppClientGroup
{
    public AppGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "App")
    {
        Actions = new AppGroupActions(Index: CreateGetAction<EmptyRequest>("Index"), GetApp: CreatePostAction<EmptyRequest, AppModel>("GetApp"), GetResourceGroups: CreatePostAction<EmptyRequest, ResourceGroupModel[]>("GetResourceGroups"), GetRoles: CreatePostAction<EmptyRequest, AppRoleModel[]>("GetRoles"), GetMostRecentRequests: CreatePostAction<int, AppRequestExpandedModel[]>("GetMostRecentRequests"), GetMostRecentErrorEvents: CreatePostAction<int, AppLogEntryModel[]>("GetMostRecentErrorEvents"), GetModifierCategories: CreatePostAction<EmptyRequest, ModifierCategoryModel[]>("GetModifierCategories"), GetDefaultModifier: CreatePostAction<EmptyRequest, ModifierModel>("GetDefaultModifier"));
    }

    public AppGroupActions Actions { get; }

    public Task<AppModel> GetApp(string modifier, CancellationToken ct = default) => Actions.GetApp.Post(modifier, new EmptyRequest(), ct);
    public Task<ResourceGroupModel[]> GetResourceGroups(string modifier, CancellationToken ct = default) => Actions.GetResourceGroups.Post(modifier, new EmptyRequest(), ct);
    public Task<AppRoleModel[]> GetRoles(string modifier, CancellationToken ct = default) => Actions.GetRoles.Post(modifier, new EmptyRequest(), ct);
    public Task<AppRequestExpandedModel[]> GetMostRecentRequests(string modifier, int model, CancellationToken ct = default) => Actions.GetMostRecentRequests.Post(modifier, model, ct);
    public Task<AppLogEntryModel[]> GetMostRecentErrorEvents(string modifier, int model, CancellationToken ct = default) => Actions.GetMostRecentErrorEvents.Post(modifier, model, ct);
    public Task<ModifierCategoryModel[]> GetModifierCategories(string modifier, CancellationToken ct = default) => Actions.GetModifierCategories.Post(modifier, new EmptyRequest(), ct);
    public Task<ModifierModel> GetDefaultModifier(string modifier, CancellationToken ct = default) => Actions.GetDefaultModifier.Post(modifier, new EmptyRequest(), ct);
    public sealed record AppGroupActions(AppClientGetAction<EmptyRequest> Index, AppClientPostAction<EmptyRequest, AppModel> GetApp, AppClientPostAction<EmptyRequest, ResourceGroupModel[]> GetResourceGroups, AppClientPostAction<EmptyRequest, AppRoleModel[]> GetRoles, AppClientPostAction<int, AppRequestExpandedModel[]> GetMostRecentRequests, AppClientPostAction<int, AppLogEntryModel[]> GetMostRecentErrorEvents, AppClientPostAction<EmptyRequest, ModifierCategoryModel[]> GetModifierCategories, AppClientPostAction<EmptyRequest, ModifierModel> GetDefaultModifier);
}