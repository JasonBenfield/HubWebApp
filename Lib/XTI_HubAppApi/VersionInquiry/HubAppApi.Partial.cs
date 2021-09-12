using System;
using XTI_HubAppApi.VersionInquiry;

namespace XTI_HubAppApi
{
    partial class HubAppApi
    {
        public VersionInquiryGroup Version { get; private set; }

        partial void version(IServiceProvider services)
        {
            Version = new VersionInquiryGroup
            (
                source.AddGroup
                (
                    nameof(Version),
                    HubInfo.ModCategories.Apps,
                    Access.WithAllowed(HubInfo.Roles.ViewApp)
                ),
                services
            );
        }
    }
}
