using XTI_HubAppApi.Apps;
using Microsoft.Extensions.DependencyInjection;
using System;
using XTI_App;
using XTI_WebApp;
using XTI_WebApp.Api;

namespace XTI_HubAppApi.Users
{
    public sealed class AppUserGroupFactory
    {
        private readonly IServiceProvider services;

        public AppUserGroupFactory(IServiceProvider services)
        {
            this.services = services;
        }

        internal TitledViewAppAction<int> CreateIndex()
        {
            var pageContext = services.GetService<IPageContext>();
            return new TitledViewAppAction<int>(pageContext, "Index", "App User");
        }

        internal GetUserRolesAction CreateGetUserRoles()
        {
            var appFromPath = services.GetService<AppFromPath>();
            var factory = services.GetService<AppFactory>();
            return new GetUserRolesAction(appFromPath, factory);
        }

        internal GetUserRoleAccessAction CreateGetUserRoleAccess()
        {
            var appFromPath = services.GetService<AppFromPath>();
            var factory = services.GetService<AppFactory>();
            return new GetUserRoleAccessAction(appFromPath, factory);
        }

        internal GetUserModifierCategoriesAction CreateGetUserModifierCategories()
        {
            var appFromPath = services.GetService<AppFromPath>();
            var factory = services.GetService<AppFactory>();
            return new GetUserModifierCategoriesAction(appFromPath, factory);
        }
    }
}
