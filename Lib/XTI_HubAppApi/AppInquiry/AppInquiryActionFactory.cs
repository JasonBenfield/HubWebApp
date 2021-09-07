using Microsoft.Extensions.DependencyInjection;
using System;
using XTI_App.Api;
using XTI_WebApp;
using XTI_WebApp.Api;

namespace XTI_HubAppApi.AppInquiry
{
    public sealed class AppInquiryActionFactory
    {
        private readonly IServiceProvider sp;

        public AppInquiryActionFactory(IServiceProvider sp)
        {
            this.sp = sp;
        }

        public TitledViewAppAction<EmptyRequest> CreateIndex()
        {
            var pageContext = sp.GetService<IPageContext>();
            return new TitledViewAppAction<EmptyRequest>(pageContext, "Index", "App");
        }

        internal GetAppAction CreateGetApp()
        {
            var appFromPath = sp.GetService<AppFromPath>();
            return new GetAppAction(appFromPath);
        }

        internal GetRolesAction CreateGetRoles()
        {
            var appFromPath = sp.GetService<AppFromPath>();
            return new GetRolesAction(appFromPath);
        }

        internal GetRoleAction CreateGetRole()
        {
            var appFromPath = sp.GetService<AppFromPath>();
            return new GetRoleAction(appFromPath);
        }

        internal GetResourceGroupsAction CreateGetResourceGroups()
        {
            var appFromPath = sp.GetService<AppFromPath>();
            return new GetResourceGroupsAction(appFromPath);
        }

        internal GetMostRecentRequestsAction CreateGetMostRecentRequests()
        {
            var appFromPath = sp.GetService<AppFromPath>();
            return new GetMostRecentRequestsAction(appFromPath);
        }

        internal GetMostRecentErrorEventsAction CreateGetMostRecentErrorEvents()
        {
            var appFromPath = sp.GetService<AppFromPath>();
            return new GetMostRecentErrorEventsAction(appFromPath);
        }

        internal GetModifierCategoriesAction CreateGetModifierCategories()
        {
            var appFromPath = sp.GetService<AppFromPath>();
            return new GetModifierCategoriesAction(appFromPath);
        }

        internal GetModifierCategoryAction CreateGetModifierCategory()
        {
            var appFromPath = sp.GetService<AppFromPath>();
            return new GetModifierCategoryAction(appFromPath);
        }

        internal GetDefaultModifierAction CreateGetDefaultModifier()
        {
            var appFromPath = sp.GetService<AppFromPath>();
            return new GetDefaultModifierAction(appFromPath);
        }
    }
}
