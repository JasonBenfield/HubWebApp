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
        GetApp = source.AddAction<EmptyRequest, AppModel>("GetApp").WithExecution<GetAppAction>();
        GetDefaultAppOptions = source.AddAction<EmptyRequest, string>("GetDefaultAppOptions").WithExecution<GetDefaultAppOptionsAction>();
        GetDefaultModifier = source.AddAction<EmptyRequest, ModifierModel>("GetDefaultModifier").WithExecution<GetDefaultModifierAction>();
        GetDefaultOptions = source.AddAction<EmptyRequest, string>("GetDefaultOptions").WithExecution<GetDefaultOptionsAction>();
        GetModifierCategories = source.AddAction<EmptyRequest, ModifierCategoryModel[]>("GetModifierCategories").WithExecution<GetModifierCategoriesAction>();
        GetMostRecentErrorEvents = source.AddAction<int, AppLogEntryModel[]>("GetMostRecentErrorEvents").WithExecution<GetMostRecentErrorEventsAction>();
        GetMostRecentRequests = source.AddAction<int, AppRequestExpandedModel[]>("GetMostRecentRequests").WithExecution<GetMostRecentRequestsAction>();
        GetResourceGroups = source.AddAction<EmptyRequest, ResourceGroupModel[]>("GetResourceGroups").WithExecution<GetResourceGroupsAction>();
        GetRoles = source.AddAction<EmptyRequest, AppRoleModel[]>("GetRoles").WithExecution<GetRolesAction>();
        Index = source.AddAction<EmptyRequest, WebViewResult>("Index").WithExecution<IndexAction>();
        Configure();
    }

    partial void Configure();
    public AppApiActionBuilder<EmptyRequest, AppModel> GetApp { get; }
    public AppApiActionBuilder<EmptyRequest, string> GetDefaultAppOptions { get; }
    public AppApiActionBuilder<EmptyRequest, ModifierModel> GetDefaultModifier { get; }
    public AppApiActionBuilder<EmptyRequest, string> GetDefaultOptions { get; }
    public AppApiActionBuilder<EmptyRequest, ModifierCategoryModel[]> GetModifierCategories { get; }
    public AppApiActionBuilder<int, AppLogEntryModel[]> GetMostRecentErrorEvents { get; }
    public AppApiActionBuilder<int, AppRequestExpandedModel[]> GetMostRecentRequests { get; }
    public AppApiActionBuilder<EmptyRequest, ResourceGroupModel[]> GetResourceGroups { get; }
    public AppApiActionBuilder<EmptyRequest, AppRoleModel[]> GetRoles { get; }
    public AppApiActionBuilder<EmptyRequest, WebViewResult> Index { get; }

    public AppGroup Build() => new AppGroup(source, this);
}