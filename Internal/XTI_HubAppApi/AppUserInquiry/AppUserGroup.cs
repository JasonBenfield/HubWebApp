using Microsoft.Extensions.DependencyInjection;
using XTI_App.Api;
using XTI_Hub;
using XTI_WebApp.Api;

namespace XTI_HubAppApi.AppUserInquiry;

public sealed class AppUserGroup : AppApiGroupWrapper
{
    public AppUserGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        var actions = new WebAppApiActionFactory(source);
        Index = source.AddAction(actions.View(nameof(Index), () => sp.GetRequiredService<IndexAction>()));
        GetUserAccess = source.AddAction(actions.Action(nameof(GetUserAccess), () => sp.GetRequiredService<GetUserAccessAction>()));
        GetUnassignedRoles = source.AddAction(actions.Action(nameof(GetUnassignedRoles), () => sp.GetRequiredService<GetUnassignedRolesAction>()));
        GetUserModCategories = source.AddAction(actions.Action(nameof(GetUserModCategories), () => sp.GetRequiredService<GetUserModifierCategoriesAction>()));
    }
    public AppApiAction<int, WebViewResult> Index { get; }
    public AppApiAction<UserModifierKey, UserAccessModel> GetUserAccess { get; }
    public AppApiAction<UserModifierKey, AppRoleModel[]> GetUnassignedRoles { get; }
    public AppApiAction<int, UserModifierCategoryModel[]> GetUserModCategories { get; }
}