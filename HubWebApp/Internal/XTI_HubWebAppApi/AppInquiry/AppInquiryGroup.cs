namespace XTI_HubWebAppApi.AppInquiry;

public sealed class AppInquiryGroup : AppApiGroupWrapper
{
    public AppInquiryGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        Index = source.AddAction<EmptyRequest, WebViewResult>()
            .Named(nameof(Index))
            .WithExecution<IndexAction>()
            .Build();
        GetApp = source.AddAction<EmptyRequest, AppModel>()
            .Named(nameof(GetApp))
            .WithExecution<GetAppAction>()
            .Build();
        GetResourceGroups = source.AddAction<EmptyRequest, ResourceGroupModel[]>()
            .Named(nameof(GetResourceGroups))
            .WithExecution<GetResourceGroupsAction>()
            .Build();
        GetRoles = source.AddAction<EmptyRequest, AppRoleModel[]>()
            .Named(nameof(GetRoles))
            .WithExecution<GetRolesAction>()
            .Build();
        GetMostRecentRequests = source.AddAction<int, AppRequestExpandedModel[]>()
            .Named(nameof(GetMostRecentRequests))
            .WithExecution<GetMostRecentRequestsAction>()
            .Build();
        GetMostRecentErrorEvents = source.AddAction<int, AppLogEntryModel[]>()
            .Named(nameof(GetMostRecentErrorEvents))
            .WithExecution<GetMostRecentErrorEventsAction>()
            .Build();
        GetModifierCategories = source.AddAction<EmptyRequest, ModifierCategoryModel[]>()
            .Named(nameof(GetModifierCategories))
            .WithExecution<GetModifierCategoriesAction>()
            .Build();
        GetDefaultModifier = source.AddAction<EmptyRequest, ModifierModel>()
            .Named(nameof(GetDefaultModifier))
            .WithExecution<GetDefaultModifierAction>()
            .Build();
        GetDefaultOptions = source.AddAction<EmptyRequest, string>()
            .Named(nameof(GetDefaultOptions))
            .WithExecution<GetDefaultOptionsAction>()
            .Build();
        GetDefaultAppOptions = source.AddAction<EmptyRequest, string>()
            .Named(nameof(GetDefaultAppOptions))
            .WithExecution<GetDefaultAppOptionsAction>()
            .Build();
    }

    public AppApiAction<EmptyRequest, WebViewResult> Index { get; }
    public AppApiAction<EmptyRequest, AppModel> GetApp { get; }
    public AppApiAction<EmptyRequest, ResourceGroupModel[]> GetResourceGroups { get; }
    public AppApiAction<EmptyRequest, AppRoleModel[]> GetRoles { get; }
    public AppApiAction<int, AppRequestExpandedModel[]> GetMostRecentRequests { get; }
    public AppApiAction<int, AppLogEntryModel[]> GetMostRecentErrorEvents { get; }
    public AppApiAction<EmptyRequest, ModifierCategoryModel[]> GetModifierCategories { get; }
    public AppApiAction<EmptyRequest, ModifierModel> GetDefaultModifier { get; }
    public AppApiAction<EmptyRequest, string> GetDefaultOptions { get; }
    public AppApiAction<EmptyRequest, string> GetDefaultAppOptions { get; }
}