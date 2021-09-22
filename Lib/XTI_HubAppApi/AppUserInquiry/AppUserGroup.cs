﻿using XTI_Hub;
using XTI_App.Api;
using XTI_WebApp.Api;

namespace XTI_HubAppApi.AppUserInquiry
{
    public sealed class AppUserGroup : AppApiGroupWrapper
    {
        public AppUserGroup(AppApiGroup source, AppUserGroupFactory factory)
            : base(source)
        {
            var actions = new WebAppApiActionFactory(source);
            Index = source.AddAction(actions.View(nameof(Index), factory.CreateIndex));
            GetUserRoles = source.AddAction(actions.Action(nameof(GetUserRoles), factory.CreateGetUserRoles));
            GetUserRoleAccess = source.AddAction(actions.Action(nameof(GetUserRoleAccess), factory.CreateGetUserRoleAccess));
            GetUserModCategories = source.AddAction(actions.Action(nameof(GetUserModCategories), factory.CreateGetUserModifierCategories));
        }
        public AppApiAction<int, WebViewResult> Index { get; }
        public AppApiAction<GetUserRolesRequest, AppRoleModel[]> GetUserRoles { get; }
        public AppApiAction<GetUserRoleAccessRequest, UserRoleAccessModel> GetUserRoleAccess { get; }
        public AppApiAction<int, UserModifierCategoryModel[]> GetUserModCategories { get; }
    }
}
