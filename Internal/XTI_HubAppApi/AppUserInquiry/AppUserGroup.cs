using Microsoft.Extensions.DependencyInjection;
using System;
using XTI_App.Api;
using XTI_Hub;
using XTI_WebApp.Api;

namespace XTI_HubAppApi.AppUserInquiry
{
    public sealed class AppUserGroup : AppApiGroupWrapper
    {
        public AppUserGroup(AppApiGroup source, IServiceProvider sp)
            : base(source)
        {
            var actions = new WebAppApiActionFactory(source);
            Index = source.AddAction(actions.View(nameof(Index), () => sp.GetRequiredService<IndexAction>()));
            GetUserRoles = source.AddAction(actions.Action(nameof(GetUserRoles), () => sp.GetRequiredService<GetUserRolesAction>()));
            GetUserRoleAccess = source.AddAction(actions.Action(nameof(GetUserRoleAccess), () => sp.GetRequiredService<GetUserRoleAccessAction>()));
            GetUserModCategories = source.AddAction(actions.Action(nameof(GetUserModCategories), () => sp.GetRequiredService<GetUserModifierCategoriesAction>()));
        }
        public AppApiAction<int, WebViewResult> Index { get; }
        public AppApiAction<GetUserRolesRequest, AppRoleModel[]> GetUserRoles { get; }
        public AppApiAction<GetUserRoleAccessRequest, UserRoleAccessModel> GetUserRoleAccess { get; }
        public AppApiAction<int, UserModifierCategoryModel[]> GetUserModCategories { get; }
    }
}
