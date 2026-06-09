// Generated Code
namespace XTI_HubAppClient;
public sealed partial class AppGroup : AppClientGroup
{
    public AppGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "App")
    {
        Actions = new AppGroupActions(ConfigureInstall: CreatePostAction<ConfigureAppInstallRequest, InstallConfigurationModel>("ConfigureInstall"), DeleteInstallConfiguration: CreatePostAction<InstallConfigurationIDRequest, EmptyActionResult>("DeleteInstallConfiguration"), GetApp: CreatePostAction<EmptyRequest, AppModel>("GetApp"), GetDefaultAppOptions: CreatePostAction<EmptyRequest, string>("GetDefaultAppOptions"), GetDefaultModifier: CreatePostAction<EmptyRequest, ModifierModel>("GetDefaultModifier"), GetDefaultOptions: CreatePostAction<EmptyRequest, string>("GetDefaultOptions"), GetInstallConfigurations: CreatePostAction<EmptyRequest, InstallConfigurationModel[]>("GetInstallConfigurations"), GetModifierCategories: CreatePostAction<EmptyRequest, ModifierCategoryModel[]>("GetModifierCategories"), GetMostRecentErrorEvents: CreatePostAction<int, AppLogEntryModel[]>("GetMostRecentErrorEvents"), GetMostRecentRequests: CreatePostAction<int, AppRequestExpandedModel[]>("GetMostRecentRequests"), GetResourceGroups: CreatePostAction<EmptyRequest, ResourceGroupModel[]>("GetResourceGroups"), GetRoles: CreatePostAction<EmptyRequest, AppRoleModel[]>("GetRoles"), Index: CreateGetAction<EmptyRequest>("Index"), UpdateVersionsFromPublished: CreatePostAction<EmptyRequest, EmptyActionResult>("UpdateVersionsFromPublished"));
        Configure();
    }

    partial void Configure();
    public AppGroupActions Actions { get; }

    public Task<InstallConfigurationModel> ConfigureInstall(string modifier, ConfigureAppInstallRequest requestData, CancellationToken ct = default) => Actions.ConfigureInstall.Post(modifier, requestData, ct);
    public Task<EmptyActionResult> DeleteInstallConfiguration(string modifier, InstallConfigurationIDRequest requestData, CancellationToken ct = default) => Actions.DeleteInstallConfiguration.Post(modifier, requestData, ct);
    public Task<AppModel> GetApp(string modifier, CancellationToken ct = default) => Actions.GetApp.Post(modifier, new EmptyRequest(), ct);
    public Task<string> GetDefaultAppOptions(string modifier, CancellationToken ct = default) => Actions.GetDefaultAppOptions.Post(modifier, new EmptyRequest(), ct);
    public Task<ModifierModel> GetDefaultModifier(string modifier, CancellationToken ct = default) => Actions.GetDefaultModifier.Post(modifier, new EmptyRequest(), ct);
    public Task<string> GetDefaultOptions(string modifier, CancellationToken ct = default) => Actions.GetDefaultOptions.Post(modifier, new EmptyRequest(), ct);
    public Task<InstallConfigurationModel[]> GetInstallConfigurations(string modifier, CancellationToken ct = default) => Actions.GetInstallConfigurations.Post(modifier, new EmptyRequest(), ct);
    public Task<ModifierCategoryModel[]> GetModifierCategories(string modifier, CancellationToken ct = default) => Actions.GetModifierCategories.Post(modifier, new EmptyRequest(), ct);
    public Task<AppLogEntryModel[]> GetMostRecentErrorEvents(string modifier, int requestData, CancellationToken ct = default) => Actions.GetMostRecentErrorEvents.Post(modifier, requestData, ct);
    public Task<AppRequestExpandedModel[]> GetMostRecentRequests(string modifier, int requestData, CancellationToken ct = default) => Actions.GetMostRecentRequests.Post(modifier, requestData, ct);
    public Task<ResourceGroupModel[]> GetResourceGroups(string modifier, CancellationToken ct = default) => Actions.GetResourceGroups.Post(modifier, new EmptyRequest(), ct);
    public Task<AppRoleModel[]> GetRoles(string modifier, CancellationToken ct = default) => Actions.GetRoles.Post(modifier, new EmptyRequest(), ct);
    public Task<EmptyActionResult> UpdateVersionsFromPublished(string modifier, CancellationToken ct = default) => Actions.UpdateVersionsFromPublished.Post(modifier, new EmptyRequest(), ct);
    public sealed record AppGroupActions(AppClientPostAction<ConfigureAppInstallRequest, InstallConfigurationModel> ConfigureInstall, AppClientPostAction<InstallConfigurationIDRequest, EmptyActionResult> DeleteInstallConfiguration, AppClientPostAction<EmptyRequest, AppModel> GetApp, AppClientPostAction<EmptyRequest, string> GetDefaultAppOptions, AppClientPostAction<EmptyRequest, ModifierModel> GetDefaultModifier, AppClientPostAction<EmptyRequest, string> GetDefaultOptions, AppClientPostAction<EmptyRequest, InstallConfigurationModel[]> GetInstallConfigurations, AppClientPostAction<EmptyRequest, ModifierCategoryModel[]> GetModifierCategories, AppClientPostAction<int, AppLogEntryModel[]> GetMostRecentErrorEvents, AppClientPostAction<int, AppRequestExpandedModel[]> GetMostRecentRequests, AppClientPostAction<EmptyRequest, ResourceGroupModel[]> GetResourceGroups, AppClientPostAction<EmptyRequest, AppRoleModel[]> GetRoles, AppClientGetAction<EmptyRequest> Index, AppClientPostAction<EmptyRequest, EmptyActionResult> UpdateVersionsFromPublished);
}