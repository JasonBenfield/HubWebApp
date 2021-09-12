using System;
using XTI_HubAppApi.AppRegistration;

namespace XTI_HubAppApi
{
    partial class HubAppApi
    {
        public AppRegistrationGroup AppRegistration { get; private set; }

        partial void appRegistration(IServiceProvider services)
        {
            AppRegistration = new AppRegistrationGroup
            (
                source.AddGroup
                (
                    nameof(AppRegistration),
                    HubInfo.ModCategories.Apps,
                    Access.WithAllowed(HubInfo.Roles.RegisterApp)
                ),
                services
            );
        }
    }
}
