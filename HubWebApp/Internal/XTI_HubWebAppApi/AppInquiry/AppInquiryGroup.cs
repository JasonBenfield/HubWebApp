namespace XTI_HubWebAppApi.AppInquiry;

public sealed class AppInquiryGroup : AppApiGroupWrapper
{
    public AppInquiryGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        Index = source.AddAction(nameof(Index), () => sp.GetRequiredService<IndexAction>());
        GetApp = source.AddAction(nameof(GetApp), () => sp.GetRequiredService<GetAppAction>());
        GetRoles = source.AddAction(nameof(GetRoles), () => sp.GetRequiredService<GetRolesAction>());
        GetRole = source.AddAction(nameof(GetRole), () => sp.GetRequiredService<GetRoleAction>());
        GetResourceGroups = source.AddAction(nameof(GetResourceGroups), () => sp.GetRequiredService<GetResourceGroupsAction>());
        GetMostRecentRequests = source.AddAction(nameof(GetMostRecentRequests), () => sp.GetRequiredService<GetMostRecentRequestsAction>());
        GetMostRecentErrorEvents = source.AddAction(nameof(GetMostRecentErrorEvents), () => sp.GetRequiredService<GetMostRecentErrorEventsAction>());
        GetModifierCategories = source.AddAction(nameof(GetModifierCategories), () => sp.GetRequiredService<GetModifierCategoriesAction>());
        GetModifierCategory = source.AddAction(nameof(GetModifierCategory), () => sp.GetRequiredService<GetModifierCategoryAction>());
        GetDefaultModifier = source.AddAction(nameof(GetDefaultModifier), () => sp.GetRequiredService<GetDefaultModifierAction>());
    }

    public AppApiAction<EmptyRequest, WebViewResult> Index { get; }
    public AppApiAction<EmptyRequest, AppModel> GetApp { get; }
    public AppApiAction<EmptyRequest, AppRoleModel[]> GetRoles { get; }
    public AppApiAction<string, AppRoleModel> GetRole { get; }
    public AppApiAction<EmptyRequest, ResourceGroupModel[]> GetResourceGroups { get; }
    public AppApiAction<int, AppRequestExpandedModel[]> GetMostRecentRequests { get; }
    public AppApiAction<int, AppLogEntryModel[]> GetMostRecentErrorEvents { get; }
    public AppApiAction<EmptyRequest, ModifierCategoryModel[]> GetModifierCategories { get; }
    public AppApiAction<string, ModifierCategoryModel> GetModifierCategory { get; }
    public AppApiAction<EmptyRequest, ModifierModel> GetDefaultModifier { get; }
}