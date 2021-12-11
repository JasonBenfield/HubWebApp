using System;
using XTI_Hub;
using XTI_HubAppApi.AppPublish;

namespace XTI_HubAppApi
{
    partial class HubAppApi
    {
        public PublishGroup Publish { get; private set; }

        partial void publish(IServiceProvider services)
        {
            Publish = new PublishGroup
            (
                source.AddGroup
                (
                    nameof(Publish),
                    HubInfo.ModCategories.Apps,
                    Access.WithAllowed(HubInfo.Roles.Publisher)
                ),
                services
            );
        }
    }
}
