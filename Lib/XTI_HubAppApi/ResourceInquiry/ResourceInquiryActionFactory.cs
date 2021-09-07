using Microsoft.Extensions.DependencyInjection;
using System;

namespace XTI_HubAppApi.ResourceInquiry
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

        internal GetRoleAccessAction CreateGetResourceRoleAccess()
        {
            var appFromPath = sp.GetService<AppFromPath>();
            return new GetRoleAccessAction(appFromPath);
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
