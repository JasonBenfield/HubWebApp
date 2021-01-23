using Microsoft.Extensions.DependencyInjection;
using System;

namespace HubWebApp.Apps
{
    public sealed class ResourceInquiryActionFactory
    {
        private readonly IServiceProvider sp;

        public ResourceInquiryActionFactory(IServiceProvider sp)
        {
            this.sp = sp;
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

        internal GetMostRecentErrorEventsForResourceAction CreateGetMostRecentErrorEvents()
        {
            var appFromPath = sp.GetService<AppFromPath>();
            return new GetMostRecentErrorEventsForResourceAction(appFromPath);
        }

        internal GetMostRecentRequestsForResourceAction CreateGetMostRecentRequests()
        {
            var appFromPath = sp.GetService<AppFromPath>();
            return new GetMostRecentRequestsForResourceAction(appFromPath);
        }
    }
}
