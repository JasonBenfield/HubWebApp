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

    }
}
