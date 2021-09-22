using Microsoft.Extensions.DependencyInjection;
using System;

namespace XTI_HubAppApi.ResourceGroupInquiry
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

        internal GetResourceAction CreateGetResource()
        {
            var appFromPath = sp.GetService<AppFromPath>();
            return new GetResourceAction(appFromPath);
        }

        internal GetResourceGroupAction CreateGetResourceGroup()
        {
            var appFromPath = sp.GetService<AppFromPath>();
            return new GetResourceGroupAction(appFromPath);
        }

        internal GetRoleAccessAction CreateGetResourceGroupRoleAccess()
        {
            var appFromPath = sp.GetService<AppFromPath>();
            return new GetRoleAccessAction(appFromPath);
        }

        internal GetModCategoryAction CreateGetResourceGroupModCategory()
        {
            var appFromPath = sp.GetService<AppFromPath>();
            return new GetModCategoryAction(appFromPath);
        }

        internal GetMostRecentErrorEventsAction CreateGetMostRecentErrorEvents()
        {
            var appFromPath = sp.GetService<AppFromPath>();
            return new GetMostRecentErrorEventsAction(appFromPath);
        }

        internal GetMostRecentRequestsAction CreateGetMostRecentRequests()
        {
            var appFromPath = sp.GetService<AppFromPath>();
            return new GetMostRecentRequestsAction(appFromPath);
        }
    }
}
