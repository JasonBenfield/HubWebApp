using XTI_HubWebAppApiActions.AppInquiry;

// Generated Code
#nullable enable
namespace XTI_HubWebAppApi.App;
public sealed partial class AppGroup : AppApiGroupWrapper
{
    internal AppGroup(AppApiGroup source, AppGroupBuilder builder) : base(source)
    {
        GetApp = builder.GetApp.Build();
        GetDefaultAppOptions = builder.GetDefaultAppOptions.Build();
        GetDefaultModifier = builder.GetDefaultModifier.Build();
        GetDefaultOptions = builder.GetDefaultOptions.Build();
        GetModifierCategories = builder.GetModifierCategories.Build();
        GetMostRecentErrorEvents = builder.GetMostRecentErrorEvents.Build();
        GetMostRecentRequests = builder.GetMostRecentRequests.Build();
        GetResourceGroups = builder.GetResourceGroups.Build();
        GetRoles = builder.GetRoles.Build();
        Index = builder.Index.Build();
        Configure();
    }

    partial void Configure();
    public AppApiAction<EmptyRequest, AppModel> GetApp { get; }
    public AppApiAction<EmptyRequest, string> GetDefaultAppOptions { get; }
    public AppApiAction<EmptyRequest, ModifierModel> GetDefaultModifier { get; }
    public AppApiAction<EmptyRequest, string> GetDefaultOptions { get; }
    public AppApiAction<EmptyRequest, ModifierCategoryModel[]> GetModifierCategories { get; }
    public AppApiAction<int, AppLogEntryModel[]> GetMostRecentErrorEvents { get; }
    public AppApiAction<int, AppRequestExpandedModel[]> GetMostRecentRequests { get; }
    public AppApiAction<EmptyRequest, ResourceGroupModel[]> GetResourceGroups { get; }
    public AppApiAction<EmptyRequest, AppRoleModel[]> GetRoles { get; }
    public AppApiAction<EmptyRequest, WebViewResult> Index { get; }
}