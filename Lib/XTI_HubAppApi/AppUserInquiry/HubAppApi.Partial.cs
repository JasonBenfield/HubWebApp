using System;
using XTI_HubAppApi.AppUserInquiry;

namespace XTI_HubAppApi
{
    partial class HubAppApi
    {
        public AppUserGroup AppUser { get; private set; }

        partial void appUser(IServiceProvider services)
        {
            AppUser = new AppUserGroup
            (
                source.AddGroup
                (
                    nameof(AppUser),
                    HubInfo.ModCategories.Apps,
                    Access.WithAllowed(HubInfo.Roles.ViewApp, HubInfo.Roles.ViewUser)
                ),
                new AppUserGroupFactory(services)
            );
        }
    }
}
