﻿namespace XTI_HubWebAppApi.AppInquiry;

public sealed class AppInquiryGroup : AppApiGroupWrapper
{
    public AppInquiryGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        Index = source.AddAction(nameof(Index), () => sp.GetRequiredService<IndexAction>());
        GetApp = source.AddAction(nameof(GetApp), () => sp.GetRequiredService<GetAppAction>());
        GetResourceGroups = source.AddAction(nameof(GetResourceGroups), () => sp.GetRequiredService<GetResourceGroupsAction>());
        GetRoles = source.AddAction(nameof(GetRoles), () => sp.GetRequiredService<GetRolesAction>());
        GetMostRecentRequests = source.AddAction(nameof(GetMostRecentRequests), () => sp.GetRequiredService<GetMostRecentRequestsAction>());
        GetMostRecentErrorEvents = source.AddAction(nameof(GetMostRecentErrorEvents), () => sp.GetRequiredService<GetMostRecentErrorEventsAction>());
        GetModifierCategories = source.AddAction(nameof(GetModifierCategories), () => sp.GetRequiredService<GetModifierCategoriesAction>());
        GetDefaultModifier = source.AddAction(nameof(GetDefaultModifier), () => sp.GetRequiredService<GetDefaultModifierAction>());
        GetDefaultOptions = source.AddAction(nameof(GetDefaultOptions), () => sp.GetRequiredService<GetDefaultOptionsAction>());
        GetDefaultAppOptions = source.AddAction(nameof(GetDefaultAppOptions), () => sp.GetRequiredService<GetDefaultAppOptionsAction>());
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