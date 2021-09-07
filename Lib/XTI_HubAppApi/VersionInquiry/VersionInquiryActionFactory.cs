using Microsoft.Extensions.DependencyInjection;
using System;

namespace XTI_HubAppApi.VersionInquiry
{
    public sealed class VersionInquiryActionFactory
    {
        private readonly IServiceProvider services;

        public VersionInquiryActionFactory(IServiceProvider services)
        {
            this.services = services;
        }

        internal GetVersionAction CreateGetVersion()
        {
            var appFromPath = services.GetService<AppFromPath>();
            return new GetVersionAction(appFromPath);
        }

        internal GetCurrentVersionAction CreateGetCurrentVersion()
        {
            var appFromPath = services.GetService<AppFromPath>();
            return new GetCurrentVersionAction(appFromPath);
        }

        internal GetResourceGroupAction CreateGetResourcegroup()
        {
            var appFromPath = services.GetService<AppFromPath>();
            return new GetResourceGroupAction(appFromPath);
        }
    }
}
