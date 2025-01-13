// Generated Code
namespace XTI_HubAppClient;
public sealed partial class AppGroup : AppClientGroup
{
    public AppGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "App")
    {
        Actions = new AppGroupActions(GetApp: CreatePostAction<EmptyRequest, AppModel>("GetApp"), GetDefaultAppOptions: CreatePostAction<EmptyRequest, string>("GetDefaultAppOptions"), GetDefaultModifier: CreatePostAction<EmptyRequest, ModifierModel>("GetDefaultModifier"), GetDefaultOptions: CreatePostAction<EmptyRequest, string>("GetDefaultOptions"), GetModifierCategories: CreatePostAction<EmptyRequest, ModifierCategoryModel[]>("GetModifierCategories"), GetMostRecentErrorEvents: CreatePostAction<int, AppLogEntryModel[]>("GetMostRecentErrorEvents"), GetMostRecentRequests: CreatePostAction<int, AppRequestExpandedModel[]>("GetMostRecentRequests"), GetResourceGroups: CreatePostAction<EmptyRequest, ResourceGroupModel[]>("GetResourceGroups"), GetRoles: CreatePostAction<EmptyRequest, AppRoleModel[]>("GetRoles"), Index: CreateGetAction<EmptyRequest>("Index"));
        Configure();
    }

    partial void Configure();
    public AppGroupActions Actions { get; }

    public Task<AppModel> GetApp(string modifier, CancellationToken ct = default) => Actions.GetApp.Post(modifier, new EmptyRequest(), ct);
    public Task<string> GetDefaultAppOptions(string modifier, CancellationToken ct = default) => Actions.GetDefaultAppOptions.Post(modifier, new EmptyRequest(), ct);
    public Task<ModifierModel> GetDefaultModifier(string modifier, CancellationToken ct = default) => Actions.GetDefaultModifier.Post(modifier, new EmptyRequest(), ct);
    public Task<string> GetDefaultOptions(string modifier, CancellationToken ct = default) => Actions.GetDefaultOptions.Post(modifier, new EmptyRequest(), ct);
    public Task<ModifierCategoryModel[]> GetModifierCategories(string modifier, CancellationToken ct = default) => Actions.GetModifierCategories.Post(modifier, new EmptyRequest(), ct);
    public Task<AppLogEntryModel[]> GetMostRecentErrorEvents(string modifier, int requestData, CancellationToken ct = default) => Actions.GetMostRecentErrorEvents.Post(modifier, requestData, ct);
    public Task<AppRequestExpandedModel[]> GetMostRecentRequests(string modifier, int requestData, CancellationToken ct = default) => Actions.GetMostRecentRequests.Post(modifier, requestData, ct);
    public Task<ResourceGroupModel[]> GetResourceGroups(string modifier, CancellationToken ct = default) => Actions.GetResourceGroups.Post(modifier, new EmptyRequest(), ct);
    public Task<AppRoleModel[]> GetRoles(string modifier, CancellationToken ct = default) => Actions.GetRoles.Post(modifier, new EmptyRequest(), ct);
    public sealed record AppGroupActions(AppClientPostAction<EmptyRequest, AppModel> GetApp, AppClientPostAction<EmptyRequest, string> GetDefaultAppOptions, AppClientPostAction<EmptyRequest, ModifierModel> GetDefaultModifier, AppClientPostAction<EmptyRequest, string> GetDefaultOptions, AppClientPostAction<EmptyRequest, ModifierCategoryModel[]> GetModifierCategories, AppClientPostAction<int, AppLogEntryModel[]> GetMostRecentErrorEvents, AppClientPostAction<int, AppRequestExpandedModel[]> GetMostRecentRequests, AppClientPostAction<EmptyRequest, ResourceGroupModel[]> GetResourceGroups, AppClientPostAction<EmptyRequest, AppRoleModel[]> GetRoles, AppClientGetAction<EmptyRequest> Index);
}