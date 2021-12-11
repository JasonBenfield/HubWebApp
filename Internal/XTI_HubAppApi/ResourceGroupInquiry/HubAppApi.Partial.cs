using System;
using XTI_Hub;
using XTI_HubAppApi.ResourceGroupInquiry;

namespace XTI_HubAppApi
{
    partial class HubAppApi
    {
        public ResourceGroupInquiryGroup ResourceGroup { get; private set; }

        partial void resourceGroup(IServiceProvider services)
        {
            ResourceGroup = new ResourceGroupInquiryGroup
            (
                source.AddGroup
                (
                    nameof(ResourceGroup),
                    HubInfo.ModCategories.Apps,
                    Access.WithAllowed(HubInfo.Roles.ViewApp)
                ),
                services
            );
        }
    }
}
