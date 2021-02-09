using Microsoft.Extensions.DependencyInjection;
using System;

namespace HubWebAppApi.Apps
{
    public sealed class ResourceGroupInquiryActionFactory
    {
        private readonly IServiceProvider sp;

        public ResourceGroupInquiryActionFactory(IServiceProvider sp)
        {
            this.sp = sp;
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

        internal GetMostRecentErrorEventsForResourceGroupAction CreateGetMostRecentErrorEvents()
        {
            var appFromPath = sp.GetService<AppFromPath>();
            return new GetMostRecentErrorEventsForResourceGroupAction(appFromPath);
        }

        internal GetMostRecentRequestsForResourceGroupAction CreateGetMostRecentRequests()
        {
            var appFromPath = sp.GetService<AppFromPath>();
            return new GetMostRecentRequestsForResourceGroupAction(appFromPath);
        }
    }
}
