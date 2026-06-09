using XTI_HubWebAppApiActions.App;
using XTI_HubWebAppApiActions.AppInquiry;

// Generated Code
#nullable enable
namespace XTI_HubWebAppApi.App;
public sealed partial class AppGroupBuilder
{
    private readonly AppApiGroup source;
    internal AppGroupBuilder(AppApiGroup source)
    {
        this.source = source;
        ConfigureInstall = source.AddAction<ConfigureAppInstallRequest, InstallConfigurationModel>("ConfigureInstall").WithExecution<ConfigureInstallAction>();
        DeleteInstallConfiguration = source.AddAction<InstallConfigurationIDRequest, EmptyActionResult>("DeleteInstallConfiguration").WithExecution<DeleteInstallConfigurationAction>();
        GetApp = source.AddAction<EmptyRequest, AppModel>("GetApp").WithExecution<GetAppAction>();
        GetDefaultAppOptions = source.AddAction<EmptyRequest, string>("GetDefaultAppOptions").WithExecution<GetDefaultAppOptionsAction>();
        GetDefaultModifier = source.AddAction<EmptyRequest, ModifierModel>("GetDefaultModifier").WithExecution<GetDefaultModifierAction>();
        GetDefaultOptions = source.AddAction<EmptyRequest, string>("GetDefaultOptions").WithExecution<GetDefaultOptionsAction>();
        GetInstallConfigurations = source.AddAction<EmptyRequest, InstallConfigurationModel[]>("GetInstallConfigurations").WithExecution<GetInstallConfigurationsAction>();
        GetModifierCategories = source.AddAction<EmptyRequest, ModifierCategoryModel[]>("GetModifierCategories").WithExecution<GetModifierCategoriesAction>();
        GetMostRecentErrorEvents = source.AddAction<int, AppLogEntryModel[]>("GetMostRecentErrorEvents").WithExecution<GetMostRecentErrorEventsAction>();
        GetMostRecentRequests = source.AddAction<int, AppRequestExpandedModel[]>("GetMostRecentRequests").WithExecution<GetMostRecentRequestsAction>();
        GetResourceGroups = source.AddAction<EmptyRequest, ResourceGroupModel[]>("GetResourceGroups").WithExecution<GetResourceGroupsAction>();
        GetRoles = source.AddAction<EmptyRequest, AppRoleModel[]>("GetRoles").WithExecution<GetRolesAction>();
        Index = source.AddAction<EmptyRequest, WebViewResult>("Index").WithExecution<IndexAction>();
        UpdateVersionsFromPublished = source.AddAction<EmptyRequest, EmptyActionResult>("UpdateVersionsFromPublished").WithExecution<UpdateVersionsFromPublishedAction>();
        Configure();
    }

    partial void Configure();
    public AppApiActionBuilder<ConfigureAppInstallRequest, InstallConfigurationModel> ConfigureInstall { get; }
    public AppApiActionBuilder<InstallConfigurationIDRequest, EmptyActionResult> DeleteInstallConfiguration { get; }
    public AppApiActionBuilder<EmptyRequest, AppModel> GetApp { get; }
    public AppApiActionBuilder<EmptyRequest, string> GetDefaultAppOptions { get; }
    public AppApiActionBuilder<EmptyRequest, ModifierModel> GetDefaultModifier { get; }
    public AppApiActionBuilder<EmptyRequest, string> GetDefaultOptions { get; }
    public AppApiActionBuilder<EmptyRequest, InstallConfigurationModel[]> GetInstallConfigurations { get; }
    public AppApiActionBuilder<EmptyRequest, ModifierCategoryModel[]> GetModifierCategories { get; }
    public AppApiActionBuilder<int, AppLogEntryModel[]> GetMostRecentErrorEvents { get; }
    public AppApiActionBuilder<int, AppRequestExpandedModel[]> GetMostRecentRequests { get; }
    public AppApiActionBuilder<EmptyRequest, ResourceGroupModel[]> GetResourceGroups { get; }
    public AppApiActionBuilder<EmptyRequest, AppRoleModel[]> GetRoles { get; }
    public AppApiActionBuilder<EmptyRequest, WebViewResult> Index { get; }
    public AppApiActionBuilder<EmptyRequest, EmptyActionResult> UpdateVersionsFromPublished { get; }

    public AppGroup Build() => new AppGroup(source, this);
}