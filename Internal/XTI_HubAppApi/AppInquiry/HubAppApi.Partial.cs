using System;
using XTI_Hub;
using XTI_HubAppApi.AppInquiry;

namespace XTI_HubAppApi
{
    partial class HubAppApi
    {
        public AppInquiryGroup App { get; private set; }

        partial void appInquiry(IServiceProvider services)
        {
            App = new AppInquiryGroup
            (
                source.AddGroup
                (
                    nameof(App),
                    HubInfo.ModCategories.Apps,
                    Access.WithAllowed(HubInfo.Roles.ViewApp)
                ),
                services
            );
        }
    }
}
