using Microsoft.Extensions.DependencyInjection;
using System;
using XTI_App;
using XTI_App.Api;
using XTI_WebApp;
using XTI_WebApp.Api;

namespace HubWebAppApi.Apps
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

        internal GetCurrentVersionAction CreateGetCurrentVersion()
        {
            var appFromPath = sp.GetService<AppFromPath>();
            return new GetCurrentVersionAction(appFromPath);
        }

        internal GetResourceGroupsAction CreateGetResourceGroups()
        {
            var appFromPath = sp.GetService<AppFromPath>();
            return new GetResourceGroupsAction(appFromPath);
        }

        internal GetMostRecentRequestsForAppAction CreateGetMostRecentRequests()
        {
            var appFromPath = sp.GetService<AppFromPath>();
            return new GetMostRecentRequestsForAppAction(appFromPath);
        }

        internal GetMostRecentErrorEventsForAppAction CreateGetMostRecentErrorEvents()
        {
            var appFromPath = sp.GetService<AppFromPath>();
            return new GetMostRecentErrorEventsForAppAction(appFromPath);
        }

        internal GetModifierCategoriesAction CreateGetModifierCategories()
        {
            var appFromPath = sp.GetService<AppFromPath>();
            return new GetModifierCategoriesAction(appFromPath);
        }

        internal GetResourcesAction CreateGetResources()
        {
            var appFromPath = sp.GetService<AppFromPath>();
            return new GetResourcesAction(appFromPath);
        }

        internal GetResourceGroupAction CreateGetResourceGroup()
        {
            var appFromPath = sp.GetService<AppFromPath>();
            return new GetResourceGroupAction(appFromPath);
        }

        internal GetResourceGroupRoleAccessAction CreateGetResourceGroupRoleAccess()
        {
            var appFromPath = sp.GetService<AppFromPath>();
            return new GetResourceGroupRoleAccessAction(appFromPath);
        }

        internal GetResourceGroupModCategoryAction CreateGetResourceGroupModCategory()
        {
            var appFromPath = sp.GetService<AppFromPath>();
            return new GetResourceGroupModCategoryAction(appFromPath);
        }

        internal GetResourceAction CreateGetResource()
        {
            var appFromPath = sp.GetService<AppFromPath>();
            return new GetResourceAction(appFromPath);
        }

        internal GetResourceRoleAccessAction CreateGetResourceRoleAccess()
        {
            var appFromPath = sp.GetService<AppFromPath>();
            return new GetResourceRoleAccessAction(appFromPath);
        }

        internal GetMostRecentErrorEventsForResourceGroupAction CreateGetMostRecentErrorEventsForResourceGroup()
        {
            var appFromPath = sp.GetService<AppFromPath>();
            return new GetMostRecentErrorEventsForResourceGroupAction(appFromPath);
        }

        internal GetMostRecentErrorEventsForResourceAction CreateGetMostRecentErrorEventsForResource()
        {
            var appFromPath = sp.GetService<AppFromPath>();
            return new GetMostRecentErrorEventsForResourceAction(appFromPath);
        }

        internal GetMostRecentRequestsForResourceGroupAction CreateGetMostRecentRequestsForResourceGroupAction()
        {
            var appFromPath = sp.GetService<AppFromPath>();
            return new GetMostRecentRequestsForResourceGroupAction(appFromPath);
        }

        internal GetMostRecentRequestsForResourceAction CreateGetMostRecentRequestsForResourceAction()
        {
            var appFromPath = sp.GetService<AppFromPath>();
            return new GetMostRecentRequestsForResourceAction(appFromPath);
        }
    }
}
