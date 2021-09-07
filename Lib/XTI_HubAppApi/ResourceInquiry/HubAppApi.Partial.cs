using System;
using XTI_HubAppApi.ResourceInquiry;

namespace XTI_HubAppApi
{
    partial class HubAppApi
    {
        public ResourceInquiryGroup Resource { get; private set; }

        partial void resource(IServiceProvider services)
        {
            Resource = new ResourceInquiryGroup
            (
                source.AddGroup
                (
                    nameof(Resource),
                    HubInfo.ModCategories.Apps,
                    Access.WithAllowed(HubInfo.Roles.ViewApp)
                ),
                new ResourceInquiry.ResourceInquiryActionFactory(services)
            );
        }
    }
}
